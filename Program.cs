using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace yeniyeni
{
    internal class Program
    {
        public class ShopContext : DbContext
        {
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Order> Orders {get; set;}
            public DbSet<User> Users {get;set;}
            public DbSet<Address> Addresses {get; set;}

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Data Source=YAGMURSMATEBOOK\SQLEXPRESS;Initial Catalog=ShopDb;Integrated Security=SSPI; TrustServerCertificate=true");
            }
        }
        public class User
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public List<Address> Addresses { get; set; }
        }
        public class Address
        {
            public int Id { get; set; }
            public string Fullname { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public User User {get;set;}
            public int? UserId { get; set; }
        }


        public class Product
        {
            public int ProductId { get; set; }

            [MaxLength(100)]
            [Required]
            public string Name { get; set; }

            public decimal Price { get; set; }
            public int CategoryId { get; set; }
        }

        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class Order
        {
            public int OrderId { get; set;}
            public int ProductId { get; set; }
            public DateTime DateAdded { get; set; }
        }

        static void Main(string[] args)
        {
            // AddProducts();
            // AddProduct();
            // GetAllProducts();
            // GetProductById(1);
            // GetProductByName("5");
            // UpdateProduct();
            // DeleteProduct(2);
        }

        static void AddProducts()
        {
            using (var db = new ShopContext())
            {
                var products = new List<Product>()
                {
                    new Product { Name = "Samsung S7", Price = 4000 },
                    new Product { Name = "Samsung S8", Price = 5000 },
                    new Product { Name = "Samsung S9", Price = 6000 }
                };
                db.Products.AddRange(products);
                db.SaveChanges();
                Console.WriteLine("Veriler eklendi.");
            }
        }

        static void AddProduct()
        {
            using (var db = new ShopContext())
            {
                var product = new Product { Name = "Samsung S7", Price = 4000 };
                db.Products.Add(product);
                db.SaveChanges();
                Console.WriteLine("Veri eklendi.");
            }
        }

        static void GetAllProducts()
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.ToList();
                foreach (var p in products)
                {
                    Console.WriteLine($"Name: {p.Name} Price: {p.Price}");
                }
            }
        }
         static void GetProductById(int id)
        {
            using (var context = new ShopContext())
            {
                var product = context.Products.Where(p => p.ProductId == id).FirstOrDefault();
                
                System.Console.WriteLine($"name: {product.Name} price: {product.Price}");
                
            }
        }
        static void GetProductByName(string name)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .Select(p => new
    {
        p.Name,
        p.Price
    })
                .ToList();
                foreach(var product in products)
                {
                System.Console.WriteLine($"name: {product.Name} price: {product.Price}");
                }
            }
        }
        static void UpdateProduct()
        {
            using(var db = new ShopContext())
            {
                var p = db.Products.Where(p => p.ProductId ==1).FirstOrDefault();
                if(p!=null)
                {
                    p.Price=2400;
                    db.Products.Update(p); //tüm satırlar güncellenir. o yğzden aşağıdakini kullanmak daha mmantıklı, sadece güncellenmek istenen satır güncellenir.
                    db.SaveChanges();
                    System.Console.WriteLine("guncellendi");
                }
            }
            // using(var db = new ShopContext())
            // {
            //     var entity = new Product(){ProductId=1};
            //     db.Products.Attach(entity); //bir tracking başlatır!
            //     entity.Price = 3000;
            //     db.SaveChanges();
            //     System.Console.WriteLine("guncellendi.");
            // }
            // using(var db= new ShopContext())
            // {
            
            //     var p =db.Products.Where(i => i.ProductId == 2).FirstOrDefault();
            //     if(p!=null)
            //     {
            //         p.Price*=1.2m;
            //         db.SaveChanges();
            //         System.Console.WriteLine("güncelleme yapıldı.");
            //     }
            // }
        }
        static void DeleteProduct(int id)
        {
            using(var db = new ShopContext())
          {
            var p = new Product(){ProductId=6};
            db.Products.Remove(p);
            // db.Entry(p).State = EntityState.Deleted;
            db.SaveChanges();

            
          }
        //   using(var db = new ShopContext())
        //   {
        //     var p = db.Products.Where(i => i.ProductId ==id).FirstOrDefault();
        //     if(p!=null)
        //     {
        //        db.Products.Remove(p);
        //        db.SaveChanges();
        //        System.Console.WriteLine("silindi.");
        //     }
        //   }
        }
    }
}
