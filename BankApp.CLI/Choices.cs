using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.CLI // add enum for type of transaction
{

    public enum MainMenu
    {
        CreateBank=1,
        CreateStaffAccount,
        Login,
        EXIT,
    }

    public enum CustomerLoginMenu
    {
        Deposit = 1,
        TransferMoney ,
        Withdraw,
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