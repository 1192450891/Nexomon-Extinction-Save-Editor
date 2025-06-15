using System.IO;

namespace Save_Editor.Models
{
    public class RematchBeatenTamer : NotifyPropertyChangedImpl 
    {
        public string name    { get; set; }
        
        public int unknownData1 { get; set; }
        
        
    }
    
    public static partial class Extensions {
        public static RematchBeatenTamer ReadRematchBeatenTamers(this BinaryReader reader)
        {
            var rematchBeatenTamers = new RematchBeatenTamer
            {
                name = reader.ReadString(),
                unknownData1 = reader.ReadInt32()
            };

            return rematchBeatenTamers;
        }

        public static void Write(this BinaryWriter writer, RematchBeatenTamer rematchBeatenTamer) {
            writer.Write(rematchBeatenTamer.name);
            writer.Write(rematchBeatenTamer.unknownData1);
        }
    }
}