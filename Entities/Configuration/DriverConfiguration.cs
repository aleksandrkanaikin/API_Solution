using System;
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
                    Id = new Guid("305a8736-8187-4854-8686-f6869493b302"),
                    Name= "Aleksandr Kanaikin",
                    Address= "Voroshilova 5",
                    CarId = new Guid("b9e4d52a-129a-4277-a559-37600c6da2c6")
                },
                new Driver 
                {
                    Id = new Guid("27feac3d-b9d9-429f-8ca4-a520513fa714"),
                    Name = "Ruslan Palytin",
                    Address = "Volgogradskaya 74",
                    CarId = new Guid("0e6191bc-2cbb-47ab-b4f9-246a3a7ecb7d")
                }
                );
        }
    }
}
