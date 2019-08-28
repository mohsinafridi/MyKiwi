using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class KiwiRepository : IKiwiRepository
    {
         public readonly DataContext _context ;
        public KiwiRepository(DataContext context)
        {
            this._context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public Task<User> GetUser(int Id)
        {
          var user = _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(x=>x.Id==Id);
           return user;        
         }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users =await _context.Users.Include(p=>p.Photos).ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
           return await _context.SaveChangesAsync() > 0 ;
        }
    }
}