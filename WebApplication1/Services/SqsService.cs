using Amazon.SQS.Model;
using Amazon.SQS;
using System.Text.Json;
using WebApplication1.Models;
using OrderService.Services.Interface;

namespace OrderService.Services
{
    public class SqsService : ISqsService
    {
        private readonly IAmazonSQS _sqs;
        private readonly IConfiguration _configuration;

        public SqsService(IAmazonSQS sqs, IConfiguration configuration)
        {
            _sqs = sqs;
            _configuration = configuration;
        }

        public async Task SendMessageAsync(Order order)
        {
            var message = JsonSerializer.Serialize(order);
            var request = new SendMessageRequest
            {
                QueueUrl = _configuration["AWS:SqsQueueUrl"],
                MessageBody = message
            };
            await _sqs.SendMessageAsync(request);
        }
    }
}
