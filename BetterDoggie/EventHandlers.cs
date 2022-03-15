using UnityEngine;

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
            // When 939 dies change the size back to normal
            if (!Is939(ev.Player.Role.Type))
                ev.Player.Scale = new Vector3(1, 1, 1);

            Timing.CallDelayed(2f, () =>
            {
                if (ev.Player == null || !Is939(ev.Player.Role)) return;
                
                ev.Player.Broadcast(BetterDoggie.Singleton.Config.SpawnBroadcast);

                ev.Player.Health = BetterDoggie.Singleton.Config.DoggieHealth;
                ev.Player.MaxHealth = BetterDoggie.Singleton.Config.DoggieHealth;
                ev.Player.ArtificialHealth = BetterDoggie.Singleton.Config.DoggieAhp;
                ev.Player.MaxArtificialHealth = BetterDoggie.Singleton.Config.DoggieAhp;

                ev.Player.Scale = BetterDoggie.Singleton.Config.DoggieScale;

                if (BetterDoggie.Singleton.Config.ColaSpeedBoost <= 0) return;
                ev.Player.EnableEffect<MovementBoost>();
                ev.Player.ChangeEffectIntensity<MovementBoost>(BetterDoggie.Singleton.Config.ColaSpeedBoost);
            });
        }

        public static void OnHurtingPlayer(HurtingEventArgs ev)
        {
            if (ev.Attacker == null || ev.Target == null || ev.Attacker == ev.Target || !Is939(ev.Attacker.Role))
                return;
            
            // Original damage + percentage of hume shield gone * max damage | ex. (40 + .50 * 150)
            var maxHume = BetterDoggie.Singleton.Config.DoggieAhp;
            ev.Amount = BetterDoggie.Singleton.Config.BaseDamage +
                        Math.Abs(ev.Attacker.ArtificialHealth - maxHume) /
                        (maxHume * BetterDoggie.Singleton.Config.MaxDamageBoost);
            
            ev.Attacker.EnableEffect<SinkHole>(BetterDoggie.Singleton.Config.SlowdownDuration, BetterDoggie.Singleton.Config.ShouldSlowdownStack);
            ev.Attacker.ChangeEffectIntensity<SinkHole>(2);
        }

        public static void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (!BetterDoggie.Singleton.Config.EnableDogDoorBusting)
                return;
            
            if (!Is939(ev.Player.Role)
                || (ev.Door.Base is IDamageableDoor door && door.IsDestroyed)
                || (ev.Door.Base is PryableDoor gate && gate.IsConsideredOpen()))
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
            
            ply.ChangeEffectIntensity<MovementBoost>(BetterDoggie.Singleton.Config.BustBoostAmount);
            Timing.CallDelayed(2f, () => ply.ChangeEffectIntensity<MovementBoost>(BetterDoggie.Singleton.Config.ColaSpeedBoost));
        }

        
        /// <summary>
        /// Check if player is 939
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private static bool Is939(RoleType role)
        {
            return role == RoleType.Scp93953 || role == RoleType.Scp93989;
        }
    }
}