//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Models;

namespace Bank.Management.Console.Brokers.Storages.RegistrsStorage
{
    internal interface IRegistrBroker
    {
        User AddUser(User user);
        bool LogIn(User user);
    }
}
