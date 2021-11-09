

namespace BankApp.CLI
{
    public class Choices
    {
        public enum MainChoice
        {
            SetupBank=1,
            CreateStaffAccount,
            Login ,
            EXIT,
        }

        public enum CustomerLoginChoice
        {
            TransferMoney = 1,
            Deposit,
            Withdraw,
            ShowTransactions,
            RevertTransaction,
            Logout,
        }

        public enum StaffLoginChoice
        {
            CreateAccount = 1,
            UpdateAccount,
            DeleteAccount,
            UpdateSameBankRTGSIMPS,
            UpdatedifferentBankRTGSIMPS,
            AddCurrency,
            ViewAccountTransaction,
            Logout,
            
        }

        public enum UpdateCustomerAccountChoice
        {
            UpdateName = 1,
            UpdatePassword,
            Back,
        }
    }
}