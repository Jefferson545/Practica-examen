using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace webapi2.Models

{
    public class empleadosContext :DbContext
    {
        public empleadosContext(DbContextOptions<empleadosContext> options) : base(options)
        {

        }
        public DbSet<clientes> clientes { get; set; }

    }
}
