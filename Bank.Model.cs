using System;


namespace BankApplication.Models
{
    public class bankModel
    {
        public static string bankId { get; set; }
        public string IFSC { set; get; }
        public string name { set; get; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bankModel(string name,DateTime createdOn , string CreatedBy) 
        {
            bankId = generateBankId();
            IFSC = "sbh100000";
            this.name = name;
            CreatedOn = createdOn;
            this.CreatedBy = CreatedBy;
        }

        public string generateBankId() { 
            string id= "sbh"+ System.DateTime.Now.ToString();
            return id;
        }
       
    }
}
