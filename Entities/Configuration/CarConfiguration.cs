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
                    Id = 1,
                    Brend = "Toyota",
                    Model = "Avensis"                   
                },
                new Car
                {
                    Id = 2,
                    Brend = "BMW",
                    Model = "5-series"
                }
                ) ;

        }
    }
}
