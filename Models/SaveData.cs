﻿using System;
using System.Collections.Generic;
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
        public          string                              saveDateUtc     { get; set; }
        public          string                              playerName      { get; set; }
        public          string                              playerBody      { get; set; }
        public          string                              petBody         { get; set; }
        public          int                                 playtimeSeconds { get; set; }
        public          int                                 mapId           { get; set; }
        public          int                                 playerX         { get; set; }
        public          int                                 playerY         { get; set; }
        public          string                              playerDirection { get; set; }
        public          int                                 checkpointMapId { get; set; }
        public          int                                 checkpointX     { get; set; }
        public          int                                 checkpointY     { get; set; }
        public          int                                 volumeBGM       { get; set; }
        public          int                                 volumeSFX       { get; set; }
        public          bool                                autoSaveEnabled { get; set; }
        public          string                              languageId      { get; set; }
        


        public int q1 { get; set; }


        public string total_coins_accumulated_Str { get; set; }
        public          int                                 totalCoinsAccumulated { get; set; }
        public string total_coins_spent_Str { get; set; }
        public          int                                 totalCoinsSpent  { get; set; }
        public string total_boulders_broken_Str { get; set; }
        public          int                                 totalBouldersBroken { get; set; }
        public string total_meals_tossedd_Str { get; set; }
        public          int                                 totalMealsTossedd { get; set; }
        public string total_shards_sold_Str { get; set; }
        public          int                                 totalShardsSold  { get; set; }
        
        public          ObservableCollection<Monster>       party           { get; } = new ObservableCollection<Monster>();
        public          ObservableCollection<Box>           storage         { get; } = new ObservableCollection<Box>();
        public          ObservableCollection<InventoryItem> items           { get; } = new ObservableCollection<InventoryItem>();
        public          Wallet                              wallet          { get; }
        public          int                                 beatenTamerCount{ get; set; }
        public         ObservableCollection<BeatenTamer>    beatenTamers    { get; } = new ObservableCollection<BeatenTamer>();
        
        public          int                                 miningCount     { get; set; }
        public         ObservableCollection<Mineral>        minerals       { get; } = new ObservableCollection<Mineral>();
        
        public readonly List<byte> remainderBytes;
        
        //public         Rematcher                       rematcher;
        //public         Achievements                    achievements;
        //public         PermanentlyDestroyedEntities    permanentlyKilledEntities;
        //private        Dictionary<int, bool>           switches;
        //private        Dictionary<int, List<string>>   permanentlyKilledFlags;
        //private        Dictionary<string, int>         variables;
        //public         HashSet<DatabaseMonsters.Entry> seenMonsters;
        //public         HashSet<DatabaseMonsters.Entry> ownedMonsters;
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
            q1 = reader.ReadInt32();
            total_coins_accumulated_Str = reader.ReadString();
            totalCoinsAccumulated = reader.ReadInt32();
            total_coins_spent_Str = reader.ReadString();
            totalCoinsSpent = reader.ReadInt32();
            total_boulders_broken_Str = reader.ReadString();
            totalBouldersBroken = reader.ReadInt32();
            total_meals_tossedd_Str = reader.ReadString();
            totalMealsTossedd = reader.ReadInt32();
            total_shards_sold_Str = reader.ReadString();
            totalShardsSold = reader.ReadInt32();

            
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

            writer.Write(saveData.q1);
            writer.Write(saveData.total_coins_accumulated_Str);
            writer.Write(saveData.totalCoinsAccumulated);
            writer.Write(saveData.total_coins_spent_Str);
            writer.Write(saveData.totalCoinsSpent);
            writer.Write(saveData.total_boulders_broken_Str);
            writer.Write(saveData.totalBouldersBroken);
            writer.Write(saveData.total_meals_tossedd_Str);
            writer.Write(saveData.totalMealsTossedd);
            writer.Write(saveData.total_shards_sold_Str);
            writer.Write(saveData.totalShardsSold);
            
            foreach (var b in saveData.remainderBytes) {
                writer.Write(b);
            }
        }
    }
}