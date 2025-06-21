using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;

namespace Save_Editor.Models
{
    public class MonsterId : NotifyPropertyChangedImpl 
    {
        public short id    { get; set; }
    }
    
    public static partial class Extensions {
        public static MonsterId ReadMonsterFlag(this BinaryReader reader)
        {
            var monsterFlag = new MonsterId
            {
                id = reader.ReadInt16()
            };
            return monsterFlag;
        }

        public static void Write(this BinaryWriter writer, MonsterId monsterId) {
            writer.Write(monsterId.id);
        }
    }
}