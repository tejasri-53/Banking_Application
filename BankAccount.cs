using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Application
{
    public class BankAccount
    {
        
        public void Deposit(int  amount, int AccountId, int[] Account_Ids,int[] AccountBalances) {

            int c = 0;
            for (int i = 0; i < Account_Ids.Length ; i++) {
                if (Account_Ids[i] == AccountId) {
                    AccountBalances[i] += amount;
                    Console.WriteLine("Deposited " + amount + " to the bank account " +AccountId);
                    c= 1;
                    break;
                }
            }
            this.AccountBalance(AccountId,Account_Ids,AccountBalances);
            
        }

        public void Withdraw(int  Money, int AccountId,int[] Account_Ids, int[] AccountBalances) {
     
            for (int i = 0; i <Account_Ids.Length; i++)
            {
               
                if (Account_Ids[i]== AccountId)
                {
                    if (Money> AccountBalances[i])
                    {
                        Console.WriteLine("Withdrawl Amount Exceeds Balance");
                        break;
                    }
                    else
                    {
                        AccountBalances[i]=AccountBalances[i]- Money;
                        Console.WriteLine("Withdrawn " + Money + " from the bank account " + AccountId);
                        break;
                    }
                }
            }
        }

        public void AccountBalance(int AccountId,int[] Account_Ids, int[] AccountBalances) {
            for (int i = 0; i < Account_Ids.Length; i++)
            {
                if (Account_Ids[i] == AccountId)
                {
                    Console.WriteLine("Account "+AccountId  + " has amount  " + AccountBalances[i]);
                    break;
                }
            }
            
        }



    }
}
