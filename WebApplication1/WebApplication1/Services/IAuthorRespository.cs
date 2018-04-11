using MessengerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerAPI.Services
{
    public interface IAuthorRespository
    {
        Author Add(Author book);
        IEnumerable<Author> GetAll();
        Author GetById(int id);
        void Delete(Author book);
        void Update(Author book);
    }
}
