
using System;
using System.Collections.Generic;
using BankApplication.Models;
using BankApplication.Services;
using Transaction.Models;
using Transaction.Services;

namespace BankApplication
{
    class Transfer
    {
        
        public static void TransferAmount(Dictionary<string, AccountModel> AccountsList, List<transactionModels> transactionList)
        {

            string accountID = DisplayMessages.EnterAccountID();

            if (AccountsList.ContainsKey(accountID))
            {
                string pin = DisplayMessages.EnterPIN();
                if (!Bank.Validate(accountID, pin))
                {
                    DisplayMessages.InvalidPIN();
                }
                else
                {
                    string toID = DisplayMessages.EnterAccountID();
                    if (AccountsList.ContainsKey(toID))
                    {
                        double amount = DisplayMessages.EnterTransferAmount();
                        AccountModel account = AccountsList[accountID];
                        
                        if (bankAccount.VerifyBalanceAmount(account,amount))
                        {
                            account.SetAmount(account.GetAmount() - amount);
                            AccountsList[toID].SetAmount(AccountsList[toID].GetAmount() + amount);
                            DisplayMessages.TransferMessage();
                            bankAccount.BalanceEnquiry(account);
                            transactionServices.AddTransaction(transactionList, accountID, toID, "transfer", amount, DateTime.Now.ToString("MM/dd/yyyy h:mm tt"),bankModel.bankId);
                            transactionServices.PrintTransactions(accountID, transactionList, AccountsList);
                            
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
