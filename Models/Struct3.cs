using System.Collections.Generic;
using System.IO;

namespace Save_Editor.Models
{
    public class Struct3 : NotifyPropertyChangedImpl 
    {
        public short a1    { get; set; }
        public short a2     { get; set; }
        public int a3     { get; set; }
        public int a4    { get; set; }
        public int a5    { get; set; }
        public int a6    { get; set; }
        public bool a7    { get; set; }
        
    }
    
    public static partial class Extensions {
        public static Struct3 ReadStruct3(this BinaryReader reader)
        {
            var struct3 = new Struct3
            {
                a1 = reader.ReadInt16(),
                a2 = reader.ReadInt16(),
                a3 = reader.ReadInt32(),
                a4 = reader.ReadInt32(),
                a5 = reader.ReadInt32(),
                a6 = reader.ReadInt32(),
                a7 = reader.ReadBoolean(),
            };
            return struct3;
        }

        public static void Write(this BinaryWriter writer, Struct3 struct3) {
            writer.Write(struct3.a1);
            writer.Write(struct3.a2);
            writer.Write(struct3.a3);
            writer.Write(struct3.a4);
            writer.Write(struct3.a5);
            writer.Write(struct3.a6);
            writer.Write(struct3.a7);
        }
    }
}