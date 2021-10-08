
using System.Collections.Generic;
using BankApplication.Services;
namespace BankApplication
{
    class Transfer
    {
        
        public static void TransferAmount(Dictionary<int, Account> AccountsList)
        {

            int accountID = DisplayMessages.EnterAccountID();

            if (AccountsList.ContainsKey(accountID))
            {
                string pin = DisplayMessages.EnterPIN();
                if (!Bank.Validate(accountID, pin))
                {
                    DisplayMessages.InvalidPIN();
                }
                else
                {
                    int toID = DisplayMessages.EnterAccountID();
                    if (AccountsList.ContainsKey(toID))
                    {
                        double amount = DisplayMessages.EnterTransferAmount();
                        Account account = AccountsList[accountID];
                        
                        if (BankAccount.VerifyBalanceAmount(account, amount))
                        {
                            account.SetAmount(account.GetAmount() - amount);
                            AccountsList[toID].SetAmount(AccountsList[toID].GetAmount() + amount);
                            DisplayMessages.TransferMessage();
                            BankAccount.BalanceEnquiry(account);

                        }
                        else
                        {
                            DisplayMessages.InsufficientAmount();
                        }
                    }
                    else
                    {
                        DisplayMessages.AccountDoesntExist();
                    }
                }
            }
            else
            {
                DisplayMessages.AccountDoesntExist();
            }
        }

    }


}
