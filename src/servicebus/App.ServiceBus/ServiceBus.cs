using App.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.ServiceBus
{
    public class ServiceBus
    {
        public readonly IServiceMessage _serviceMessage;

        public ServiceBus(IServiceMessage serviceBus)
        {
            this._serviceMessage = serviceBus;
        }

        static void Main(string[] args)
        {


            

        }
    }
}
