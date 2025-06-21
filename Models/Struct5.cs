using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;

namespace Save_Editor.Models
{
    public class Struct5 : NotifyPropertyChangedImpl 
    {
        public int a1    { get; set; }
        public int a2    { get; set; }
        public int a3    { get; set; }
        public string a4    { get; set; }
        public string a5    { get; set; }
        public int a6    { get; set; }
        public int a7    { get; set; }
        public string a8    { get; set; }
        public int a9    { get; set; }
        public int a10    { get; set; }
        public string a11    { get; set; }
    }
    
    public static partial class Extensions {
        public static Struct5 ReadStruct5(this BinaryReader reader)
        {
            var struct5 = new Struct5
            {
                a1 = reader.ReadInt32(),
                a2 = reader.ReadInt32(),
                a3 = reader.ReadInt32(),
                a4 = reader.ReadString(),
                a5 = reader.ReadString(),
                a6 = reader.ReadInt32(),
                a7 = reader.ReadInt32(),
                a8 = reader.ReadString(),
                a9 = reader.ReadInt32(),
                a10 = reader.ReadInt32(),
                a11 = reader.ReadString(),
            };
            return struct5;
        }

        public static void Write(this BinaryWriter writer, Struct5 struct5) {
            writer.Write(struct5.a1);
            writer.Write(struct5.a2);
            writer.Write(struct5.a3);
            writer.Write(struct5.a4);
            writer.Write(struct5.a5);
            writer.Write(struct5.a6);
            writer.Write(struct5.a7);
            writer.Write(struct5.a8);
            writer.Write(struct5.a9);
            writer.Write(struct5.a10);
            writer.Write(struct5.a11);
        }
    }
}