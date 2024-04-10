using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IQueue
    {
        public Task<long> EnqueueAsync(string value, CancellationToken cancellationToken = default);
        public Task<string> DequeueAsync(CancellationToken cancellationToken = default);
    }
}
