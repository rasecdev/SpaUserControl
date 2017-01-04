using SpaUserControl.Common.Resources;
using SpaUserControl.Common.Validacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaUserControl.Domain.Models
{
    public class User
    {
        #region Ctor

        //Construtor para o Entity FrameWork, protected para continuar com a segurança para modificação dos atributos.
        protected User() { }

        public User(string name, string email)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;

            this.Email = email;
                
        }
        #endregion

        #region Properties
        //Private set para que somente a classe user possa modificar seus atributos.
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }
        #endregion

        #region Methods
        public void  SetPassword(string password, string confirmPassword)
        {
            AssertionConcern.AssertArgumentNotNull(password, Errors.InvalidPassword);
            AssertionConcern.AssertArgumentNotNull(confirmPassword, Errors.InvalidPasswordConfirmation);

            AssertionConcern.AssertArgumentEquals(password, confirmPassword, Errors.PasswordDoNotMatch);

            AssertionConcern.AssertArgumentLength(password, 6, 20, Errors.InvalidPassword);

            this.Password = PasswordAssertionConcern.Encrypt(password);
        }

        public string ResetPassword()
        {
            string password = Guid.NewGuid().ToString().Substring(0, 8);

            this.Password = PasswordAssertionConcern.Encrypt(password);

            return password;
        }

        public void ChangeName(string name)
        {
            this.Name = name;
        }

        public void Validate()
        {
            AssertionConcern.AssertArgumentLength(this.Name, 3, 60, Errors.InvalidUserName);

            EmailAssertionConcern.AssertIsValid(this.Email);

            PasswordAssertionConcern.AssertIsValid(this.Password);

        }
        #endregion
    }
}
