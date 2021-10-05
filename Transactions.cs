using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Application
{
    class Transactions
    {     
        public void Solve(int[] a,int[] b,int len) {
            Console.WriteLine("Choose:  \n 1.Deposit Money \n 2.Withdraw \n 3.Balance ");
            int ch = Convert.ToInt32(Console.ReadLine());
            BankAccount bankAccount = new BankAccount();
            
            if (ch == 1)
            {
                Console.WriteLine("Enter Amount to be Deposited :");
                int amt = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Account id :");
                int id = Convert.ToInt32(Console.ReadLine());
                bankAccount.Deposit(amt, id,a,b);
            }
            else if (ch == 2)
            {
                Console.WriteLine("Enter Amount to be Withdrawn :");
                int amt = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Account id :");
                int id = Convert.ToInt32(Console.ReadLine());
                bankAccount.Withdraw(amt, id,a,b);
            }
            else {
                Console.WriteLine("Enter Account id :");
                int id = Convert.ToInt32(Console.ReadLine());
                bankAccount.AccountBalance(id,a,b);
            }
        }







    }
}