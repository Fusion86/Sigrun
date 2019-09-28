using System;
using System.Diagnostics;
using System.IO;

namespace Sigrun.Serialization
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CustomDataEntry : ICanRead
    {
        public Guid Id { get; private set; }
        public int Value { get; private set; }

        [DebuggerHidden]
        private string DebuggerDisplay => $"{{{Id}: {Value}}}";

        public void Read(BinaryReader br)
        {
            Id = new Guid(br.ReadBytes(16));
            Value = br.ReadInt32();
        }
    }
}
