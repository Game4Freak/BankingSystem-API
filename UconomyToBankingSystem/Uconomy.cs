﻿using System;
using Game4Freak.BankingSystem;
using Game4Freak.BankingSystem.model;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;

namespace fr34kyn01535.Uconomy
{
    public class Uconomy : RocketPlugin<UconomyConfiguration>
    {
        public DatabaseManager Database;
        public static Uconomy Instance;

        public static string MessageColor;

        public BankingSystemHandler handler;

        protected override void Load()
        {
            Instance = this;
            Database = new DatabaseManager();
            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            MessageColor = Configuration.Instance.MessageColor;

            Level.onLevelLoaded += onLevelLoaded;
        }

        public delegate void PlayerBalanceUpdate(UnturnedPlayer player, decimal amt);

        public event PlayerBalanceUpdate OnBalanceUpdate;

        public delegate void PlayerBalanceCheck(UnturnedPlayer player, decimal balance);

        public event PlayerBalanceCheck OnBalanceCheck;

        public delegate void PlayerPay(UnturnedPlayer sender, string receiver, decimal amt);

        public event PlayerPay OnPlayerPay;

        public override TranslationList DefaultTranslations =>
            new TranslationList
            {
                {"command_balance_show_colored", "Your current balance is: <color=#08DDA8>{0} {1} {2}</color>."},
                {"command_balance_error_player_not_found_colored", "<color=#FF3C3C>Failed</color> to find player!"},
                {"command_balance_check_noPermissions_colored", "<color=#FF3C3C>Insufficent</color> Permissions!"},
                {"command_balance_show_otherPlayer_colored", "<color=#08DDA8>{0}</color>'s current balance is: <color=#08DDA8>{0} {1} {2}</color>"},
                {"command_pay_invalid", "Invalid arguments."},
                {"command_pay_invalid_colored", "<color=#FF3C3C>Invalid arguments</color>."},
                {"command_pay_error_pay_self", "You cant pay yourself."},
                {"command_pay_error_pay_self_colored", "You <color=#FF3C3C>cant pay</color> yourself."},
                {"command_pay_error_invalid_amount", "Invalid amount."},
                {"command_pay_error_invalid_amount_colored", "<color=#FF3C3C>Invalid</color> amount."},
                {"command_pay_error_cant_afford", "Your balance does not allow this payment."},
                {"command_pay_error_cant_afford_colored", "Your <color=#FF3C3C>balance</color> does <color=#FF3C3C>not allow</color> this payment."},
                {"command_pay_error_player_not_found", "Failed to find player."},
                {"command_pay_error_player_not_found_colored", "<color=#FF3C3C>Failed</color> to find player."},
                {"command_pay_private", "You paid <color=#08DDA8>{0}</color> <color=#FF3C3C>{1} {2}</color>."},
                {"command_pay_private_colored", "You paid {0} {1} {2}."},
                {"command_pay_console", "You received a payment of {0} {1}."},
                {"command_pay_console_colored", "You received a payment of <color=#08DDA8>{0} {1}</color>."},
                {"command_pay_other_private", "You received a payment of {0} {1} from {2}."},
                {"command_pay_other_private_colored", "You received a payment of <color=#08DDA8>{0} {1}</color> from <color=#08DDA8>{2}</color>."}
            };

        private void onLevelLoaded(int level)
        {
            handler = new BankingSystemHandler();
        }

        internal void HasBeenPayed(UnturnedPlayer sender, string receiver, decimal amt)
        {
            OnPlayerPay?.Invoke(sender, receiver, amt);
        }

        internal void BalanceUpdated(string steamId, decimal amt)
        {
            if (OnBalanceUpdate == null) return;

            var player = UnturnedPlayer.FromCSteamID(new CSteamID(Convert.ToUInt64(steamId)));
            OnBalanceUpdate(player, amt);
        }

        internal void OnBalanceChecked(string steamId, decimal balance)
        {
            if (OnBalanceCheck == null) return;

            var player = UnturnedPlayer.FromCSteamID(new CSteamID(Convert.ToUInt64(steamId)));
            OnBalanceCheck(player, balance);
        }

        private void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            // Ensure that the account exists within the database.
            Database.CheckSetupAccount(player.CSteamID);
        }
    }
}