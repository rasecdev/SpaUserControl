using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaUserControl.Domain.Models;

namespace SpaUserControl.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private SpaUserControlContext _context = new SpaUserControlContext();

        public User Get(Guid id)
        {
            return _context.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public User Get(string email)
        {
            return _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
