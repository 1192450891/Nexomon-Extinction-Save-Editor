﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Save_Editor.Resources;
using Save_Editor.Resources.Models;

namespace Save_Editor {
    public static class Data {
        public static readonly Dictionary<int, string>        ITEM_NAMES_BY_ID;
        public static readonly Dictionary<short, MonsterData> MONSTERS_BY_ID      = new Dictionary<short, MonsterData>();
        public static readonly Dictionary<short, string>      MONSTER_NAMES_BY_ID = new Dictionary<short, string>();
        public static readonly Dictionary<int, string>        SKILL_NAMES_BY_ID   = new Dictionary<int, string>();
        public static readonly List<string>                   AVATARS;
        public static readonly List<string>                   PET_AVATARS;
        public static readonly List<string>                   LANGUAGES;
        public static readonly Dictionary<short, TamerData>   TAMERS_BY_ID        = new Dictionary<short, TamerData>();
        public static readonly Dictionary<short, string>      TAMER_NAMES_BY_ID   = new Dictionary<short, string>();

        private static readonly TextInfo TEXT_INFO = new CultureInfo("en-US", false).TextInfo;

        static Data() {
            var itemsByName = JsonConvert.DeserializeObject<Dictionary<string, ItemData>>(Encoding.UTF8.GetString(Assets.items));

            var itemNameFiles = ItemNames.ResourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            var itemKeyToName = new Dictionary<string, string>();
            foreach (DictionaryEntry itemNameFile in itemNameFiles!) {
                var key  = (string) itemNameFile.Key;
                var name = new StringReader(((string) itemNameFile.Value)!).ReadLine();
                itemKeyToName[key] = name;
            }

            ITEM_NAMES_BY_ID = itemsByName.ToDictionary(pair => pair.Value.itemId, pair => itemKeyToName.TryGet(pair.Key.Replace('-', '_'), pair.Key));

            var monstersByName = JsonConvert.DeserializeObject<Dictionary<string, MonsterData>>(Encoding.UTF8.GetString(Assets.monsters));

            var monsterNames = Assets.monster_names.Split("\n");

            for (short i = 0; i < monstersByName.Count; i++) {
                MONSTERS_BY_ID[(short) (i + 1)]      = monstersByName.ElementAt(i).Value;
                MONSTER_NAMES_BY_ID[(short) (i + 1)] = TEXT_INFO.ToTitleCase(monsterNames[i]);
            }

            var skillNames = Assets.skill_names.Split("\n");

            for (var i = 0; i < skillNames.Length; i++) {
                SKILL_NAMES_BY_ID[i + 1] = skillNames[i];
            }

            AVATARS     = JsonConvert.DeserializeObject<List<string>>(Encoding.UTF8.GetString(Assets.avatars));
            PET_AVATARS = JsonConvert.DeserializeObject<List<string>>(Encoding.UTF8.GetString(Assets.pets));
            PET_AVATARS.Insert(0, "");
            LANGUAGES = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(Assets.languages)).Keys.ToList();
            
            var tempTamersDict = JsonConvert.DeserializeObject<Dictionary<string, BaseTamerData>>(Encoding.UTF8.GetString(Assets.tamers), new TamerDataConverter());
            TAMERS_BY_ID = new Dictionary<short, TamerData>();
            TAMER_NAMES_BY_ID = new Dictionary<short, string>();
            short idCounter = 0;
            foreach (var kvp in tempTamersDict)
            {
                TAMERS_BY_ID[idCounter] = kvp.Value as TamerData;
                TAMER_NAMES_BY_ID[idCounter] = kvp.Key;
                idCounter++;
            }
        }
    }
}