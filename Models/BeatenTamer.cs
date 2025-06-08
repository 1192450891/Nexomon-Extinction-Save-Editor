using System.IO;

namespace Save_Editor.Models
{
    public class BeatenTamer : NotifyPropertyChangedImpl 
    {
        public string name    { get; set; }
        
        public int beatenTimes    { get; set; }
        
        public short unknownData1 { get; set; }
        
        public short unknownData2   { get; set; }
        
    }
    
    public static partial class Extensions {
        public static BeatenTamer ReadBeatenTamer(this BinaryReader reader)
        {
            var beatenTamer = new BeatenTamer
            {
                name = reader.ReadString(),
                beatenTimes = reader.ReadInt32(),
                unknownData1 = reader.ReadInt16(),
                unknownData2 = reader.ReadInt16()
            };

            return beatenTamer;
        }

        public static void Write(this BinaryWriter writer, BeatenTamer beatenTamer) {
            writer.Write(beatenTamer.name);
            writer.Write(beatenTamer.beatenTimes);
            writer.Write(beatenTamer.unknownData1);
            writer.Write(beatenTamer.unknownData2);
        }
    }
}