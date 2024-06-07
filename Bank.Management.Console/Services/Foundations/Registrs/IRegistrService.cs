//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Models;

namespace Bank.Management.Console.Services.Foundations.Registrs
{
    internal interface IRegistrService
    {
        bool LogIn(User user);
        User SigUp(User user);
    }
}
