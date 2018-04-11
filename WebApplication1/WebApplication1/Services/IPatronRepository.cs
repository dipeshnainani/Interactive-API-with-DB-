using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerAPI.Models;

namespace MessengerAPI.Services
{
    public interface IPatronRepository
    {
        Patron Add(Patron book);
        IEnumerable<Patron> GetAll();
        Patron GetById(int id);
        void Delete(Patron book);
        void Update(Patron book);
    }

}
