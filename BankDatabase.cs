using System;
using System.Collections.Generic;
using System.Text;

namespace Bank_Application
{
    class Bank_Database
    {
        public static void bankData(int[] amounts,int[] ids,int i)
        {
            Console.WriteLine("Account id    Amount");
            for (int j = 0; j < i; j++)
            {
                Console.WriteLine(ids[j] + "            " + amounts[j]);
            }
        }
}
}
