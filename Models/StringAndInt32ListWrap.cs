using System.Collections.Generic;
using System.IO;

namespace Save_Editor.Models
{
    public class StringAndInt32ListWrap
    {
        public readonly List<List<StringAndInt32>> Items =new List<List<StringAndInt32>>();
        
        public void Add(List<StringAndInt32> stringAndInt32List)
        {
            Items.Add(stringAndInt32List);
        }

        public int WriteIndex;

        public void Reset()
        {
            WriteIndex = 0;
        }

        public int GetValue(int i, int j)
        {
            while (Items.Count - 1 < i)
            {
                Items.Add(new List<StringAndInt32>());
            }
            while (Items[i].Count - 1 < j)
            {
                Items[i].Add(new StringAndInt32());
            }
            return Items[i][j].value;
        }
        public void SetValue(int i, int j, int newValue) => Items[i][j].value = newValue;
        
        public int totalCoinsAccumulated
        {
            get => GetValue(0, 0);
            set => SetValue(0, 0, value);
        }

        public int totalCoinsSpent
        {
            get => GetValue(0, 1);
            set => SetValue(0, 1, value);
        }

        public int totalBouldersBroken
        {
            get => GetValue(0, 2);
            set => SetValue(0, 2, value);
        }

        public int totalMealsTossedd
        {
            get => GetValue(0, 3);
            set => SetValue(0, 3, value);
        }

        public int totalShardsSold
        {
            get => GetValue(0, 4);
            set => SetValue(0, 4, value);
        }

        public int guessed_number
        {
            get => GetValue(1, 0);
            set => SetValue(1, 0, value);
        }

        public int defeated_thieves_frozen_tundra
        {
            get => GetValue(1, 1);
            set => SetValue(1, 1, value);
        }
    }
    public static partial class Extensions {
        public static List<StringAndInt32> ReadStringAndInt32List(this BinaryReader reader)
        {
            var stringAndInt32List = new List<StringAndInt32>();
            var readCount = reader.ReadInt32();
            for (int i = 0; i < readCount; i++)
            {
                stringAndInt32List.Add(reader.ReadStringAndInt32());
            }

            return stringAndInt32List;
        }

        public static void Write(this BinaryWriter writer, StringAndInt32ListWrap stringAndInt32ListWrap)
        {
            var stringAndInt32List = stringAndInt32ListWrap.Items[stringAndInt32ListWrap.WriteIndex];
            writer.Write(stringAndInt32List.Count);
            foreach (var stringAndInt32 in stringAndInt32List)
            {
                writer.Write(stringAndInt32);
            }

            stringAndInt32ListWrap.WriteIndex++;
        }
    }
}