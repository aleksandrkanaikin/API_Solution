using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class CarConfiguration: IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(
                new Car
                {
                    Id = new Guid("b9e4d52a-129a-4277-a559-37600c6da2c6"),
                    Brend = "Toyota",
                    Model = "Avensis"                   
                },
                new Car
                {
                    Id = new Guid("0e6191bc-2cbb-47ab-b4f9-246a3a7ecb7d"),
                    Brend = "BMW",
                    Model = "5-series"
                }
                ) ;

        }
    }
}
