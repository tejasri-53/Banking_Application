using System;

namespace BankApplication
{
    public class AccountModel
    {
         
        public static int index = 0;
        public string AccountID;
        public string name;
        public double amount;
        public string pin;


        public AccountModel(string name, string pin)
        {
            this.AccountID = ""+ ++index;
            this.pin = pin;
            this.name = name;
            this.amount = 0;
            Console.WriteLine("Account Created Successfully with Account No: " + this.AccountID + " Name: " + name + " Balance: " + amount);
        }

        public String GetPin()
        {
            return this.pin;
        }

        public string getAccountId() {
            return this.AccountID;
        }
        
        public String GenerateAccountId(String name) {
            string accId = "";
            for (int i = 0; i < 3; i++) {
                accId += name[i];
            }
            accId += System.DateTime.Now.ToString();
            return accId;
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