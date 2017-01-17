using Microsoft.Practices.Unity;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Domain.Contracts.Service;
using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Data;
using SpaUserControl.Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaUserControl.Startup
{
    public static class DependecyResolver
    {
        //Usando o Unity (pattern and practices da Microsoft) para a resolução de dependência.
        public static void Resolve(UnityContainer container)
        {
            container.RegisterType<SpaUserControlContext, SpaUserControlContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
                        
        }
    }
}
