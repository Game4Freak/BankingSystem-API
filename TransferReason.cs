using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game4Freak.BankingSystem.model
{
    public abstract class TransferReason
    {
        // fromPlayer is the player the money is coming from (doesnt have to be the id)
        public string fromPlayer { private set; get; }
        // toPlayer is the player the money is going to (doesnt have to be the id)
        public string toPlayer { private set; get; }
        // amount is the amount of money that is transfered
        public decimal amount { private set; get; }

        public TransferReason(string fromPlayer, string toPlayer, decimal amount)
        {
            this.fromPlayer = fromPlayer;
            this.toPlayer = toPlayer;
            this.amount = amount;
        }

        // This function will be called to get the text of the transfer reason
        // This method adds the amount at the start of the reason
        public string getText()
        {
            return $"{BankingSystem.Instance.api.getAmountString(amount)}: {getTransferReason()}";
        }

        // Override this method with your custom reason
        protected abstract string getTransferReason();
    }

    // Custom Example 1
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

    // Custom Example 2
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
