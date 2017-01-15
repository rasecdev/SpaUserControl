using SpaUserControl.Domain.Contracts.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaUserControl.Domain.Models;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Common.Validacion;
using SpaUserControl.Common.Resources;

namespace SpaUserControl.Business.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public string PasswordAsser { get; private set; }

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Authenticate(string email, string password)
        {
            var user = GetByEmail(email);

            if (user.Password != PasswordAssertionConcern.Encrypt(password))
                throw new Exception(Errors.InvalidCredentials);

            return user;
        }

        public void ChangeInformation(string email, string name)
        {
            var user = GetByEmail(email);

            user.ChangeName(name);

            user.Validate();

            _repository.Update(user);
        }

        public void ChangePassword(string email, string password, string newPassword, string confirmPassword)
        {
            var user = Authenticate(email, password);

            user.SetPassword(password, confirmPassword);

            user.Validate();

            _repository.Update(user);
        }

        public User GetByEmail(string email)
        {
            var user = _repository.Get(email);

            if (user == null)
                throw new Exception(Errors.UserNotFound);

            return user;           
        }

        public void Register(string name, string email, string password, string confirmPassword)
        {
            var hasUser = GetByEmail(email);

            if (hasUser != null)
                throw new Exception(Errors.DuplicateEmail);

            var user = new User(name, email);
            user.SetPassword(password, confirmPassword);
            user.Validate();

            _repository.Create(user);
            
        }

        public string resetPassword(string email)
        {
            var user = GetByEmail(email);

            var password = user.ResetPassword();
            user.Validate();

            _repository.Update(user);
            return password;
        }

        public void Dispose()
        {
            //Para fechar todas as conexões.
            _repository.Dispose();
        }
    }
}
