using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SaveAsync();
    }
}
