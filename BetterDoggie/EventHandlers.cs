namespace BetterDoggie
{
    using System;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using Interactables.Interobjects;
    using Interactables.Interobjects.DoorUtils;
    using CustomPlayerEffects;
    using MEC;

    public static class EventHandlers
    {
        public static void OnChangingRoles(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole != RoleType.Scp93953 && ev.NewRole != RoleType.Scp93989)
                return;

            Timing.CallDelayed(2f, () =>
            {
                ev.Player.Broadcast(BetterDoggie.Singleton.Config.SpawnBroadcast);

                ev.Player.Health = BetterDoggie.Singleton.Config.DoggieHealth;
                ev.Player.MaxHealth = BetterDoggie.Singleton.Config.DoggieHealth;
                ev.Player.ArtificialHealth = BetterDoggie.Singleton.Config.DoggieAhp;
                ev.Player.MaxArtificialHealth = BetterDoggie.Singleton.Config.DoggieAhp;

                ev.Player.Scale = BetterDoggie.Singleton.Config.DoggieScale;
                
                if (BetterDoggie.Singleton.Config.ColaSpeedBoost)
                    ev.Player.EnableEffect<Scp207>();
            });
        }

        public static void OnHurtingPlayer(HurtingEventArgs ev)
        {
            if (ev.Attacker.Role != RoleType.Scp93953 && ev.Attacker.Role != RoleType.Scp93989 || ev.Attacker == ev.Target)
                return;

            if (ev.DamageType == DamageTypes.Scp207)
            {
                ev.IsAllowed = false;
                return;
            }

            var maxHume = BetterDoggie.Singleton.Config.DoggieAhp;
            ev.Amount = BetterDoggie.Singleton.Config.BaseDamage + Math.Abs(ev.Attacker.ArtificialHealth - maxHume) / maxHume  * BetterDoggie.Singleton.Config.MaxDamageBoost;
            
            ev.Attacker.EnableEffect<SinkHole>(3f, true);
            ev.Attacker.ChangeEffectIntensity<SinkHole>(2);
        }

        public static void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (!BetterDoggie.Singleton.Config.EnableDogDoorBusting)
                return;
            
            if (ev.Player.Role != RoleType.Scp93953 && ev.Player.Role != RoleType.Scp93989)
                return;

            if (ev.Player.ArtificialHealth <= BetterDoggie.Singleton.Config.DoorBustAhp)
                BustDoor(ev.Door.Base, ev.Player, BetterDoggie.Singleton.Config.EnableBustSpeedBoost);
        }

        /// <summary>
        /// Busts down a door and applies effect
        /// </summary>
        /// <param name="door"></param>
        /// <param name="ply"></param>
        /// <param name="speedBoost"></param>
        private static void BustDoor(DoorVariant door, Player ply, bool speedBoost)
        {
            switch (door)
            {
                case IDamageableDoor damage:
                    damage.IsDestroyed = true;
                    break;
                case PryableDoor pryableDoor:
                    pryableDoor.TryPryGate();
                    break;
            }

            if (!speedBoost)
                return;
            
            ply.ChangeEffectIntensity<Scp207>(BetterDoggie.Singleton.Config.BustBoostAmount);
            if (BetterDoggie.Singleton.Config.ColaSpeedBoost)
                Timing.CallDelayed(2f, () => ply.ChangeEffectIntensity<Scp207>(1));
        }
    }
}