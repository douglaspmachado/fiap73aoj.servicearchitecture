using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using App.Application.Interfaces;

namespace App.Infra.Providers
{
    public class ServiceMessage : IServiceMessage
    {

        private readonly IConfiguration _config;
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;

        public ServiceMessage(IConfiguration config)
        {
            _config = config;

        }

        public void GetConnectionFactory()
        {

            factory = new ConnectionFactory
            {
                HostName = _config.GetConnectionString("rabbit_host_docker_tools"),
                Port = int.Parse(_config.GetConnectionString("rabbit_porta")),
                UserName = _config.GetConnectionString("rabbit_user"),
                Password = _config.GetConnectionString("rabbit_pwd"),


            };

        }
               
        public bool CreateConnection()
        {

            try
            {
                connection = factory.CreateConnection();
                return true;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                return false;
            }

        }

        public void CloseConnection()
        {
            try
            {
                if (connection.IsOpen)
                {
                    connection.Close();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public void CreateModel()
        {
            try
            {
                if (connection.IsOpen)
                {

                    channel = connection.CreateModel();

                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public bool IsChannelOpen()
        {
            return channel.IsOpen;
        }

        public bool SendMessageQueue(string pQueue, string pConteudoMsg)
        {
            try
            {


                if (channel.IsOpen)
                {

                    channel = connection.CreateModel();

                    channel.QueueDeclare(queue: pQueue,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message =
                        $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - " +
                        $"Conteúdo da Mensagem: {pConteudoMsg}";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "TestesASPNETCore",
                                         basicProperties: null,
                                         body: body);

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                return false;
            }

        }
    }
}
