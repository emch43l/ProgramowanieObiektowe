using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Net;

namespace lab_10
{
    class Program
    {
        public record Book
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public int EditionYear { get; set; }
            public int AuthorId { get; set; }
        }

        public record Author
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        class AppContext: DbContext
        {
            public DbSet<Book> Books { get; set; }
            public DbSet<Author> Authors { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("DATASOURCE=d:\\database\\data.db");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Book>().ToTable("books").HasData(
                    new Book() { Id = 1, AuthorId = 1, EditionYear = 2020, Title = "C# 9.0" },
                    new Book() { Id = 2, AuthorId = 1, EditionYear = 2018, Title = "Asp.NET" },
                    new Book() { Id = 3, AuthorId = 2, EditionYear = 2021, Title = "Java" },
                    new Book() { Id = 4, AuthorId = 2, EditionYear = 2019, Title = "F#" }
                    );
                modelBuilder.Entity<Author>().ToTable("authors").HasData(
                    new Author() { Id = 1, Name = "Michael"},
                    new Author() { Id = 2, Name = "Boris"}
                    );
            }
        }

        static void Main(string[] args)
        {
            AppContext context = new AppContext();
            context.Database.EnsureCreated();
            Console.WriteLine(context.Authors.Find(1));
            IQueryable<Book> books = from b in context.Books select b;
            var booksWithAuthors = from book in context.Books join author in context.Authors on book.AuthorId equals author.Id select new { Title = book.Title, Author = author.Name };
            var booksIdsWithAuthors = from book in context.Books join author in context.Authors on book.AuthorId equals author.Id where book.EditionYear > 2019 select new { Title = book.Id, Author = author.Name };

            string xml =
                "<books>" +
                    "<book>" +
                        "<id>1</id>" +
                        "<title>C#</title>" +
                        "<editionYear>2000</editionYear>" +
                    "</book>" +
                    "<book>" +
                        "<id>2</id>" +
                        "<title>Java</title>" +
                        "<editionYear>2002</editionYear>" +
                    "</book>" +
                "</books>";

            XDocument doc = XDocument.Parse(xml);
            IEnumerable<XElement> enumerable = doc.Elements("books").Elements("book");

            WebClient client = new WebClient();
            client.Headers.Set("Accept", "application/xml");
            string xmlRaid = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C/");
            XDocument raidDoc = XDocument.Parse(xmlRaid);
            IEnumerable<XElement> raid = raidDoc.Elements("ArrayOfExchangeRatesTable").Elements("ExchangeRatesTable").Elements("Rates").Elements("Rate").Select(x => new { 
                Code = x.Element("Code").Value,
                Ask = x.Element("Ask").Value,
                Bid = x.Element("Bid").Value
            });
            foreach(var rate in raid)
            {
                Console.WriteLine(rate);
            }
            /// api.nbp.pl - do domu zapytanie
        }
    }

    
}
