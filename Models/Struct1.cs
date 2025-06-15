using System.IO;

namespace Save_Editor.Models
{
    public class Struct1 : NotifyPropertyChangedImpl 
    {
        public short a1    { get; set; }
        public short a2    { get; set; }
        public bool a3     { get; set; }
        
        
        
    }
    
    public static partial class Extensions {
        public static Struct1 ReadStruct1(this BinaryReader reader)
        {
            var struct1 = new Struct1
            {
                a1 = reader.ReadInt16(),
                a2 = reader.ReadInt16(),
                a3 = reader.ReadBoolean()
            };

            return struct1;
        }

        public static void Write(this BinaryWriter writer, Struct1 struct1) {
            writer.Write(struct1.a1);
            writer.Write(struct1.a2);
            writer.Write(struct1.a3);
        }
    }
}