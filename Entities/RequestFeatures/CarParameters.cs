namespace Entities.RequestFeatures
{
    public class CarParameters: RequestParameters
    {
        public CarParameters() 
        {
            OrderBy = "brend";
        }

        public string FirstCarBrand { get; set; } = "A";
        public string LastCarBrand { get; set; } = "Z";
        public string SearchTerm { get; set; }

    }
}
