using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class Currency
    {
        public string CurrencyCode { get; set; }
        public double EquivalentINR { get; set; }
        public Currency(string currencyCode, double equivalentINR)
        {
            CurrencyCode = currencyCode;
            EquivalentINR = equivalentINR;
        }

    }
}