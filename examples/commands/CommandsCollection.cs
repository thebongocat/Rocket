﻿using Rocket.API.Chat;
using Rocket.API.Commands;
using Rocket.API.I18N;
using Rocket.API.Player;
using Rocket.Core.Commands;
using Rocket.Core.I18N;
using Rocket.Core.Permissions;

namespace Rocket.Examples.CommandsPlugin
{
    public class CommandsCollection
    {
        private readonly IChatManager chatManager;
        private readonly ITranslationLocator translations;

        public CommandsCollection(IChatManager chatManager, ITranslationLocator translations)
        {
            this.chatManager = chatManager;
            this.translations = translations;
        }

        [Command] //By default, name is the same as the method name, and it will support all command callers
        public void KillPlayer(ICommandCaller sender, IOnlinePlayer target)
        {
            if (target is ILivingEntity entity)
                entity.Kill(sender);
            else // the game likely does not support killing players (e.g. Eco)
                sender.SendMessage("Target could not be killed :(");
        }
        
        [Command(Name = "Broadcast", Description = "Broadcast a message")]
        [CommandAlias("bc")]
        [CommandCaller(typeof(IConsoleCommandCaller))] // only console can call it
        public void Broadcast(ICommandCaller sender, string[] args)
        {
            string message = string.Join(" ", args);
            chatManager.BroadcastLocalized(translations, message, sender, message);
        }
    }
}