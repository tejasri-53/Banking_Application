using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Application
{
    class Transfer
    {
        public static void transfer(int[] AccountBalances,int[] Account_ids) 
        {
            Console.WriteLine("Enter  Account id from which transfer has to be processed :");
            int fromId= Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Account id to which the transfer amount has to be credited:");
            int to_Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Amount to be transfered :");
            int Amount = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < Account_ids.Length; i++)
            {
                if (Account_ids[i] == fromId)
                {
                    AccountBalances[i] -= Amount;
                }
                else if(Account_ids[i] == to_Id){
                    AccountBalances[i] += Amount;
                }
            }
            Console.WriteLine(Amount + " has been transfered from the bank account " + fromId + " to the bank Account "+ to_Id);
        }
    }
}
