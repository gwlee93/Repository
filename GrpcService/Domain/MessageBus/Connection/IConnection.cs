using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MessageBus.Connection
{
    public interface IConnection
    {
        
        void CreateConnection();
        void CloseConnection();
    }
}
