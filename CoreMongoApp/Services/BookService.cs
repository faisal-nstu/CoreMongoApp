using CoreMongoApp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMongoApp.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings?.ConnectionString);

            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get()
        {
            var books = _books.Find(book => true).ToList();
            return books;
        }
        
        public Book Get(string id)
        {
            var book = _books.Find<Book>(book => book.Id == id).FirstOrDefault();
            return book;
        }

        public void Create(Book book)
        {
            _books.InsertOne(book);
        }

        public void Update(string id, Book book)
        {
            _books.ReplaceOne(b => b.Id == id, book);
        }

        public void Remove(string id)
        {
            _books.DeleteOne(book => book.Id == id);
        }
    }
}
