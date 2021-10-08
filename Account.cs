using System;

namespace BankApplication
{
    class Account
    {
        public static int index = 0;
        public int AccountID;
        public string name;
        public double amount;
        public string pin;


        public Account(string name, string pin)
        {
            this.AccountID = index++;
            this.pin = pin;
            this.name = name;
            this.amount = 0;
            Console.WriteLine("Account Created Successfully with Account No: " + AccountID + " Name: " + name + " Balance: " + amount);
        }

        public String GetPin()
        {
            return this.pin;
        }


        public int GetAccountID()
        {
            return this.AccountID;
        }

        public double GetAmount()
        {
            return this.amount;
        }
        public void SetAmount(double amount)
        {
            this.amount = amount;
        }
    }

}
