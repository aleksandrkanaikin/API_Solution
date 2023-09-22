using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Solution.Controllers
{
    [Route("api/drivers/{driverId}/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CarsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetCarsWithHelpDriver(Guid driverId)
        {
            var driver = _repository.Driver.GetDriver(driverId, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carsFromDB = _repository.Car.GetCars(driverId, trackChanges: false);
            var carsDto = _mapper.Map<IEnumerable<CarDto>>(carsFromDB);
            return Ok(carsDto);
        }

        [HttpGet("{id}")]
        public ActionResult GetCarWithHelpDriver(Guid driverId, Guid carId)
        {
            var driver = _repository.Driver.GetDriver(driverId, trackChanges: false);
            if (driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carDB = _repository.Car.GetCarById(driverId,carId, trackChanges: false);
            if(carDB == null)
            {
                _logger.LogInfo($"Car with id: {carId} doesn't exist in the database.");
                return NotFound();
            }
            var carDto = _mapper.Map<CarDto>(carDB);
            return Ok(carDto);
        }
    }
}
