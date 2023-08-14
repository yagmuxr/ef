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

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=shop.db");
            }
        }

        public class Product
        {
            public int ProductId { get; set; }

            [MaxLength(100)]
            [Required]
            public string Name { get; set; }

            public decimal Price { get; set; }
        }

        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        static void Main(string[] args)
        {
            // AddProducts();
            // AddProduct();
            // GetAllProducts();
            // GetProductById(1);
            GetProductByName("5");
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
    }
}
