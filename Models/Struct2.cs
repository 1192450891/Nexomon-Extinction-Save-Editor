using System.IO;

namespace Save_Editor.Models
{
    public class Struct2 : NotifyPropertyChangedImpl 
    {
        public short id    { get; set; }
        public bool a2    { get; set; }
        public bool a3    { get; set; }
        
    }
    
    public static partial class Extensions {
        public static Struct2 ReadStruct2(this BinaryReader reader)
        {
            var struct2 = new Struct2
            {
                id = reader.ReadInt16(),
                a2 = reader.ReadBoolean(),
                a3 = reader.ReadBoolean()
            };

            return struct2;
        }

        public static void Write(this BinaryWriter writer, Struct2 struct2) {
            writer.Write(struct2.id);
            writer.Write(struct2.a2);
            writer.Write(struct2.a3);
        }
    }
}