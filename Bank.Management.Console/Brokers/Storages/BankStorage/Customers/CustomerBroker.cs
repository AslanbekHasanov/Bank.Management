//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Models;
using System.Security.AccessControl;

namespace Bank.Management.Console.Brokers.Storages.BankStorage.Customers
{
    internal class CustomerBroker : ICustomerBroker
    {

        private readonly string filePath = "../../../Assets/CustomerFileDB.txt";
        private bool isDelete;
        public CustomerBroker()
        {
            isDelete = false;
            EnsureFileExists();
        }

        public bool CloseAccountNumberForClient(decimal accountNumber)
        {
            string[] clientAllInfo = File.ReadAllLines(filePath);
            File.WriteAllText(filePath,string.Empty);

            for (int itaration = 0; itaration < clientAllInfo.Length; itaration++)
            {
                string clientInfo = clientAllInfo[itaration];
                string[] client = clientInfo.Split('*');

                if (client[1].Contains(accountNumber.ToString()) is true)
                {
                    isDelete = true;
                }
                else
                {
                    File.AppendAllText(filePath,clientInfo + "\n");
                }
            }

            return isDelete;
        }

        public bool CreateAccountNumberForClient(Customer customer)
        {
            string[] clientAllInfo = File.ReadAllLines(filePath);

            for (int itaration = 0; itaration < clientAllInfo.Length; itaration++)
            {
                string clientInfo = clientAllInfo[itaration];
                string[] client = clientInfo.Split('*');

                if (client[0].Contains(customer.Name)
                    && client[1].Contains(customer.AccountNumber.ToString()))
                {
                    return false;
                }
            }

            string newClient = $"{customer.Name}*{customer.AccountNumber}*{customer.Balance}\n";
            File.AppendAllText(filePath,newClient);
            return true;
        }

        public bool TransferMoneyBetweenAccounts(decimal firstAccountNumber, decimal secondAccountNumber, decimal money)
        {
            string[] clientInfo = File.ReadAllLines(filePath);
            File.WriteAllText(filePath,string.Empty);

            if (IsAccountNumberCheck(firstAccountNumber) 
                && IsAccountNumberCheck(secondAccountNumber))
            {
                int firstIndex = this.GetIndex(firstAccountNumber);
                int secondIndex = this.GetIndex(secondAccountNumber);

                if (Convert.ToDecimal(clientInfo[firstIndex].Split('*')[2]) >= money)
                {

                    clientInfo[firstIndex].Split('*')[2] = 
                        (Convert.ToDecimal(clientInfo[firstIndex].Split('*')[2]) - money).ToString();
                    clientInfo[secondIndex].Split('*')[2] =
                        (Convert.ToDecimal(clientInfo[secondIndex].Split('*')[2]) + money).ToString();

                    for (int itaration = 0; itaration < clientInfo.Length; itaration++)
                    {
                        string clientLineInfo = clientInfo[itaration];
                        File.AppendAllText(filePath, clientLineInfo + "\n");
                    }

                    return true;
                }
                
            }

            return false;
        }

        private bool IsAccountNumberCheck(decimal accountNumber)
        {
            string[] clientAllInfo = File.ReadAllLines(filePath);

            for (int itaration = 0; itaration < clientAllInfo.Length; itaration++)
            {
                string clietInfo = clientAllInfo[itaration];
                string[] client = clietInfo.Split('*');
                if (client[1].Contains(accountNumber.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        private int GetIndex(decimal accountNumber)
        {
            string[] clientAllInfo = File.ReadAllLines(filePath);

            for (int itaration = 0; itaration < clientAllInfo.Length; itaration++)
            {
                string clietInfo = clientAllInfo[itaration];
                string[] client = clietInfo.Split('*');
                if (client[1].Contains(accountNumber.ToString()))
                {
                    return itaration;
                }
            }

            return -1;
        }

        private void EnsureFileExists()
        {
            bool isFileThere = File.Exists(filePath);

            if (isFileThere is false)
            {
                File.Create(filePath).Close();
            }
        }
    }
}
