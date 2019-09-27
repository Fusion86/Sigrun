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
    }
}
