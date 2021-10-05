using System;

namespace Bank_Application
{
    class Bank
    {
        static void Main(string[] args)
        {
            int[] ids = new int[100000000];
            int[] amounts = new int[10000000];
            int i = 0;
            
            while (true)
            {
                Console.WriteLine("Enter your Choice : \n 1. Create New Account \n 2. Perform Transactions on Existing Account \n 3. Bank Accounts Database \n 4. Transfer ");
                int choice= Convert.ToInt32(Console.ReadLine());
                if (choice == 2)
                {
                    Transactions t = new Transactions();
                    t.Solve(ids, amounts, i);
                    
                }
                else if (choice == 1)
                {
                    CreateAccount.createAccount(ids, amounts, i);
                    i += 1;
                }
                else if (choice == 3)
                {
                    Bank_Database.bankData(amounts, ids, i);

                }
                else if(choice==4){
                    Transfer.transfer(amounts,ids);
                }
                else
                {
                    Console.WriteLine("Please Enter Valid Option ");
                }
                Console.WriteLine("Enter 1 if you wish to Continue else Enter 2 ");
                int ch= Convert.ToInt32(Console.ReadLine());
                
                if (ch== 2)
                {
                    break;
                }
            }
        }
    }

    
}
