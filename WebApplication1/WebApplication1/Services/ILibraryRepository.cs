using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerAPI.Models;

namespace MessengerAPI.Services
{
    public interface ILibraryRepository
    {
        Library Add(Library book);
        IEnumerable<Library> GetAll();
        Library GetById(int id);
        void Delete(Library book);
        void Update(Library book);
    }
}
