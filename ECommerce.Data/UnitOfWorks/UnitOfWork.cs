using ECommerce.Data.Context;
using ECommerce.Data.Repositories.Abstracts;
using ECommerce.Data.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }




        //  1 . DisposeAsync metodu, DbContext'in asenkron olarak serbest bırakılmasını sağlar.
        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        // 2 
        public IGenericRepository<T> Repository<T>() where T : class
        {
           return new GenericRepository<T>(_dbContext);
        }


        // 3 . SaveAsync metodu, DbContext üzerindeki değişiklikleri veritabanına asenkron olarak kaydeder.
        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

// UnitOfWork sınıfı, veritabanı işlemlerini tek bir birim altında toplayarak yönetmeyi sağlar.
// DbContext'i elinde tutar
// İstenilen repository’yi üretip verir
// Tüm repository’lerin aynı DbContext’i kullanmasını sağlar




//1 metot açıklaması\\
//İş bittikten sonra veritabanı bağlantısını kapatmak.
//IAsyncDisposable → DisposeAsync metodunun zorunlu olmasını sağlar.
//DisposeAsync → DbContext’i asenkron olarak kapatmanı sağlar


//2 metot açıklaması\\
//örnek normalde isteiğim bir entity clasa erişmek için her entity clasa için ayrı bir context açılır buda gereksiz kod ve performans kaybına yol açar ama
// UnitOfWork ile tek bir context üzerinden istediğimiz entity clasa erişebiliriz



//3 metot açıklaması\\
//örnek birden fazla repository üzerinden değişiklik yapıldığında bu değişikliklerin hepsini tek seferde veritabanına kaydetmek için kullanılır 
// ama tek tek kaydetme işlemi yaparsak performans kaybı olur ve veri tutarsızlığı yaşanabilir
// İNT kullanılma sebebi ise ef core tarafından zorunlu olarak int olur çünkü burda döndüreln değer değilde döndürülen değer sayısı alınır veritabanına kaç adet gidicek
