namespace BetterDoggie
{
    using System;
    using Exiled.API.Features;
    using Exiled.API.Enums;
    
    using PlayerEvents = Exiled.Events.Handlers.Player; 
    using ServerEvents = Exiled.Events.Handlers.Server;
    
    public class BetterDoggie : Plugin<Config>
    {
        public static BetterDoggie Singleton;
        
        public override string Author => "Parkeymon";
        public override string Name => "BetterDoggie";
        public override string Prefix => "better_doggie";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(3, 0, 5);
        public override PluginPriority Priority => PluginPriority.Low;
        
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