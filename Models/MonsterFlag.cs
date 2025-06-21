using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;

namespace Save_Editor.Models
{
    public class MonsterFlag : NotifyPropertyChangedImpl 
    {
        public byte id    { get; set; }
        public bool flag    { get; set; }
    }
    
    public static partial class Extensions {
        public static MonsterFlag ReadStruct6(this BinaryReader reader)
        {
            var struct6 = new MonsterFlag
            {
                id = reader.ReadByte(),
                flag = reader.ReadBoolean(),
            };
            return struct6;
        }

        public static void Write(this BinaryWriter writer, MonsterFlag monsterFlag) {
            writer.Write(monsterFlag.id);
            writer.Write(monsterFlag.flag);
        }
    }
}