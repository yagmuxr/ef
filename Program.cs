using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace yeniyeni
{
internal class Program
{
    public class ShopContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories {get;set;}

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
        public int Id { get; set;}
        public string Name { get; set; }
    }
    static void Main(string[] args)
    {
        
    }
    static void AddProduct()
    {
        using(var db=new ShopContext())
        {
            var products = new List<Product>()
            {
             ,
             new Product{ Name="Samsung S7", Price=4000},
             new Product{ Name="Samsung S8", Price=5000},
             new Product{ Name="Samsung S9", Price=6000}
            };
            // foreach(var p in products)
            // {
            //     db.Products.Add(p);
            // }
            db.Products.AddRange(products);
            
            db.SaveChanges();
            System.Console.WriteLine(  "Veriler eklendi.");
        }
        static void AddProduct()
        {
        using(var db=new ShopContext())
        {
            var products = new Product{ Name="Samsung S7", Price=4000};
            db.Products.Add(products);
            
            db.SaveChanges();
            System.Console.WriteLine(  "Veriler eklendi.");
        }

        }
    }
}
}