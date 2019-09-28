using Sigrun.Serialization;
using Sigrun.Serialization.UETypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sigrun.Extensions
{
    public static class BinaryReaderExtensions
    {
        public static T ReadObject<T>(this BinaryReader br) where T : ICanRead, new()
        {
            var obj = new T();
            obj.Read(br);
            return obj;
        }

        public static string ReadFixedString(this BinaryReader br, int length) => ReadFixedString(br, length, Encoding.UTF8);

        public static string ReadFixedString(this BinaryReader br, int length, Encoding encoding)
        {
            var b = br.ReadBytes(length);
            return encoding.GetString(b);
        }

        public static string ReadStringZero(this BinaryReader br) => ReadStringZero(br, Encoding.UTF8);

        /// <summary>
        /// Read zero terminated string (skips zeros in front of it).
        /// Leaves BaseStream.Position += stringLength + 1 (for szString terminator)
        /// </summary>
        /// <param name="br"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ReadStringZero(this BinaryReader br, Encoding encoding)
        {
            byte b;
            List<byte> szBytes = new List<byte>();

            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                b = br.ReadByte();

                if (b == 0)
                {
                    break;
                }
                else
                {
                    szBytes.Add(b);
                }
            }

            return encoding.GetString(szBytes.ToArray());
        }

        /// <summary>
        /// Read Unreal Engine string.
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static string ReadUEString(this BinaryReader br)
        {
            if (br.PeekChar() < 0)
                return null;

            var length = br.ReadInt32();
            if (length == 0)
                return null;

            if (length == 1)
                return "";

            var valueBytes = br.ReadBytes(length);
            return Encoding.UTF8.GetString(valueBytes, 0, valueBytes.Length - 1);
        }

        public static UEProperty ReadUEProperty(this BinaryReader br)
        {
            var name = br.ReadUEString();
            if (name == null) return null;

            if (name == "None")
            {
                br.BaseStream.Position += 4; // Skip 4 zeros
                return new UENoneProperty();
            }

            var type = br.ReadUEString();
            var valueLength = br.ReadInt64();

            UEProperty prop = type switch
            {
                "IntProperty" => new UEIntProperty(),
                "ArrayProperty" => new UEArrayProperty(),
                _ => throw new Exception("Unrecognized property type!"),
            };

            prop.Name = name;
            prop.Type = type;

            switch (prop)
            {
                case UEIntProperty x:
                    {
                        var terminator = br.ReadByte();
                        if (terminator != 0)
                            throw new FormatException($"Offset: 0x{br.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

                        if (valueLength != sizeof(int))
                            throw new FormatException($"Expected int value of length {sizeof(int)}, but was {valueLength}");

                        x.Value = br.ReadInt32();
                    }
                    break;
                case UEArrayProperty x:
                    {
                        var itemType = br.ReadUEString();
                        var terminator = br.ReadByte();
                        if (terminator != 0)
                            throw new FormatException($"Offset: 0x{br.BaseStream.Position - 1:x8}. Expected terminator (0x00), but was (0x{terminator:x2})");

                        switch (itemType)
                        {
                            case "ByteProperty":
                                var itemLength = br.ReadInt32();
                                x.Value = br.ReadBytes(itemLength);
                                break;
                            default:
                                throw new Exception("Unrecognized property type!");
                        }
                    }
                    break;
            }

            return prop;
        }
    }
}
