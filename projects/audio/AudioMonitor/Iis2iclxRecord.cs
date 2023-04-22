using System.Text;

namespace AudioMonitor
{
    public class Iis2IclxRecord
    {
        public int RecordType { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public int SequenceNum { get; set; }
        public float AccelX { get; set; }
        public float AccelY { get; set; }
        public float SampleRate { get; set; }
        public float Scale { get; set; }
        public override string ToString()
        {
            var sb = new StringBuilder();

            return sb.ToString();
        }
    }
}