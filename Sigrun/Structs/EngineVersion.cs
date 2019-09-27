using Sigrun.Extensions;
using System.IO;
using System.Runtime.InteropServices;

namespace Sigrun.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class EngineVersion: ICanRead
    {
        public short Major { get; private set; }
        public short Minor { get; private set; }
        public short Patch { get; private set; }
        public int Build { get; private set; }
        public string BuildId { get; private set; }

        public void Read(BinaryReader br)
        {
            Major = br.ReadInt16();
            Minor = br.ReadInt16();
            Patch = br.ReadInt16();
            Build = br.ReadInt32();
            BuildId = br.ReadUEString();
        }
    }
}
