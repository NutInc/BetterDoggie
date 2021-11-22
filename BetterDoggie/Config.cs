namespace BetterDoggie
{
    using UnityEngine;
    using System.ComponentModel;
    using Exiled.API.Interfaces;
    
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("The size the 939 will be sized to.")]
        public Vector3 DoggieScale { get; set; } = new Vector3(.85f, .85f, .85f);

        [Description("The speed boost that the dog should get. (Default is 7)")]
        public float DogSpeed { get; set; } = 9f;

        [Description("The base amount of damage 939 will do.")]
        public float BaseDamage { get; set; } = 40f;

        [Description("The maximum amount of additional damage 939 can deal.")]
        public float MaxDamageBoost { get; set; } = 150f;

        [Description("Message to send to players when they spawn as 939")]
        public string SpawnMessage { get; set; } =
            "<color=orange>You have spawned as an <color=red>upgraded</color> SCP-939! You run <color=red>faster</color> but slow down when you attack!</color>";

        [Description("Can 939 bust open doors and gates if it is below a certain AHP?")]
        public bool EnableDogDoorBusting { get; set; } = true;
        
        [Description("The dog has to have an AHP lower than this to bust doors.")]
        public int DoorBustAhp { get; set; } = 50;
    }
}