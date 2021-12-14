using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Model
{
    public class StaffAccounts
    {
        public string AccountId { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public string BankId { get; set; }

        /*
        public StaffAccounts(string accountId, string name, string password)
        {
            this.AccountId = accountId;
            this.Name = name;
            this.Password = password;
        }*/
    }
}
