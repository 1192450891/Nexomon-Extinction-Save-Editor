using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;

namespace Save_Editor.Models
{
    public class Struct4 : NotifyPropertyChangedImpl 
    {
        public int a1    { get; set; }
        public short a2    { get; set; }
        
        public List<short> a3    { get; set; }
    }
    
    public static partial class Extensions {
        public static Struct4 ReadStruct4(this BinaryReader reader)
        {
            var struct4 = new Struct4
            {
                a1 = reader.ReadInt32(),
                a2 = reader.ReadInt16(),
                a3 = new List<short>()
            };
            for (int i = 0; i < struct4.a2; i++)
            {
                struct4.a3.Add(reader.ReadInt16());
            }

            return struct4;
        }

        public static void Write(this BinaryWriter writer, Struct4 struct4) {
            writer.Write(struct4.a1);
            writer.Write(struct4.a2);
            foreach (var v in struct4.a3)
            {
                 writer.Write(v);
            }
        }
    }
}