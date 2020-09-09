using System;
using System.Collections.Generic;
using System.Text;
using App.Infra.Providers.Mensageria;

namespace App.ServiceBus
{
    class Program
    {

        static void Main(string[] args)
        {
            

            ServiceBus serviceRabbit = new ServiceBus();
            

            serviceRabbit.GetConnectionFactory();
            serviceRabbit.CreateConnection();
            serviceRabbit.CreateModel();

            if (serviceRabbit.IsChannelOpen())
            {
                while (true)
                {
                    serviceRabbit.ReceiveMessageQueue(QueueMessage.ABERTURA_CHAMADO);

                }

            }

        }

    }
}
