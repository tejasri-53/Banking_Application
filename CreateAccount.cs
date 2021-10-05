using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Application
{
    class CreateAccount
    {
        public static void createAccount(int[] ids,int[] amounts,int i)
        {
            Console.WriteLine("Enter the Account Id:");
            int id= Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Amount:");
            int amount = Convert.ToInt32(Console.ReadLine());

            amounts[i] = amount;
            ids[i] = id;
            Console.WriteLine(id + " has  amount " + amounts[i]);
        }
    }
}
