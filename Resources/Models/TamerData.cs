using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Save_Editor.Resources.Models
{
    public class BaseTamerData
    {
        public int @switch;
        public string comment;
        public string folder;
        
        // [JsonExtensionData]
        // public IDictionary<string, JToken> AdditionalData { get; set; }
    }
    
    public class TamerData : BaseTamerData
    {
        [JsonProperty("party-early")]
        public List<TamerPartyMonsterData> party_early = new List<TamerPartyMonsterData>();
    
        [JsonProperty("party-regular")]
        public List<TamerPartyMonsterData> party_regular = new List<TamerPartyMonsterData>();
    
        [JsonProperty("party-late")]
        public List<TamerPartyMonsterData> party_late = new List<TamerPartyMonsterData>();
    }

    public class SpecialBattleData : BaseTamerData
    {
        public List<TamerPartyMonsterData> party = new List<TamerPartyMonsterData>();
        public string bgm;
        public bool allowed_escape;
        public bool allowed_items;
        public bool allowed_party;
        public bool allowed_capture;
        public bool allowed_defeat;
        public bool auto_heal_on_exit_if_initially_alive;
        public bool wild;
        public bool instant;
        public string scene;
        public bool[] use_map_difficulty;
        public double hp_percentage_modifier;
        public double sta_percentage_modifier;
        public int[] special_bosses;
    }
    
    public class TamerPartyMonsterData
    {
        public string                    name;
        public int                       difficulty;
        public List<int>                 skills;
        public List<string>                 cores;
    }
    
    public class TamerDataConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(BaseTamerData);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
        
            if (jo.ContainsKey("party-early") || jo.ContainsKey("party-regular") || jo.ContainsKey("party-late"))
                return jo.ToObject<TamerData>(serializer);
        
            if (jo.ContainsKey("party"))
                return jo.ToObject<SpecialBattleData>(serializer);
        
            throw new JsonException("Unknown tamer data type");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}