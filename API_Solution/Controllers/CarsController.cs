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

        [HttpGet("{id}", Name = "GetCarForDriver")]
        public ActionResult GetCarWithHelpDriver(Guid driverId, Guid id)
        {
            var driver = _repository.Driver.GetDriver(driverId, trackChanges: false);
            if (driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carDB = _repository.Car.GetCarById(driverId,id, trackChanges: false);
            if(carDB == null)
            {
                _logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var carDto = _mapper.Map<CarDto>(carDB);
            return Ok(carDto);
        }

        [HttpPost]
        public IActionResult CreateCarForDriver(Guid driverId, [FromBody] CarForCreationDto car)
        {
            if (car == null)
            {
                _logger.LogError("CarForCreationDto object sent from client is  null.");
                return BadRequest("CarForCreationDto object is null");
            }
            var driver = _repository.Driver.GetDriver(driverId, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carEntity = _mapper.Map<Car>(car);
            _repository.Car.CreateCarForDriver(driverId,carEntity);
            _repository.Save();
            var carToReturn = _mapper.Map<CarDto>(carEntity);
            return CreatedAtRoute("GetCarForDriver", new { driverId, id = carToReturn.Id }, carToReturn);
        }
    }
}
