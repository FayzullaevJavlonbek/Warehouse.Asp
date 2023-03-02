using Microsoft.EntityFrameworkCore;

namespace Warehouse.DbConstants
{
    public class AppDbContext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    string connectionString = "Host=localhost; Port=5432; Database=main-warehouse-db; User Id = postgres; Password = 2001;";
        //    optionsBuilder.UseNpgsql(connectionString);
        //}
        
    }
}
