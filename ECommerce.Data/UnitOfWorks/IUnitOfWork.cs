using ECommerce.Data.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        
        IGenericRepository<T> Repository<T>() where T : class;
        Task<int> SaveAsync();
    }
}
// IAsyncDisposable arayüzü, asenkron olarak kaynakları serbest bırakmak için bir sözleşme sağlar.
// yani IAsyncDisposable kullanırsan UnitOfWork işi bitince veritabanı bağlantısını güvenli şekilde kapatır.amaIAsyncDisposable kullanmazsan
// DbContext bazen kapanmaz, bağlantılar açık kalır, memory leak olur ve uygulama performansı bozulur