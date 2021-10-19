

namespace Transaction.Models
{
    public class transactionModels
    {
        public string txnId { get; set; }
        public string accountId { get; set; }
        public  string toId { get; set; }
        public  double amount { get; set; }
        public  string description { get; set; }
        public  string dateTime { get; set; }
        

        public transactionModels(string accountID, string toID, double amount1, string description, string datetime,string bankId)
        {
            txnId = generateTxnId(accountID, bankId);
            accountId = accountID;
            toId = toID;
            amount = amount1;
            this.description = description;
            dateTime = datetime;
        }
        
        private string generateTxnId(string accountId,string bankId) { 
              return "TXN"+accountId+bankId+ System.DateTime.Now.ToString(); 
        }
        

        
    }
 }
