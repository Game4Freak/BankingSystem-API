using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game4Freak.BankingSystem.model
{
    public abstract class TransferReason
    {
        public string fromPlayer { private set; get; }
        public string toPlayer { private set; get; }
        public decimal amount { private set; get; }

        public TransferReason(string fromPlayer, string toPlayer, decimal amount)
        {
            this.fromPlayer = fromPlayer;
            this.toPlayer = toPlayer;
            this.amount = amount;
        }

        public string getText()
        {
            return $"{BankingSystem.Instance.api.getAmountString(amount)}: {getTransferReason()}";
        }

        protected abstract string getTransferReason();
    }

    public class TransferReason_Unknown_In : TransferReason
    {
        public TransferReason_Unknown_In(string fromPlayer, string toPlayer, decimal amount) : base(fromPlayer, toPlayer, amount)
        {
        }

        protected override string getTransferReason()
        {
            return BankingSystem.Instance.Translate("transferreason_unknown_in", fromPlayer);
        }
    }

    public class TransferReason_Unknown_Out : TransferReason
    {
        public TransferReason_Unknown_Out(string fromPlayer, string toPlayer, decimal amount) : base(fromPlayer, toPlayer, amount)
        {
        }

        protected override string getTransferReason()
        {
            return BankingSystem.Instance.Translate("transferreason_unknown_out", toPlayer);
        }
    }
}
