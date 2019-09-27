using Sigrun.Extensions;
using System.IO;

namespace Sigrun
{
    public class CustomData : ICanRead
    {
        public int Version { get; private set; }
        public object[] Entries { get; private set; }

        public void Read(BinaryReader br)
        {
            Version = br.ReadInt32();
            var entryCount = br.ReadInt32();

            Entries = new object[entryCount];
            for (int i = 0; i < entryCount; i++)
                Entries[i] = br.ReadObject<CustomDataEntry>();
        }
    }
}
