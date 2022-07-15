namespace BetterDoggie
{
    using System;
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Exiled.API.Enums;
    using MEC;
    
    using PlayerEvents = Exiled.Events.Handlers.Player; 
    
    public class BetterDoggie : Plugin<Config>
    {
        public static BetterDoggie Singleton;
        
        public override string Author => "Parkeymon";
        public override string Name => "BetterDoggie";
        public override string Prefix => "better_doggie";
        public override Version Version => new Version(1, 3, 1);
        public override Version RequiredExiledVersion => new Version(5, 0, 0);
        public override PluginPriority Priority => PluginPriority.Low;

        public Dictionary<Player, CoroutineHandle?> ActiveAbilities = new Dictionary<Player, CoroutineHandle?>();
        public Dictionary<Player, int> AbilityCooldowns = new Dictionary<Player, int>();
        
        public override void OnEnabled()
        {
            Singleton = this;
            
            PlayerEvents.ChangingRole += EventHandlers.OnChangingRoles;
            PlayerEvents.Hurting += EventHandlers.OnHurtingPlayer;
            PlayerEvents.InteractingDoor += EventHandlers.OnInteractingDoor;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            PlayerEvents.ChangingRole -= EventHandlers.OnChangingRoles;
            PlayerEvents.Hurting -= EventHandlers.OnHurtingPlayer;
            PlayerEvents.InteractingDoor -= EventHandlers.OnInteractingDoor;

            Singleton = null;
            
            base.OnDisabled();
        }
    }
}