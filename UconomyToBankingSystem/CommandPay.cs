using System.Collections.Generic;
using Game4Freak.BankingSystem;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Commands;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;

namespace fr34kyn01535.Uconomy
{
    public class CommandPay : IRocketCommand
    {
        public string Help => "Pays a specific player money from your account";

        public string Name => "pay";

        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Syntax => "<player> <amount>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> {"uconomy.pay"};

        public void Execute(IRocketPlayer caller, params string[] command)
        {
            if (command.Length != 2)
            {
                if (caller is UnturnedPlayer)
                {
                    ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_pay_invalid_colored")), Color.white, null, ((UnturnedPlayer)caller).SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                }
                else
                {
                    UnturnedChat.Say(caller, Uconomy.Instance.Translations.Instance.Translate("command_pay_invalid"),
                        UnturnedChat.GetColorFromName(Uconomy.MessageColor, Color.green));
                }
                return;
            }

            var otherPlayer = command.GetCSteamIDParameter(0)?.ToString();
            var otherPlayerOnline = UnturnedPlayer.FromName(command[0]);
            if (otherPlayerOnline != null) otherPlayer = otherPlayerOnline.Id;

            if (otherPlayer != null)
            {
                if (caller.Id == otherPlayer)
                {
                    if (caller is UnturnedPlayer)
                    {
                        ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_pay_error_pay_self_colored")), Color.white, null, ((UnturnedPlayer)caller).SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                    }
                    else
                    {
                        UnturnedChat.Say(caller,
                        Uconomy.Instance.Translations.Instance.Translate("command_pay_error_pay_self"),
                        UnturnedChat.GetColorFromName(Uconomy.MessageColor, Color.green));
                    }
                    return;
                }

                if (!decimal.TryParse(command[1], out var amount) || amount <= 0)
                {
                    if (caller is UnturnedPlayer)
                    {
                        ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_pay_error_invalid_amount_colored")), Color.white, null, ((UnturnedPlayer)caller).SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                    }
                    else
                    {
                        UnturnedChat.Say(caller,
                            Uconomy.Instance.Translations.Instance.Translate("command_pay_error_invalid_amount"),
                            UnturnedChat.GetColorFromName(Uconomy.MessageColor, Color.green));
                    }
                    return;
                }

                if (caller is ConsolePlayer)
                {
                    Uconomy.Instance.Database.IncreaseBalance(otherPlayer, amount);
                    if (otherPlayerOnline != null)
                    {
                        ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_pay_console_colored", amount,
                                Uconomy.Instance.Configuration.Instance.MoneyName)), Color.white, null, otherPlayerOnline.SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                    }
                }
                else
                {
                    var myBalance = Uconomy.Instance.Database.GetBalance(caller.Id);
                    if (myBalance - amount <= 0)
                    {
                        if (caller is UnturnedPlayer)
                        {
                            ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_pay_error_invalid_amount_colored")), Color.white, null, ((UnturnedPlayer)caller).SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                        }
                        else
                        {
                            UnturnedChat.Say(caller,
                            Uconomy.Instance.Translations.Instance.Translate("command_pay_error_cant_afford"),
                            UnturnedChat.GetColorFromName(Uconomy.MessageColor, Color.green));
                        }
                    }
                    else
                    {
                        Uconomy.Instance.Database.IncreaseBalance(caller.Id, -amount);
                        if (otherPlayerOnline != null)
                        {
                            if (caller is UnturnedPlayer)
                            {
                                ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_pay_private_colored", otherPlayerOnline.CharacterName, amount,
                                    Uconomy.Instance.Configuration.Instance.MoneyName)), Color.white, null, ((UnturnedPlayer)caller).SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                            }
                            else
                            {
                                UnturnedChat.Say(caller,
                                Uconomy.Instance.Translations.Instance.Translate("command_pay_private",
                                    otherPlayerOnline.CharacterName, amount,
                                    Uconomy.Instance.Configuration.Instance.MoneyName),
                                UnturnedChat.GetColorFromName(Uconomy.MessageColor, Color.green));
                            }
                        }
                        else
                        {
                            if (caller is UnturnedPlayer)
                            {
                                ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_pay_private_colored", otherPlayer,
                                    amount, Uconomy.Instance.Configuration.Instance.MoneyName)), Color.white, null, ((UnturnedPlayer)caller).SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                            }
                            else
                            {
                                UnturnedChat.Say(caller,
                                Uconomy.Instance.Translations.Instance.Translate("command_pay_private", otherPlayer,
                                    amount, Uconomy.Instance.Configuration.Instance.MoneyName),
                                UnturnedChat.GetColorFromName(Uconomy.MessageColor, Color.green));
                            }
                        }

                        Uconomy.Instance.Database.IncreaseBalance(otherPlayer, amount);
                        if (otherPlayerOnline != null)
                        {
                            if (otherPlayerOnline is UnturnedPlayer)
                            {
                                ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_pay_other_private_colored", amount,
                                    Uconomy.Instance.Configuration.Instance.MoneyName, caller.DisplayName)), Color.white, null, otherPlayerOnline.SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                            }
                            else
                            {
                                UnturnedChat.Say(otherPlayerOnline.CSteamID,
                                Uconomy.Instance.Translations.Instance.Translate("command_pay_other_private", amount,
                                    Uconomy.Instance.Configuration.Instance.MoneyName, caller.DisplayName),
                                UnturnedChat.GetColorFromName(Uconomy.MessageColor, Color.green));
                            }
                        }

                        Uconomy.Instance.HasBeenPayed((UnturnedPlayer)caller, otherPlayer, amount);
                    }
                }
            }
            else
            {
                if (otherPlayerOnline is UnturnedPlayer)
                {
                    ChatManager.serverSendMessage(BankingSystem.Instance.Translate("chat_title", Uconomy.Instance.Translate("command_pay_error_player_not_found_colored")), Color.white, null, otherPlayerOnline.SteamPlayer(), EChatMode.SAY, BankingSystem.Instance.Configuration.Instance.iconUrl, true);
                }
                else
                {
                    UnturnedChat.Say(caller,
                            Uconomy.Instance.Translations.Instance.Translate("command_pay_error_player_not_found"));
                }
            }
        }
    }
}