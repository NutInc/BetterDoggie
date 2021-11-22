namespace BetterDoggie
{
    using Exiled.API.Extensions;
    using Exiled.Events.EventArgs;
    using CustomPlayerEffects;
    using MEC;
    using System;
    using Interactables.Interobjects;
    using Interactables.Interobjects.DoorUtils;

    public static class EventHandlers
    {
        public static void OnChangingRoles(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole != RoleType.Scp93953 && ev.NewRole != RoleType.Scp93989)
                return;

            Timing.CallDelayed(2f, () =>
            {
                ev.Player.Broadcast(8, BetterDoggie.Singleton.Config.SpawnMessage);
                
                ev.Player.Scale = BetterDoggie.Singleton.Config.DoggieScale;
                ev.Player.ChangeWalkingSpeed(BetterDoggie.Singleton.Config.DogSpeed);
                // 2 is the default difference in walk and run speed
                ev.Player.ChangeRunningSpeed(BetterDoggie.Singleton.Config.DogSpeed + 2);
            });
        }

        public static void OnHurtingPlayer(HurtingEventArgs ev)
        {
            if (ev.Attacker.Role != RoleType.Scp93953 && ev.Attacker.Role != RoleType.Scp93989)
                return;

            if (ev.DamageType == DamageTypes.Scp207)
            {
                ev.IsAllowed = false;
                return;
            }
                
            
            // 600 Is the maximum hume shield of 939
            ev.Amount = BetterDoggie.Singleton.Config.BaseDamage + Math.Abs(ev.Attacker.ArtificialHealth - 600) / 600  * BetterDoggie.Singleton.Config.MaxDamageBoost;

            ev.Attacker.EnableEffect<SinkHole>(3f, true);
        }

        public static void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (!BetterDoggie.Singleton.Config.EnableDogDoorBusting)
                return;
            
            if (ev.Player.Role != RoleType.Scp93953 && ev.Player.Role != RoleType.Scp93989)
                return;

            if (ev.Player.ArtificialHealth <= BetterDoggie.Singleton.Config.DoorBustAhp)
                BustDoor(ev.Door.Base, ev.Player);
        }
        
        private static void BustDoor(DoorVariant door, Exiled.API.Features.Player ply)
        {
            if (door is IDamageableDoor damage)
                damage.IsDestroyed = true;

            if (door is PryableDoor pryableDoor)
                pryableDoor.TryPryGate();
            
            ply.EnableEffect<Scp207>(2, true);
            ply.ChangeEffectIntensity<Scp207>(2);
        }
    }
}