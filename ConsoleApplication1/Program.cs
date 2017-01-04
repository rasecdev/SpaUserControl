using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User(
                "Anderson",
                "anderson@anderson.com.br"
                );

            user.SetPassword("andersoncesar", "andersoncesar");
            user.Validate();

            using(IUserRepository userRep = new UserRepository())
            {
                userRep.Create(user);

            }

            using (IUserRepository userRep = new UserRepository())
            {
                var usr = userRep.Get("anderson@anderson.com.br");
                Console.WriteLine(usr.Email);
            }

        }
    }
}
