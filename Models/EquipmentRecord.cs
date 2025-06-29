using System.IO;

namespace Save_Editor.Models
{
    public class EquipmentRecord : NotifyPropertyChangedImpl 
    {
        public int id    { get; set; }
        public bool flag     { get; set; }
    }
    
    public static partial class Extensions {
        public static EquipmentRecord ReadStruct1(this BinaryReader reader)
        {
            var struct1 = new EquipmentRecord
            {
                id = reader.ReadInt32(),
                flag = reader.ReadBoolean()
            };

            return struct1;
        }

        public static void Write(this BinaryWriter writer, EquipmentRecord equipmentRecord) {
            writer.Write(equipmentRecord.id);
            writer.Write(equipmentRecord.flag);
        }
    }
}