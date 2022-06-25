using CommandSystem;
using System;

namespace BetterDoggie.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class HelpCommand : ICommand
    {
        public string Command { get; } = "doggiehelp";
        public string[] Aliases { get; } = Array.Empty<string>();
        public string Description { get; } = "Shows help for the Better Doggie plugin";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "Upgraded SCP-939s have a boost ability that temporarily grants the ability to break down doors." +
            "To use this ability, you must set a keybind in your console (~ key) with the format: \"cmdbind <keycode> .doggieboost\"." +
            "For example: \"cmdbind f .doggieboost\" will bind your F key to the .doggieboost command.";
            return true;

        }
    }
}
