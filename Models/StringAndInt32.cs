using System.IO;

namespace Save_Editor.Models
{
    public class StringAndInt32: NotifyPropertyChangedImpl 
    {
        public string str    { get; set; }
        public int value    { get; set; }
    }
    public static partial class Extensions {
        public static StringAndInt32 ReadStringAndInt32(this BinaryReader reader)
        {
            var stringAndInt32 = new StringAndInt32
            {
                str = reader.ReadString(),
                value = reader.ReadInt32(),
            };

            return stringAndInt32;
        }

        public static void Write(this BinaryWriter writer, StringAndInt32 stringAndInt32) {
            writer.Write(stringAndInt32.str);
            writer.Write(stringAndInt32.value);
        }
    }
}