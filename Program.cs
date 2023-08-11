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
        Console.WriteLine("Hello World!");
    }
}
}