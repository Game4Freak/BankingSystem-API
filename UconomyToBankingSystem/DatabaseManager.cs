using System;
using System.Collections.Generic;
using System.Globalization;
using Game4Freak.BankingSystem;
using Game4Freak.BankingSystem.model;
using I18N.West;
using MySql.Data.MySqlClient;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Steamworks;

namespace fr34kyn01535.Uconomy
{
    public class DatabaseManager
    {
        internal DatabaseManager()
        {
        }

        /// <summary>
        /// Retrieves the current balance of a specific account.
        /// </summary>
        /// <param name="id">The Steam 64 ID of the account to retrieve the balance from.</param>
        /// <returns>The balance of the account.</returns>
        public decimal GetBalance(string id)
        {
            decimal balance = BankingSystem.Instance.api.getPlayerBalance(id);
            Uconomy.Instance.OnBalanceChecked(id, balance);
            return balance;
        }

        /// <summary>
        /// Increases the account balance of the specific ID with IncreaseBy.
        /// </summary>
        /// <param name="id">Steam 64 ID of the account.</param>
        /// <param name="increaseBy">The amount that the account should be changed with (can be negative).</param>
        /// <returns>The new balance of the account.</returns>
        public decimal IncreaseBalance(string id, decimal increaseBy)
        {
            TransferReason reason = null;
            if (increaseBy >= 0)
            {
                reason = new TransferReason_Unknown_In($"{id} ({UnturnedPlayer.FromCSteamID(new CSteamID(ulong.Parse(id)))?.DisplayName})", "", increaseBy);
            }
            else
            {
                reason = new TransferReason_Unknown_Out("", $"{id} ({UnturnedPlayer.FromCSteamID(new CSteamID(ulong.Parse(id)))?.DisplayName})", increaseBy);
            }
            BankingSystem.Instance.api.increaseBalance(id, increaseBy, reason);
            decimal balance = BankingSystem.Instance.api.getPlayerBalance(id);
            return balance;
        }

        /// <summary>
        /// Ensures that the account exists in the database and creates it if it isn't.
        /// </summary>
        /// <param name="id">Steam 64 ID of the account to ensure its existence.</param>
        public void CheckSetupAccount(CSteamID id)
        {
        }
    }
}