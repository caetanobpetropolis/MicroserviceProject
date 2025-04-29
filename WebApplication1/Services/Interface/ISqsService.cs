using WebApplication1.Models;

namespace OrderService.Services.Interface
{
    public interface ISqsService
    {
        Task SendMessageAsync(Order order);
    }
}
