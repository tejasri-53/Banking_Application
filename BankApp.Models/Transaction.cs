using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model
{
    public class Transaction
    {
        public string transactionID;
        public string sID, rID;
        public double amount;
        public string description;
        public string  time;
        public Transaction(string tID, string sID, string rID, string description,double amount,  string time)
        {
            this.transactionID = tID;
            this.sID = sID;
            this.rID = rID;
            this.description = description;
            this.amount = amount;
            this.time = time;
        }
        public Transaction(string tID, string rID, double amount, string description, string time)
        {
            this.transactionID = tID;
            this.rID = rID;
            this.amount = amount;
            this.description= description;
            this.time = time;
        }
        
    }
}