using System.Collections.Generic;
using System.IO;

namespace Save_Editor.Models
{
    public class Struct3 : NotifyPropertyChangedImpl 
    {
        public short a1    { get; set; }
        public short a3     { get; set; }
        public List<bool> a4 { get; set; } = new List<bool>();
    }
    
    public static partial class Extensions {
        public static Struct3 ReadStruct3(this BinaryReader reader)
        {
            var struct3 = new Struct3
            {
                a1 = reader.ReadInt16(),
                a3 = reader.ReadInt16(),
            };
            for (var i = struct3.a3; i > 0; i--)
            {
                struct3.a4.Add(reader.ReadBoolean());
            }

            return struct3;
        }

        public static void Write(this BinaryWriter writer, Struct3 struct3) {
            writer.Write(struct3.a1);
            writer.Write(struct3.a3);
            foreach (var a in struct3.a4)
            {
                writer.Write(a);
            }
        }
    }
}