using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Features;
using MEC;

namespace BetterDoggie.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class BoostCommand : ICommand
    {
        public string Command { get; } = "doggieboost";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Grants a temporary boost to SCP-939.";

        private Config _config = BetterDoggie.Singleton.Config;
        private Dictionary<Player, CoroutineHandle?> _activeAbilities = BetterDoggie.Singleton.ActiveAbilities;
        private Dictionary<Player, int> _abilityCooldowns = BetterDoggie.Singleton.AbilityCooldowns;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get((sender as CommandSender)?.SenderId);

            if (Is939(player.Role.Type))
            {
                if (_abilityCooldowns.ContainsKey(player))
                {
                    response = $"Door busting ability on cooldown for {_abilityCooldowns[player]} more seconds.";
                    return true;
                }

                if (_activeAbilities.ContainsKey(player))
                {
                    if (!(_activeAbilities[player] == null)) Timing.KillCoroutines((CoroutineHandle)_activeAbilities[player]);

                    _activeAbilities.Remove(player);
                    _abilityCooldowns.Add(player, _config.DoorBustingCooldown);

                    Timing.RunCoroutine(CooldownCoroutine(player));

                    response = "Door busting ability disabled.";
                    return true;
                }
                else
                {
                    _activeAbilities.Add(player, null);

                    player.ShowHint("<color=green>Door busting ability activated.");

                    response = "Door busting ability enabled.";
                    return true;
                }
            }
            else
            {
                response = "You must be an SCP-939 to run this command.";
                return false;
            }
        }

        private IEnumerator<float> CooldownCoroutine(Player player)
        {
            for(int i = _config.DoorBustingCooldown; i > 0; i--)
            {
                _abilityCooldowns[player] -= 1;

                player.ShowHint($"Door busting ability can be re-activated in {i} seconds.", 1);

                yield return Timing.WaitForSeconds(1f);
            }

            _abilityCooldowns.Remove(player);
            _activeAbilities.Remove(player);
        }

        private static bool Is939(RoleType role)
        {
            return role == RoleType.Scp93953 || role == RoleType.Scp93989;
        }
    }
}
