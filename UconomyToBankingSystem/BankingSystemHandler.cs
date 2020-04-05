using Game4Freak.BankingSystem;
using Game4Freak.BankingSystem.model;
using Rocket.Unturned.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fr34kyn01535.Uconomy
{
    public class BankingSystemHandler
    {
        public BankingSystemHandler()
        {
            API.onBalanceIncreased += onBankingSystemBalanceIncreased;
        }

        private void onBankingSystemBalanceIncreased(string accountId, decimal amount, TransferReason reason)
        {
            foreach (var playerId in BankingSystem.Instance.api.getActivePlayersFromAccount(accountId))
            {
                Uconomy.Instance.BalanceUpdated(playerId, amount);
            }
        }
    }
}
