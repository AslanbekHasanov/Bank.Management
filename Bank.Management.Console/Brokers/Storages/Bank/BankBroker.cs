//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using System.Linq;

namespace Bank.Management.Console.Brokers.Storages.Bank
{
    internal class BankBroker : IBankBroker
    {
        private readonly string filePath = "../../../Assets/BankFileDB.txt";

        public BankBroker()
        {
            EnsureFileExists();
        }

        public decimal GetBalance(decimal accountNumberForBank)
        {
            if (accountNumberForBank.ToString().Length >= 20)
            {
                string[] depositsLines = File.ReadAllLines(filePath);

                for (int itaration = 0; itaration < depositsLines.Length; itaration++)
                {
                    string depositeLine = depositsLines[itaration];
                    string[] depositeInfo = depositeLine.Split('*');

                    if (depositeInfo[0].Contains(accountNumberForBank.ToString()))
                    {
                        return Convert.ToDecimal(depositeInfo[1]);
                    }
                }

                return 0;
            }

            return 0;
        }

        public bool MakingDeposit(decimal accountNumberForBank, decimal balance)
        {
            if (accountNumberForBank.ToString().Length >= 20)
            {
                string[] depositsLines = File.ReadAllLines(filePath);

                for (int itaration = 0; itaration < depositsLines.Length; itaration++)
                {
                    string depositeLine = depositsLines[itaration];
                    string[] depositeInfo = depositeLine.Split('*');

                    if (depositeInfo[0].Contains(accountNumberForBank.ToString()))
                    {
                        depositeInfo[1] = (Convert.ToDecimal(depositeInfo[1]) + balance).ToString();
                        depositeLine = $"{depositeInfo[0]}*{depositeInfo[1]}";
                        depositsLines[itaration] = depositeLine;

                        File.Delete(filePath);
                        File.WriteAllLines(filePath, depositsLines);
                        return true;
                    }
                }

                string newDeposite = $"{accountNumberForBank}*{balance}\n";
                File.AppendAllText(filePath, newDeposite);

                return true;
            }

            return false;
        }

        public decimal WithdrawMoney(decimal accountNumberForBank, decimal balance)
        {
            if (accountNumberForBank.ToString().Length >= 20)
            {
                string[] depositsLines = File.ReadAllLines(filePath);

                for (int itaration = 0; itaration < depositsLines.Length; itaration++)
                {
                    string depositeLine = depositsLines[itaration];
                    string[] depositeInfo = depositeLine.Split('*');

                    if (depositeInfo[0].Contains(accountNumberForBank.ToString()) 
                        && Convert.ToDecimal(depositeInfo[1]) >= balance)
                    {
                        depositeInfo[1] = (Convert.ToDecimal(depositeInfo[1]) - balance).ToString();
                        depositeLine = $"{depositeInfo[0]}*{depositeInfo[1]}";
                        depositsLines[itaration] = depositeLine;

                        File.Delete(filePath);
                        File.WriteAllLines(filePath, depositsLines);
                        return balance;
                    }
                }
            }

            return 0;
        }
        private void EnsureFileExists()
        {
            bool isFileThere = File.Exists(filePath);

            if (isFileThere  is true)
            { 
                File.Create(filePath).Close();
            }
        }

    }
}
