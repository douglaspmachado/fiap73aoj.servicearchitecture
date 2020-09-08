using App.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using System.IO;
using RabbitMQ.Client.Events;

namespace App.ServiceBus
{
    public class ServiceBus
    {

        private ConnectionFactory factory;
        private IConnection _connection;
        private IModel _channel;
        private IConfigurationBuilder _builder;
        private IConfigurationRoot _configuration;

        public ServiceBus()
        {
            _builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Config.json");

            _configuration = _builder.Build();


        }

        public void GetConnectionFactory()
        {

            factory = new ConnectionFactory
            {
                HostName = _configuration["rabbit_host_docker_tools"],
                Port = int.Parse(_configuration["rabbit_porta"]),
                UserName = _configuration["rabbit_user"],
                Password = _configuration["rabbit_pwd"],

            };

        }


        public bool CreateConnection()
        {

            try
            {
                _connection = factory.CreateConnection();
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
                if (_connection.IsOpen)
                {
                    _connection.Close();
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
                if (_connection.IsOpen)
                {

                    _channel = _connection.CreateModel();

                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public bool IsChannelOpen()
        {
            return _channel.IsOpen;
        }

        public bool ReceiveMessageQueue(string pQueue)
        {
            try
            {


                if (_channel.IsOpen)
                {

                    _channel.QueueDeclare(queue: pQueue,
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);

                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received from Rabbit: {0}", message);
                    };

                    string retorno = _channel.BasicConsume(queue: pQueue,
                                            autoAck: true,
                                            consumer: consumer);



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
