using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace K_mod
{
    [JsonObject(MemberSerialization.OptIn)]
    public class KmodSave
    {
        [JsonProperty]
        public static Dictionary<Actor, float> Rider_z = new();
        [JsonProperty]
        public static Dictionary<Actor, float> Rider_x = new();
        [JsonProperty]
        public static Dictionary<Actor, Actor> Rider_horse = new();
        [JsonProperty]
        public static Dictionary<Actor, Actor> Horse_rider = new();
        [JsonProperty]
        public static List<Actor> Rider = new();
        [JsonProperty]
        public static List<Actor> Horse = new();

    }
    public class KActionSave
    {
        public Guid Id { get; set; }
        [JsonProperty]
        public string a = "null";
        [JsonProperty]
        public string id = "null";
        [JsonProperty]
        public bool destroy = false;
        [JsonProperty]
        public bool animation = false;
        [JsonProperty]
        public float interval = 100f;
        [JsonProperty]
        public float intervals = 100f;
        [JsonProperty]
        public bool paused_ = true;
        [JsonProperty]
        public bool for_ = false;
        [JsonProperty]
        public float time = -1f;
        [JsonProperty]
        public List<string> statsId = new();
        [JsonProperty]
        public List<float> statsValue = new();
        [JsonProperty]
        public List<string> textures = new();
    }
}