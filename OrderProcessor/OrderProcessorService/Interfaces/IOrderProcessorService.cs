using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.OrderProcessorService.Interfaces
{
    public interface IOrderProcessorService
    {
        Task StartAsync();
    }
}
