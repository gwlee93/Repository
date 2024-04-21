using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MessageBus.Options
{
    public class MessageBusOptions
    {
        public string? IP { get; set; }
        public string? Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? QueueName { get; set; }
    }    
}
