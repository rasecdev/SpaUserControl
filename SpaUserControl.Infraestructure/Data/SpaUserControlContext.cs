using SpaUserControl.Domain.Models;
using SpaUserControl.Infraestructure.Data.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaUserControl.Infraestructure.Data
{
    public class SpaUserControlContext : DbContext
    {
        public SpaUserControlContext()
            : base("SpaUserControlConnectionString")
        {

        }

        public DbSet<User> Users { get; set; }

        //Quando for criado um novo User o On ModelCreating irá usar as configurações do UserMap.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
