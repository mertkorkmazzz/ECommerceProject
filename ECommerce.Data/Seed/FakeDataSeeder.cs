using Bogus;
using ECommerce.Data.Context;
using ECommerce.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Seed
{
    public static class FakeDataSeeder
    {
        public static async Task SeedAsync(AppDbContext dbContext)
        {
            if (dbContext.Users.Any())
                return;

            //rulefor nedir : belirli bir özelliğe sahte veri atamak için kullanılır. 


            //Category
            var categoryfaker = new Faker<Category>("tr")
                .RuleFor(x => x.Name, f => f.Commerce.Categories(1)[0]) // commerce nedir : sahte ticaret verileri oluşturmak için kullanılır.bogus kütüphanesinin bir parçasıdır.
                .RuleFor(x => x.Description, f => f.Lorem.Sentence()); // lorem metin oluşturmak için kullanılır.sentence ise tek cümle oluşturur.


            var categories = categoryfaker.Generate(5);
            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();




            //Product
            var productfaker = new Faker<Product>("tr")
                .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                .RuleFor(x => x.Price, f => f.Random.Decimal(50, 5000))
                .RuleFor(x => x.Stock, f => f.Random.Int(1, 200))
                .RuleFor(x => x.CategoryId, f => f.PickRandom(categories).Id); // hangi ürünün hangi kategoriye ait olduğunu belirlemek için kullanılır.


            var products = productfaker.Generate(50);
            await dbContext.Products.AddRangeAsync(products);
            await dbContext.SaveChangesAsync();



            //User
            var userfaker = new Faker<User>("tr")
                .RuleFor(x => x.Name, f => f.Name.FullName())
                .RuleFor(x => x.Email, f => f.Internet.Email())
                .RuleFor(x => x.PssswordHash, f => "hashed_password");


            var users = userfaker.Generate(20);
            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();



            //Cart

            var carts = users.Select(u => new Cart
            {
                UserId = u.Id
            }).ToList();

            await dbContext.Carts.AddRangeAsync(carts);
            await dbContext.SaveChangesAsync();



            //Cart ıtem
            var cartItemFaker = new Faker<CartItem>("tr")
           .RuleFor(x => x.CartId, f => f.PickRandom(carts).Id)
           .RuleFor(x => x.ProductId, f => f.PickRandom(products).Id)
           .RuleFor(x => x.Quantity, f => f.Random.Int(1, 5));

            var cartItems = cartItemFaker.Generate(30);
            await dbContext.CartItems.AddRangeAsync(cartItems);
            await dbContext.SaveChangesAsync();



            // 6. ORDER
            var orderFaker = new Faker<Order>("tr")
                .RuleFor(x => x.UserId, f => f.PickRandom(users).Id)
                .RuleFor(x => x.OrderDate, f => f.Date.Recent(30))
                .RuleFor(x => x.TotalPrice, 0);

            var orders = orderFaker.Generate(15);
            await dbContext.Orders.AddRangeAsync(orders);
            await dbContext.SaveChangesAsync();

            // 7. ORDER ITEM
            var orderItemFaker = new Faker<OrderItem>("tr")
                .RuleFor(x => x.OrderId, f => f.PickRandom(orders).Id)
                .RuleFor(x => x.ProductId, f => f.PickRandom(products).Id)
                .RuleFor(x => x.Quantity, f => f.Random.Int(1, 5))
                .RuleFor(x => x.UnitPrice, f => f.Random.Int(50, 5000));

            var orderItems = orderItemFaker.Generate(40);
            await dbContext.OrderItems.AddRangeAsync(orderItems);
            await dbContext.SaveChangesAsync();

            // 8. PAYMENT
            var paymentFaker = new Faker<Payment>("tr")
                .RuleFor(x => x.OrderId, f => f.PickRandom(orders).Id)
                .RuleFor(x => x.Amount, f => f.Random.Decimal(100, 10000))
                .RuleFor(x => x.PaymentDate, f => f.Date.Recent(10))
                .RuleFor(x => x.PaymentMethod, f => f.PickRandom(new[] { "Kredi Kartı", "Havale", "Kapıda Ödeme" }))
                .RuleFor(x => x.IsPaid, true);

            var payments = paymentFaker.Generate(15);
            await dbContext.Payments.AddRangeAsync(payments);
            await dbContext.SaveChangesAsync();

            // 9. SHIPPING
            var shippingFaker = new Faker<Shipping>("tr")
                .RuleFor(x => x.OrderId, f => f.PickRandom(orders).Id)
                .RuleFor(x => x.Address, f => f.Address.FullAddress())
                .RuleFor(x => x.City, f => f.Address.City())
                .RuleFor(x => x.PostalCode, f => f.Address.ZipCode())
                .RuleFor(x => x.ShippedDate, f => f.Date.Recent(7))
                .RuleFor(x => x.Status, f => f.PickRandom(new[] { "Hazırlanıyor", "Yolda", "Teslim Edildi" }));

            var shippings = shippingFaker.Generate(15);
            await dbContext.Shippings.AddRangeAsync(shippings);
            await dbContext.SaveChangesAsync();
        }
    }
}
