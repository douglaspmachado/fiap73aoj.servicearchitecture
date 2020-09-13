using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using App.Application.Interfaces;
using RabbitMQ.Client.Events;

namespace App.Infra.Providers
{
    public class ServiceMessage : IServiceMessage
    {

        private readonly IConfiguration _config;
        private ConnectionFactory factory;
        private IConnection _connection;
        private IModel _channel;

        public ServiceMessage(IConfiguration config)
        {
            _config = config;

        }

        public void GetConnectionFactory()
        {

            factory = new ConnectionFactory
            {
                HostName = _config["rabbit_host_docker_tools"],
                Port = int.Parse(_config["rabbit_porta"]),
                UserName = _config["rabbit_user"],
                Password = _config["rabbit_pwd"],
               
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

        public bool SendMessageQueue(string pQueue, string pConteudoMsg)
        {
            try
            {


                if (_channel.IsOpen)
                {

                    _channel = _connection.CreateModel();

                    _channel.QueueDeclare(queue: pQueue,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message =
                        $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - " +
                        $"Conteúdo da Mensagem: {pConteudoMsg}";

                    var body = Encoding.UTF8.GetBytes(message);

                    _channel.BasicPublish(exchange: "",
                                         routingKey: pQueue,
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

        public string ReceiveMessageQueue(string pQueue)
        {
            string retorno = string.Empty;

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
                       
                        
                    };


                     retorno = _channel.BasicConsume(queue: pQueue,
                                          autoAck: true,
                                          consumer: consumer);


                    return retorno;
                }
                else
                {
                    return retorno;
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                return retorno;
            }

        }
    }
}
