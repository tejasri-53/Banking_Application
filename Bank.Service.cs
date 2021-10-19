using BankApplication.Models;
using System;
using System.Collections.Generic;

namespace Bank_Application.Services
{
    public class bankService
    {
        private List<bankModel> banks;

        public bankService() 
        {
            this.banks = new List<bankModel>();
        }

        public int createBank(string name, int IFSC,DateTime createdOn, string createdBy) 
        {

            bankModel bankMod = new bankModel
            {
                IFSC = RandomBankId(),
                name = name,
                CreatedOn = createdOn,
                CreatedBy=createdBy,
                
            };
            this.banks.Add(bankMod);
            return IFSC;
        }
        private int RandomBankId() 
        {
            Random random = new Random();
            return random.Next(1, 10000);
        }
    }
}
