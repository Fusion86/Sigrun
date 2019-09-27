using Sigrun.Extensions;
using System.IO;

namespace Sigrun.Structs
{
    public class GvasHeader : ICanRead
    {
        public string Magic { get; private set; }
        public int SaveGameVersion { get; private set; }
        public int PackageVersion { get; private set; }
        public EngineVersion EngineVersion { get; private set; }

        public void Read(BinaryReader br)
        {
            Magic = br.ReadFixedString(4);
            SaveGameVersion = br.ReadInt32();
            PackageVersion = br.ReadInt32();
            EngineVersion = br.ReadObject<EngineVersion>();
        }
    }
}
