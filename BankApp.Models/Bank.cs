using BankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankApp.Model.Account;

namespace BankApp.Model
{
    public class Bank
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public int SameBankRTGSCharge { get; set; }

        public int SameBankIMPSCharge { get; set; }

        public int DifferentBankRTGSCharge { get; set; }

        public int DifferentBankIMPSCharge { get; set; }

        public Dictionary<string, Account> customerAccounts = new Dictionary<string, Account>();

        public Dictionary<string, StaffAccount> staffAccounts = new Dictionary<string, StaffAccount>();


        public Bank(string bankName, int sameBankRTGS, int sameBankIMPS, int otherBankRTGS, int otherBankIMPS)
        {
            this.Name = bankName;
            DateTime PresentDate = DateTime.Today;
            this.ID = PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
            this.SameBankRTGSCharge= sameBankRTGS;
            this.SameBankIMPSCharge = sameBankIMPS;
            this.DifferentBankRTGSCharge= otherBankRTGS;
            this.DifferentBankIMPSCharge= otherBankIMPS;

        }

        public Bank(string bankName)
        {
            this.Name = bankName;
            DateTime PresentDate = DateTime.Today;
            this.ID=Name.Substring(0, 3) + PresentDate.ToString("dd") + PresentDate.ToString("MM") + PresentDate.ToString("yyyy");
            this.SameBankRTGSCharge= 0;
            this.SameBankIMPSCharge = 5;
            this.DifferentBankRTGSCharge = 2;
            this.DifferentBankIMPSCharge= 6;

        }
    }
}
