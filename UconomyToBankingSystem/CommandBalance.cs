using System.Collections.Generic;
using Game4Freak.BankingSystem;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace fr34kyn01535.Uconomy
{
    public class CommandBalance : IRocketCommand
    {
        public string Name => "balance";

        public string Help => "Shows the current balance";


        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Syntax => "(player)";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> {"uconomy.balance"};

        public void Execute(IRocketPlayer caller, params string[] command)
        {
            if (command.Length == 1)
            {
                if (caller.HasPermission("balance.check"))
                {
                    var target = UnturnedPlayer.FromName(command[0]);
                    if (target != null)
                    {
                        var balance = Uconomy.Instance.Database.GetBalance(target.Id);
                        ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_balance_show_otherPlayer_colored",
                                Uconomy.Instance.Configuration.Instance.MoneySymbol, balance,
                                Uconomy.Instance.Configuration.Instance.MoneyName)), Color.white, null, ((UnturnedPlayer)caller).SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                    }
                    else
                    {
                        ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_balance_error_player_not_found_colored")), Color.white, null, ((UnturnedPlayer)caller).SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                    }
                }
                else
                {
                    ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_balance_check_noPermissions_colored")), Color.white, null, ((UnturnedPlayer)caller).SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                }
            }
            else
            {
                var balance = Uconomy.Instance.Database.GetBalance(caller.Id); 
                ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_balance_show_colored",
                        Uconomy.Instance.Configuration.Instance.MoneySymbol, balance,
                        Uconomy.Instance.Configuration.Instance.MoneyName)), Color.white, null, ((UnturnedPlayer)caller).SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
            }
        }
    }
}