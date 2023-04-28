using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AsyncProductApi.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()
            .UseSqlite("Data Source=RequestDb.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}
