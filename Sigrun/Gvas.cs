using Sigrun.Extensions;
using Sigrun.Structs;
using System.IO;
using System.Text;

namespace Sigrun
{
    public class Gvas : ICanRead
    {
        public GvasHeader Header { get; private set; }
        public CustomData CustomData { get; private set; }
        public string SaveGameType { get; private set; }

        public static Gvas Load(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (BinaryReader br = new BinaryReader(fs, Encoding.UTF8))
            {
                return br.ReadObject<Gvas>();
            }
        }

        public void Read(BinaryReader br)
        {
            Header = br.ReadObject<GvasHeader>();
            CustomData = br.ReadObject<CustomData>();
            SaveGameType = br.ReadUEString();
            // TODO: UEProperties
        }
    }
}
