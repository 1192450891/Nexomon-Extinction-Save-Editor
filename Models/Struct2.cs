using System.IO;

namespace Save_Editor.Models
{
    public class Struct2 : NotifyPropertyChangedImpl 
    {
        public byte a1    { get; set; }
        public bool a2    { get; set; }
        public byte a3     { get; set; }
        public bool a4    { get; set; }
        
        
        
    }
    
    public static partial class Extensions {
        public static Struct2 ReadStruct2(this BinaryReader reader)
        {
            var Struct2 = new Struct2
            {
                a1 = reader.ReadByte(),
                a2 = reader.ReadBoolean(),
                a3 = reader.ReadByte(),
                a4 = reader.ReadBoolean(),
            };

            return Struct2;
        }

        public static void Write(this BinaryWriter writer, Struct2 Struct2) {
            writer.Write(Struct2.a1);
            writer.Write(Struct2.a2);
            writer.Write(Struct2.a3);
        }
    }
}