using System.IO;

namespace Save_Editor.Resources.Models
{
    public class Mineral
    {
        public short id    { get; set; }
        
        public bool isDigged    { get; set; }
        
        public short type    { get; set; }
        
        public short unknownData1    { get; set; }
        public bool unknownData2    { get; set; }
    }
    
    public static partial class Extensions {
        public static Mineral ReadMineral(this BinaryReader reader)
        {
            var mineral = new Mineral
            {
                id = reader.ReadInt16(),
                isDigged = reader.ReadBoolean()
            };
            if (mineral.isDigged)
            {
                mineral.type = reader.ReadInt16();
                mineral.unknownData1 = reader.ReadInt16();
            }
            mineral.unknownData2 = reader.ReadBoolean();

            return mineral;
        }

        public static void Write(this BinaryWriter writer, Mineral mineral) {
            writer.Write(mineral.id);
            writer.Write(mineral.isDigged);
            if (mineral.isDigged)
            {
                writer.Write(mineral.type);
                writer.Write(mineral.unknownData1);
            }
            writer.Write(mineral.unknownData2);
        }
    }
}