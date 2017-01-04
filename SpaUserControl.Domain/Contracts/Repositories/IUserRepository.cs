using SpaUserControl.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaUserControl.Domain.Contracts.Repositories
{
    //Herdar do Idisposable para fechar a conexão com o banco, após uma requisição.
    public interface IUserRepository : IDisposable
    {
        User Get(string email);

        User Get(Guid id);

        void Create(User user);

        void Update(User user);

        void Delete(User user);
    }
}
