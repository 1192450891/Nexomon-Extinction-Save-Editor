using System.IO;

namespace Save_Editor.Resources.Models
{
    public class Mineral
    {
        public short id    { get; set; }
        public short times    { get; set; }
        
        public short unknownData1    { get; set; }
        public bool unknownData2    { get; set; }
        
        public short unknownData3    { get; set; }
        public bool unknownData4    { get; set; }
    }
    
    public static partial class Extensions {
        public static Mineral ReadMineral(this BinaryReader reader)
        {
            var mineral = new Mineral
            {
                id = reader.ReadInt16(),
                times  = reader.ReadInt16()
            };
            for (int i = 0; i < mineral.times; i++)
            {
                mineral.unknownData1 = reader.ReadInt16();
                mineral.unknownData2 = reader.ReadBoolean();
                mineral.unknownData3 = reader.ReadInt16();
                mineral.unknownData4 = reader.ReadBoolean();
            }
            
            return mineral;
        }

        public static void Write(this BinaryWriter writer, Mineral mineral) {
            writer.Write(mineral.id);
            writer.Write(mineral.times);
            for (int i = 0; i < mineral.times; i++)
            {
                writer.Write(mineral.unknownData1);
                writer.Write(mineral.unknownData2);
                writer.Write(mineral.unknownData3);
                writer.Write(mineral.unknownData4);
            }
        }
    }
}