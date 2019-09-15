using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace ServiceBusPublisher
{
    class Program
    {
        private static QueueClient sendClient;
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Publishing {args[0]} messages...");

            string connectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
            sendClient = new QueueClient(connectionString, "queue");

            var taskList = new List<Task>();

            for (int i = 0; i < int.Parse(args[0]); i++)
            {
                var message = new Message(Encoding.UTF8.GetBytes("Hello World"));

                taskList.Add(sendClient.SendAsync(message));
            }

            await Task.WhenAll(taskList);
        }
    }
}
