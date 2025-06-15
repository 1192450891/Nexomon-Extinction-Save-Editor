using System.IO;

namespace Save_Editor.Models
{
    public class CompletedMission : NotifyPropertyChangedImpl 
    {
        public short id    { get; set; }
        public bool isDone    { get; set; }
    }
    public static partial class Extensions {
        public static CompletedMission ReadCompletedMission(this BinaryReader reader)
        {
            var completedMission = new CompletedMission
            {
                id = reader.ReadInt16(),
                isDone = reader.ReadBoolean()
            };

            return completedMission;
        }

        public static void Write(this BinaryWriter writer, CompletedMission completedMission) {
            writer.Write(completedMission.id);
            writer.Write(completedMission.isDone);
        }
    }
}