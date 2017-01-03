using SpaUserControl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaUserControl.Domain.Contracts.Repositories
{
    public interface IUserRepository
    {
        User Get(string email);

        User Get(Guid id);

        void Create(User user);

        void Update(User user);

        void Delete(User user);
    }
}
