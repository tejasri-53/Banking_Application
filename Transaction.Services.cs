using BankApplication;
using System;
using System.Collections.Generic;
using Transaction.Models;

namespace Transaction.Services
{
    public class transactionServices
    {
        public static void AddTransaction(List<transactionModels> transactionList,string accountID, string toID, string description, double amount, string datetime,string bankId)
        {
            transactionModels t = new transactionModels(accountID,toID,  amount, description, datetime,bankId);
            transactionList.Add(t);
        }
        public static void PrintTransactions(string accountID,List<transactionModels> transactionList, Dictionary<string, AccountModel> AccountsList)
        {
            foreach (transactionModels transaction in transactionList)
            {
                if (transaction.accountId == accountID)
                {
                    Console.Write(transaction.dateTime + "  " + transaction.description + "  " + transaction.amount + "  ");
                    if (transaction.toId != "")
                    {
                        if (transaction.description == "deposit")
                        {
                            Console.Write(" from " + AccountsList[transaction.toId].name);
                        }
                        else
                        {
                            Console.Write(" to " + AccountsList[transaction.toId].name);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        

    }
}
