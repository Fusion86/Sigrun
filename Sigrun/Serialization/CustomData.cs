using Sigrun.Extensions;
using System.Collections.Generic;
using System.IO;

namespace Sigrun.Serialization
{
    public class CustomData : ICanRead
    {
        public int Version { get; private set; }
        public List<CustomDataEntry> Entries { get; private set; }

        public void Read(BinaryReader br)
        {
            Version = br.ReadInt32();
            var entryCount = br.ReadInt32();

            Entries = new List<CustomDataEntry>(entryCount);
            for (int i = 0; i < entryCount; i++)
                Entries.Add(br.ReadObject<CustomDataEntry>());
        }
    }
}
