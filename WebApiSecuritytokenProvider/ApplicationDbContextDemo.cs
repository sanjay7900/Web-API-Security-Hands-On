using Microsoft.EntityFrameworkCore;
using WebApiSecuritytokenProvider.ModelForAuth;

namespace WebApiSecuritytokenProvider
{
    public class ApplicationDbContextDemo:DbContext
    {
        public virtual DbSet<UserApiAuthntication>?  User { set; get; }
        public ApplicationDbContextDemo()
        {

        }
        public ApplicationDbContextDemo(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-AMR2CQS\MSSQLSERVER01;Initial Catalog=WebApiDatabase;Integrated Security=True");
        }

    }
}
