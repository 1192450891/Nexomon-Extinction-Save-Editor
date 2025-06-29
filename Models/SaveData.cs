using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Save_Editor.Resources.Models;

namespace Save_Editor.Models {
    public class SaveData : NotifyPropertyChangedImpl {
        public const int MIN_VERSION_WITH_AUTO_SAVE       = 1;
        public const int MIN_VERSION_WITH_HARMONY         = 12;
        public const int MIN_VERSION_WITH_COSMIC          = 15;
        public const int MIN_VERSION_WITH_EQUIPMENT       = 23;
        public const int MIN_VERSION_WITH_BEATEN_MONSTERS = 30;
        public const int MIN_VERSION_WITH_CUSTOM          = 31;
        public const int MIN_VERSION_WITH_LURING          = 39;

        public readonly char[]                              prefix;
        public readonly int                                 saveVersion;
        public          string                              buildDate       { get; }
        public          string                              buildTarget     { get; }
        public          string                              buildVersion    { get; }
        public          string                              saveDateUtc     { get; }
        public          string                              playerName      { get; set; }
        public          string                              playerBody      { get; set; }
        public          string                              petBody         { get; set; }
        public          int                                 playtimeSeconds { get; set; }
        public          int                                 mapId           { get; set; }
        public          int                                 playerX         { get; set; }
        public          int                                 playerY         { get; set; }
        public          string                              playerDirection { get; }
        public          int                                 checkpointMapId { get; set; }
        public          int                                 checkpointX     { get; set; }
        public          int                                 checkpointY     { get; set; }
        public          int                                 volumeBGM       { get; set; }
        public          int                                 volumeSFX       { get; set; }
        public          bool                                autoSaveEnabled { get; set; }
        public          string                              languageId      { get; set; }
        public          ObservableCollection<Monster>       party           { get; } = new ObservableCollection<Monster>();
        public          ObservableCollection<Box>           storage         { get; } = new ObservableCollection<Box>();
        public          ObservableCollection<InventoryItem> items           { get; } = new ObservableCollection<InventoryItem>();
        public          Wallet                              wallet          { get; }
        public          int                                 beatenTamerCount{ get; set; }
        public         ObservableCollection<BeatenTamer>    beatenTamers    { get; } = new ObservableCollection<BeatenTamer>();
        
        public          int                                 miningCount     { get; }
        public         ObservableCollection<Mineral>        minerals        { get; } = new ObservableCollection<Mineral>();
        
        public          int                                 rematcherCount   { get; }
        public         ObservableCollection<RematchBeatenTamer>   rematchBeatenTamers  { get; } = new ObservableCollection<RematchBeatenTamer>();
        public          StringAndInt32ListWrap              stringAndInt32ListWrap      { get; } = new StringAndInt32ListWrap();
        public          int                                 achievementCount            { get; set; }
        public          List<int>                           achievementIdList           { get; } = new List<int>();
        public          int                                 equipmentRecordCount        { get; }
        public          List<EquipmentRecord>               equipmentRecordList         { get; }
        public          int                                 struct2Count                { get; }
        public          List<Struct2>                       struct2List                 { get; }
        public          Struct3                             struct3                     { get; }
        public          int                                 completedMissionsCount { get; set; }
        public         ObservableCollection<CompletedMission>   completedMissionList    { get; } = new ObservableCollection<CompletedMission>();
        public          int                                 struct4Count                { get; }
        public          List<Struct4>                       struct4List                 { get; } = new List<Struct4>();
        public          int                                 tombFireCount               { get; }
        public          List<TombFire>                      tombFireList                    { get; } = new List<TombFire>();
        public          int                                 seenMonstersCount           { get; set; }
        public          List<short>                         seenMonsterIdList            { get; } = new List<short>();
        public          int                                 ownedMonstersCount          { get; set; }
        public          List<short>                         ownedMonsterIdList            { get; } = new List<short>();
        
        public readonly List<byte> remainderBytes;
        
        //public         PermanentlyDestroyedEntities    permanentlyKilledEntities;
        //private        Dictionary<int, bool>           switches;
        //private        Dictionary<int, List<string>>   permanentlyKilledFlags;
        //private        Dictionary<string, int>         variables;
        //private static List<int>                       cadiumMapsWithZieglerMiasma;

        public SaveData(BinaryReader reader, bool readPs4PrefixBytes, uint? saveSize) {
            var startPosition = reader.BaseStream.Position;

            if (readPs4PrefixBytes) {
                reader.BaseStream.Seek(-8);
                prefix = reader.ReadChars(8);
            }

            saveVersion     = reader.ReadInt32();
            buildDate       = reader.ReadString();
            buildTarget     = reader.ReadString();
            buildVersion    = reader.ReadString();
            saveDateUtc     = reader.ReadString();
            playerName      = reader.ReadString();
            playerBody      = reader.ReadString();
            playtimeSeconds = reader.ReadInt32();
            petBody         = reader.ReadString();
            mapId           = reader.ReadInt32();
            playerX         = reader.ReadInt32();
            playerY         = reader.ReadInt32();
            playerDirection = reader.ReadString();
            checkpointMapId = reader.ReadInt32();
            checkpointX     = reader.ReadInt32();
            checkpointY     = reader.ReadInt32();
            volumeBGM       = reader.ReadInt32();
            volumeSFX       = reader.ReadInt32();
            if (saveVersion >= MIN_VERSION_WITH_AUTO_SAVE) autoSaveEnabled = reader.ReadBoolean();
            languageId = reader.ReadString();

            for (var i = reader.ReadInt32(); i > 0; i--) {
                party.Add(reader.ReadMonster(saveVersion));
            }

            for (var i = reader.ReadInt32(); i > 0; i--) {
                storage.Add(reader.ReadBox(saveVersion));
            }

            for (var i = reader.ReadInt32(); i > 0; i--) {
                items.Add(reader.ReadItem());
            }

            wallet = reader.ReadWallet();

            beatenTamerCount = reader.ReadInt32();
            for (var i = beatenTamerCount; i > 0; i--) {
                beatenTamers.Add(reader.ReadBeatenTamer());
            }
            
            miningCount = reader.ReadInt32();
            for (var i = miningCount; i > 0; i--) {
                minerals.Add(reader.ReadMineral());
            }
            
            rematcherCount = reader.ReadInt32();
            for (var i = rematcherCount; i > 0; i--) {
                rematchBeatenTamers.Add(reader.ReadRematchBeatenTamers());
            }
            
            stringAndInt32ListWrap.Add(reader.ReadStringAndInt32List());
            
            achievementCount = reader.ReadInt32();
            for (var i = achievementCount; i > 0; i--) {
                achievementIdList.Add(reader.ReadInt32());
            }
            
            equipmentRecordCount = reader.ReadInt32();
            equipmentRecordList = new List<EquipmentRecord>();
            for (var i = equipmentRecordCount; i > 0; i--)
            {
                equipmentRecordList.Add(reader.ReadStruct1());
            }
            
            struct2Count = reader.ReadInt32();
            struct2List = new List<Struct2>();
            for (var i = struct2Count; i > 0; i--)
            {
                struct2List.Add(reader.ReadStruct2());
            }

            struct3 = reader.ReadStruct3();
            
            completedMissionsCount = reader.ReadInt32();
            for (var i = completedMissionsCount; i > 0; i--) {
                completedMissionList.Add(reader.ReadCompletedMission());
            }
            // completedMissionList.Sort(w=>w.id);

            stringAndInt32ListWrap.Add(reader.ReadStringAndInt32List());
            
            struct4Count = reader.ReadInt32();
            for (var i = struct4Count; i > 0; i--) {
                struct4List.Add(reader.ReadStruct4());
            }

            tombFireCount = reader.ReadInt32();
            for (var i = tombFireCount; i > 0; i--) {
                tombFireList.Add(reader.ReadTombFire());
            }
            
            seenMonstersCount = reader.ReadInt32();
            for (var i = seenMonstersCount; i > 0; i--)
            {
                seenMonsterIdList.Add(reader.ReadInt16());
            }
            ownedMonstersCount = reader.ReadInt32();
            for (var i = ownedMonstersCount; i > 0; i--)
            {
                ownedMonsterIdList.Add(reader.ReadInt16());
            }
            
            // We don't need to save the saveSize since the remainder has what we need to write.
            if (saveSize == null) {
                remainderBytes = reader.ReadRemainderAsByteArray();
            } else {
                var currentPosition = reader.BaseStream.Position;
                var remainder       = saveSize - (currentPosition - startPosition);

                if (remainder < 0) throw new InvalidOperationException("Save exceeded it's size.");
                if (remainder > int.MaxValue) throw new InvalidOperationException("Remainder too large to read in one pass."); // Very improbable.
                if (remainder > 0) {
                    remainderBytes = reader.ReadBytes((int) remainder).ToList();
                }
            }
        }

    }

    public static partial class Extensions {
        public static SaveData ReadSaveData(this BinaryReader reader, bool readPs4PrefixBytes = false, uint? saveSize = null) {
            return new SaveData(reader, readPs4PrefixBytes, saveSize);
        }

        public static void Write(this BinaryWriter writer, SaveData saveData) {
            if (saveData.prefix != null) {
                writer.Write(saveData.prefix);
            }

            writer.Write(saveData.saveVersion);
            writer.Write(saveData.buildDate);
            writer.Write(saveData.buildTarget);
            writer.Write(saveData.buildVersion);
            writer.Write(saveData.saveDateUtc);
            writer.Write(saveData.playerName);
            writer.Write(saveData.playerBody);
            writer.Write(saveData.playtimeSeconds);
            writer.Write(saveData.petBody);
            writer.Write(saveData.mapId);
            writer.Write(saveData.playerX);
            writer.Write(saveData.playerY);
            writer.Write(saveData.playerDirection);
            writer.Write(saveData.checkpointMapId);
            writer.Write(saveData.checkpointX);
            writer.Write(saveData.checkpointY);
            writer.Write(saveData.volumeBGM);
            writer.Write(saveData.volumeSFX);
            if (saveData.saveVersion > SaveData.MIN_VERSION_WITH_AUTO_SAVE) writer.Write(saveData.autoSaveEnabled);
            writer.Write(saveData.languageId);

            writer.Write(saveData.party.Count);
            foreach (var monster in saveData.party) {
                writer.Write(monster, saveData.saveVersion);
            }

            writer.Write(saveData.storage.Count);
            foreach (var box in saveData.storage) {
                writer.Write(box, saveData.saveVersion);
            }

            writer.Write(saveData.items.Count);
            foreach (var item in saveData.items) {
                writer.Write(item);
            }

            writer.Write(saveData.wallet);

            writer.Write(saveData.beatenTamerCount);
            foreach (var beatenTamer in saveData.beatenTamers) {
                writer.Write(beatenTamer);
            }
            
            writer.Write(saveData.miningCount);
            foreach (var mineral in saveData.minerals) {
                writer.Write(mineral);
            }
            
            writer.Write(saveData.rematcherCount);
            foreach (var rematchBeatenTamer in saveData.rematchBeatenTamers) {
                writer.Write(rematchBeatenTamer);
            }
            
            saveData.stringAndInt32ListWrap.Reset();
            writer.Write(saveData.stringAndInt32ListWrap);
            
            writer.Write(saveData.achievementCount);
            foreach (var achievementId in saveData.achievementIdList) {
                writer.Write(achievementId);
            }
            
            writer.Write(saveData.equipmentRecordCount);
            foreach (var struct1 in saveData.equipmentRecordList) {
                writer.Write(struct1);
            }
            
            writer.Write(saveData.struct2Count);
            foreach (var struct2 in saveData.struct2List) {
                writer.Write(struct2);
            }
            
            writer.Write(saveData.struct3);
            
            writer.Write(saveData.completedMissionsCount);
            foreach (var completedMission in saveData.completedMissionList) {
                writer.Write(completedMission);
            }

            writer.Write(saveData.stringAndInt32ListWrap);

            writer.Write(saveData.struct4Count);
            foreach (var struct4 in saveData.struct4List) {
                writer.Write(struct4);
            }
            writer.Write(saveData.tombFireCount);
            foreach (var tombFire in saveData.tombFireList) {
                writer.Write(tombFire);
            }
            
            writer.Write(saveData.seenMonstersCount);
            for (var i = 0; i < saveData.seenMonsterIdList.Count && i < saveData.seenMonstersCount; i++)
            {
                var seenMonsterId = saveData.seenMonsterIdList[i];
                writer.Write(seenMonsterId);
            }

            writer.Write(saveData.ownedMonstersCount);
            for (var i = 0; i < saveData.ownedMonsterIdList.Count && i < saveData.ownedMonstersCount; i++)
            {
                var ownedMonsterId = saveData.ownedMonsterIdList[i];
                writer.Write(ownedMonsterId);
            }

            foreach (var b in saveData.remainderBytes) {
                writer.Write(b);
            }
        }
    }
}