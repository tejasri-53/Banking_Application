using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.CLI
{

    public enum MainMenu
    {
        createBank=1,
        Login,
        EXIT,
    }

    public enum CustomerLoginMenu
    {
        
        TransferMoney =1,
        Withdraw,
        Deposit ,
        Logout,
    }
    public enum StaffLoginMenu
    {
        CreateAccount = 1,
        UpdateAccount,
        DeleteAccount,
        UpdatesRTGS,
        UpdatesIMPS,
        UpdateoRTGS,
        UpdateoIMPS,
        Logout,
        
    }

    public enum UpdateCustomerAccountMenu
    {
        UpdateName = 1,
        UpdatePassword,
        Back,
    }

    public enum TransactionModeMenu
    {
        RTGS = 1,
        IMPS,
    }
}
