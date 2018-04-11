using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerAPI.Models;

namespace MessengerAPI.Services
{
    public interface IBookRepository
    {
        Book Add(Book book);
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        void Delete(Book book);
        void Update(Book book);
    }
}
