using Amazon.Runtime;
using Amazon.SQS.Model;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessor.OrderProcessorService.Interfaces;

namespace OrderProcessor.OrderProcessorService
{
    public class OrderProcessorService : IOrderProcessorService
    {
        private readonly IAmazonSQS _sqs;
        private readonly string _queueUrl;
        private readonly IConfiguration _configuration;

        public OrderProcessorService(IConfiguration configuration, IAmazonSQS sqs)
        {
            _configuration = configuration;
            _sqs = sqs;
            _queueUrl = configuration["AWS:SqsQueueUrl"];

        }

        public async Task StartAsync()
        {
            Console.WriteLine("Listening for messages...");

            while (true)
            {
                var request = new ReceiveMessageRequest
                {
                    QueueUrl = _queueUrl,
                    MaxNumberOfMessages = 1,
                    WaitTimeSeconds = 10
                };

                var response = await _sqs.ReceiveMessageAsync(request);

                if (response.Messages != null && response.Messages.Any())
                {
                    foreach (var msg in response.Messages)
                    {
                        Console.WriteLine($"[Received]: {msg.Body}");

                        // Simulate processing
                        await Task.Delay(1000);

                        await _sqs.DeleteMessageAsync(_queueUrl, msg.ReceiptHandle);
                        Console.WriteLine("[Deleted from queue]");
                    }
                }
            }
        }
    }
}
