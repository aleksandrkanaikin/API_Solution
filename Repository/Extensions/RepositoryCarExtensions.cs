using Entities.Models;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryCarExtensions
    {

        public static IQueryable<Car> FilterCar(this IQueryable<Car> cars, string firstCarBrend, string lastCarBrend) =>
                cars.Where(e => (e.Brend[0] >= firstCarBrend[0] && e.Brend[0] <= lastCarBrend[0]));
        public static IQueryable<Car> Search(this IQueryable<Car> cars, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return cars;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return cars.Where(e => e.Brend.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Car> Sort(this IQueryable<Car> cars, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return cars.OrderBy(e => e.Brend);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Car>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
                return cars.OrderBy(e => e.Brend);
            return cars.OrderBy(orderQuery);
        }
    }
}