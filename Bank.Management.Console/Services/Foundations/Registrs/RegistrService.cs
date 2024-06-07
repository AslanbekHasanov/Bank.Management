//----------------------------------------
// Great Code Team (c) All rights reserved
//----------------------------------------

using Bank.Management.Console.Brokers.Loggings;
using Bank.Management.Console.Brokers.Storages;
using Bank.Management.Console.Models;

namespace Bank.Management.Console.Services.Foundations.Registrs
{
    internal class RegistrService : IRegistrService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IRegistrBroker registrBroker;

        public RegistrService() 
        {
            this.loggingBroker = new LoggingBroker();
            this.registrBroker = new RegistrBroker();
        }
        public bool LogIn(User user)
        {
            throw new NotImplementedException();
        }

        public User SigUp(User user)
        {
            throw new NotImplementedException();
        }
    }
}
