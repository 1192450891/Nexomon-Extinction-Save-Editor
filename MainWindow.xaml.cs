﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;
using Microsoft.Win32;
using Save_Editor.Models;
using Save_Editor.Models.Ps4;
using Save_Editor.Resources;

namespace Save_Editor {
    public partial class MainWindow : INotifyPropertyChanged {
        private const string SAVE_FILE_FILTER = "Windows|*.dat|Switch|slot-*|PS4|memory.dat";

        private Ps4Header                     ps4SaveData;
        public  Visibility                    Ps4SaveVisibility => ps4SaveData == null ? Visibility.Collapsed : Visibility.Visible; // Easier than doing a not-null converter check in binding.
        public  SlotPosition                  ps4ActiveSaveSlot { get; set; }
        public  Dictionary<SlotPosition, int> ps4SlotPositions  { get; private set; }
        private SaveData                      saveData;
        public  SaveData                      SaveData => ps4SaveData != null ? ps4SaveData.saveData[ps4SlotPositions[ps4ActiveSaveSlot]] : saveData;
        public  string                        targetFile;

        public MainWindow() {
            if (!LoadFile()) {
                Application.Current.Shutdown();
                return;
            }

            InitializeComponent();

            SetupAppWideBinding(new KeyGesture(Key.S, ModifierKeys.Control), SaveFile); // Ctrl+S.

            // SetupAppWideBinding(new KeyGesture(Key.D, ModifierKeys.Control), SetTo80); // Ctrl+D.
            // SetupAppWideBinding(new KeyGesture(Key.J, ModifierKeys.Control), SetCores); // Ctrl+J.
            // SetupAppWideBinding(new KeyGesture(Key.L, ModifierKeys.Control | ModifierKeys.Alt), Resort); // Ctrl+Alt+L.
            
            SetupAppWideBinding(new KeyGesture(Key.Q, ModifierKeys.Control), FullAllItems ); // Ctrl+Q.
            SetupAppWideBinding(new KeyGesture(Key.W, ModifierKeys.Control), AddTheBoxWithAllMonsters); // Ctrl+W.
            SetupAppWideBinding(new KeyGesture(Key.E, ModifierKeys.Control), BeatAllTamers); // Ctrl+E.
        }

        private bool LoadFile() {
            var target = GetOpenTarget();
            if (string.IsNullOrEmpty(target)) {
                Application.Current.Shutdown();
                return false;
            }

            targetFile = target;

            using var reader = new BinaryReader(File.Open(target, FileMode.Open, FileAccess.Read, FileShare.Read));

            bool isPs4Save;
            try {
                isPs4Save = new string(reader.ReadChars(8)).ToLower().Contains("fallen");
            } finally {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
            }

            if (isPs4Save) {
                ps4SaveData = reader.ReadPs4Header();

                ps4SlotPositions = new Dictionary<SlotPosition, int>();
                for (var i = 0; i < ps4SaveData.slotHeader.Length; i++) {
                    ps4SlotPositions[ps4SaveData.slotHeader[i].slotPosition] = i;
                }
                ps4SlotPositions  = ps4SlotPositions.OrderBy(pair => pair.Key.ToString()).ToDictionary(pair => pair.Key, pair => pair.Value);
                ps4ActiveSaveSlot = ps4SlotPositions.Keys.First();
            } else {
                saveData = reader.ReadSaveData();
            }

            return true;
        }

        private void SaveFile() {
            var target = GetSaveTarget();
            if (string.IsNullOrEmpty(target)) return;

            using var writer = new BinaryWriter(File.Open(target, FileMode.Create, FileAccess.Write, FileShare.Read));

            if (ps4SaveData != null) {
                writer.Write(ps4SaveData);
            } else {
                writer.Write(SaveData);
            }
        }

        private string GetOpenTarget() {
            var ofdResult = new OpenFileDialog {
                Filter           = SAVE_FILE_FILTER,
                Multiselect      = false,
                InitialDirectory = Path.GetDirectoryName(targetFile) ?? string.Empty
            };
            ofdResult.ShowDialog();

            return ofdResult.FileName;
        }

        private string GetSaveTarget() {
            var sfdResult = new SaveFileDialog {
                Filter           = SAVE_FILE_FILTER,
                FileName         = $"{Path.GetFileNameWithoutExtension(targetFile)}",
                AddExtension     = true,
                InitialDirectory = Path.GetDirectoryName(targetFile) ?? string.Empty
            };
            return sfdResult.ShowDialog() == true ? sfdResult.FileName : null;
        }

        private void SetupAppWideBinding(KeyGesture keyGesture, Action onPress) {
            // Setup App-wide Binding.
            var command = new RoutedCommand();
            var ib      = new InputBinding(command, keyGesture);
            InputBindings.Add(ib);
            // Bind handler.
            var cb = new CommandBinding(command);
            cb.Executed += (sender, args) => onPress.Invoke();
            CommandBindings.Add(cb);
        }

#if DEBUG
        private void SetTo80() {
            foreach (var item in SaveData.items) {
                if (item.Quantity >= 50 && item.Quantity < 80) item.Quantity = 80;
            }
        }

        private void SetCores() {
            foreach (var monster in SaveData.party) {
                foreach (var core in monster.cores) {
                    core.Id = ItemConst.Destruction_Core_III_;
                }

                var toAdd = 4 - monster.cores.Count;
                for (var i = 0; i < toAdd; i++) {
                    monster.cores.Add(ItemConst.Destruction_Core_III_);
                }

                monster.harmony = 100;
            }

            foreach (var box in SaveData.storage) {
                foreach (var monster in box.slots) {
                    if (monster == null) continue;
                    monster.harmony = 100;
                }
            }

            // Uncomment if you want to set the cores in storage too. Game doesn't normally allow this though.
            //foreach (var box in saveData.storage) {
            //    foreach (var monster in box.slots) {
            //        if (monster == null) continue;

            //        foreach (var core in monster.cores) {
            //            core.Id = 1161;
            //        }

            //        var toAdd = 4 - monster.cores.Count;
            //        for (var i = 0; i < toAdd; i++) {
            //            monster.cores.Add(1161);
            //        }
            //    }
            //}
        }

        private void Resort() {
            using var monsterLists = GetAllMonsters()
                                     .OrderBy(monster => monster.monsterId)
                                     .ChunkIntoToLists(Box.SIZE)
                                     .GetEnumerator();

            foreach (var box in SaveData.storage) {
                int remainder;

                // if we run out of monsters, we need to null the whole box.
                if (!monsterLists.MoveNext()) {
                    remainder = Box.SIZE;
                    goto nullTheRemainder;
                }

                var monsterList = monsterLists.Current;

                for (var m = 0; m < monsterList!.Count; m++) {
                    box.slots[m] = monsterList[m];
                }

                remainder = Box.SIZE - monsterList!.Count; // If there were 57 in the list, remainder is 3 (60 - 57).

                if (remainder == 0) continue;

                nullTheRemainder:
                for (var i = Box.SIZE - remainder; i < Box.SIZE; i++) { // Start at 57 and to to 60.
                    box.slots[i] = null;
                }

                box.OnPropertyChanged(nameof(box.slots));
            }
        }
#endif

        private void FullAllItems()
        {
            SaveData.items.Clear();
            foreach (var keyValuePair in Data.ITEM_NAMES_BY_ID)
            {
                var item = new InventoryItem
                {
                    Quantity = 999,
                    Id = keyValuePair.Key
                };
                SaveData.items.Add(item);
            }
        }

        private void AddTheBoxWithAllMonsters()
        {
            var boxSlotIndex = 0;
            var newBox = new Box
            {
                name = "All Monsters Box",
                slots = new Monster[600],
                size1 = 600
            };
            List<int> lastFourSkillIdsCache = new List<int>();
            for (int i = 1; i <= Data.MONSTER_NAMES_BY_ID.Count; i++)
            {
                var id = (short)i;
                var newMonster = new Monster
                {
                    monsterId = id,
                    nickname = Data.MONSTER_NAMES_BY_ID[id],
                    level = 99,
                    hp = 999,
                    sta = 999,
                    exp = 0,
                    useGridAdapter = false,
                    gridAdapter1 = null,
                    gridAdapter2 = 0,
                    gridAdapter3 = 0,
                    gridAdapter4 = false,
                    cosmic = true,
                    harmony = 100,
                };
                var monsterData = Data.MONSTERS_BY_ID[id];
                List<int> lastFourSkillIds;
                if (monsterData.skill_tree.Count != 0) //只有初始形态怪物有技能树数据
                {
                    lastFourSkillIds = monsterData.skill_tree.Values.TakeLast(4).ToList();
                    lastFourSkillIdsCache = lastFourSkillIds;
                }
                else
                {
                    lastFourSkillIds = lastFourSkillIdsCache;
                }
                foreach (var skillId in lastFourSkillIds)
                {
                    var skill = new Skill(skillId);
                    newMonster.skills.Add(skill);
                }
                newBox.slots[boxSlotIndex] = newMonster;
                boxSlotIndex++;
                newBox.OnPropertyChanged(nameof(newBox.slots));
            }
            SaveData.storage.Add(newBox);
        }

        private void BeatAllTamers()
        {
            SaveData.beatenTamers.Clear();
            SaveData.beatenTamerCount = Data.TAMERS_BY_ID.Count;
            foreach (var keyValuePair in Data.TAMERS_BY_ID)
            {
                var beatenTamer = new BeatenTamer()
                {
                    name = Data.TAMER_NAMES_BY_ID[keyValuePair.Key],
                    beatenTimes = 2,
                    unknownData1 = 0,
                    unknownData2 = 0
                };
                SaveData.beatenTamers.Add(beatenTamer);
            }
        }

        private IEnumerable<Monster> GetAllMonsters() {
            return from box in SaveData.storage
                   from monster in box.slots
                   where monster != null
                   select monster;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}