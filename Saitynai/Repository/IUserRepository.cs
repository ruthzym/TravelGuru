using Saitynai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynai.Repository
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task Update(User user);
        Task Delete(string id);
        Task<User> GetUser(string id);
        Task<IEnumerable<User>> GetUsers();
    }
}
