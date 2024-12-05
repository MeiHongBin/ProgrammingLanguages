using Microsoft.EntityFrameworkCore;

namespace ProgrammingLanguages.Models
//與源專案一樣，風險較小
//OnConfiguring不會被自動生成覆蓋
{
    public partial class LanguageProjectContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("LanguageProject"));
            }
        }
    }
}
