namespace BetterDoggie
{
    using UnityEngine;
    using System.ComponentModel;
    using Exiled.API.Interfaces;
    using Exiled.API.Features;
    
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("The max HP that dog will spawn with.")]
        public int DoggieHealth { get; set; } = 1500;
        
        [Description("The maximum AHP the dog can spawn with.")]
        public int DoggieAhp { get; set; } = 600;

        [Description("Should the dog get the a speed boost? (Set to 0 or less to disable)")]
        public byte ColaSpeedBoost { get; set; } = 20;

        [Description("The duration the dog should get slowed down when attacking.")]
        public float SlowdownDuration { get; set; } = 3f;

        [Description("Should the slowdown time stack for each attack the dog does? (Add X seconds to slowdown versus just resetting it to X seconds)")]
        public bool ShouldSlowdownStack { get; set; } = true;

        [Description("The size of the dog when it spawns.")]
        public Vector3 DoggieScale { get; set; } = new Vector3(.85f, .85f, .85f);

        [Description("The base amount of damage the dog will do.")]
        public float BaseDamage { get; set; } = 40f;

        [Description("The maximum amount of additional damage the dog can deal.")]
        public float MaxDamageBoost { get; set; } = 150f;

        [Description("Message to send to players when they spawn as the dog.")]
        public Broadcast SpawnBroadcast { get; set; } = new Broadcast(
            "<color=orange>You have spawned as an <color=red>upgraded</color> SCP-939! You run <color=red>faster</color> but slow down when you attack! " +
            "You can also bust down doors and pry gates when your Hume shield is below 50!</color>", 8);
        
        [Description("Can 939 bust open doors and gates if it is below a certain AHP?")]
        public bool EnableDogDoorBusting { get; set; } = true;

        [Description("The dog has to have an AHP lower than this to bust doors.")]
        public int DoorBustAhp { get; set; } = 50;

        [Description("Gives 939 a speed boost when it busts down a door.")]
        public bool EnableBustSpeedBoost { get; set; } = true;

        [Description("The speed boost the dog gets when it busts down a door.")]
        public byte BustBoostAmount { get; set; } = 50;
    }
}