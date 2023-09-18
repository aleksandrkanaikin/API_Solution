using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class DriverConfiguration: IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasData(
                new Driver
                {
                    Id = 1,
                    Name= "Aleksandr Kanaikin",
                    Address= "Voroshilova 5"
                },
                new Driver
                {
                    Id = 2,
                    Name = "Ruslan Palytin",
                    Address = "Volgogradskaya 74"
                }
                );
        }
    }
}
