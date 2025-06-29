using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Documents;

namespace Save_Editor.Models
{
    public class TombFire : NotifyPropertyChangedImpl 
    {
        public int id    { get; set; }
        public int fireStrCount    { get; set; }
        public List<string> fireStrList    { get; set; } = new List<string>();
        
    }
    
    public static partial class Extensions {
        public static TombFire ReadTombFire(this BinaryReader reader)
        {
            var tombFire = new TombFire
            {
                id = reader.ReadInt32(),
                fireStrCount = reader.ReadInt32()
            };

            for (int j = 0; j < tombFire.fireStrCount; j++)
            {
                tombFire.fireStrList.Add(reader.ReadString());
            }

            return tombFire;
        }

        public static void Write(this BinaryWriter writer, TombFire tombFire) {
            writer.Write(tombFire.id);
            writer.Write(tombFire.fireStrCount);
            foreach (var fireStr in tombFire.fireStrList)
            {
                writer.Write(fireStr);
            }
        }
    }
}