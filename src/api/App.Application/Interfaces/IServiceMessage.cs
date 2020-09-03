using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Interfaces
{
    public interface IServiceMessage
    {
        void GetConnectionFactory();

        bool CreateConnection();

        void CloseConnection();

        void  CreateModel();

        bool IsChannelOpen();

        bool SendMessageQueue(string pQueue, string pConteudoMsg);

    }
}
