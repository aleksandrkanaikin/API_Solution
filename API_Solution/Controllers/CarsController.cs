using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<ActionResult> GetCarsWithHelpDriver(Guid driverId)
        {
            var driver = _repository.Driver.GetDriverAsync(driverId, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carsFromDB = await _repository.Car.GetCarsAsync(driverId, trackChanges: false);
            var carsDto = _mapper.Map<IEnumerable<CarDto>>(carsFromDB);
            return Ok(carsDto);
        }

        [HttpGet("{id}", Name = "GetCarForDriver")]
        public async Task<ActionResult> GetCarWithHelpDriver(Guid driverId, Guid id)
        {
            var driver = _repository.Driver.GetDriverAsync(driverId, trackChanges: false);
            if (driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carDB = await _repository.Car.GetCarByIdAsync(driverId,id, trackChanges: false);
            if(carDB == null)
            {
                _logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var carDto = _mapper.Map<CarDto>(carDB);
            return Ok(carDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCarForDriverAsync(Guid driverId, [FromBody] CarForCreationDto car)
        {
            if (car == null)
            {
                _logger.LogError("CarForCreationDto object sent from client is  null.");
                return BadRequest("CarForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CarForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var driver = await _repository.Driver.GetDriverAsync(driverId, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carEntity = _mapper.Map<Car>(car);
            _repository.Car.CreateCarForDriver(driverId,carEntity);
            await _repository.SaveAsync();
            var carToReturn = _mapper.Map<CarDto>(carEntity);
            return CreatedAtRoute("GetCarForDriver", new { driverId, id = carToReturn.Id }, carToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarForDriver(Guid driverId, Guid id) 
        { 
            var driver = await _repository.Driver.GetDriverAsync(driverId, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carForDriver = await _repository.Car.GetCarByIdAsync(driverId, id, trackChanges: false);
            if (carForDriver == null)
            {
                _logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Car.DeleteCar(carForDriver);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarForDriver(Guid driverId, Guid id, [FromBody] CarForUpdateDto car)
        {
            if (car == null)
            {
                _logger.LogError("CarForUpdateDto object sent from client is null.");
                return BadRequest("CarForUpdateDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CarForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var driver = await _repository.Driver.GetDriverAsync(driverId, trackChanges: false);
            if (driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carEntity = await _repository.Car.GetCarByIdAsync(driverId, id, trackChanges: true);
            if (carEntity == null)
            {
                _logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(car, carEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateCarForDriver(Guid driverId, Guid id, [FromBody] JsonPatchDocument<CarForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var driver =await _repository.Driver.GetDriverAsync(driverId, trackChanges: false);
            if (driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var carEntity = await _repository.Car.GetCarByIdAsync(driverId, id, trackChanges: true);
            if (carEntity == null)
            {
                _logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var carToPatch = _mapper.Map<CarForUpdateDto>(carEntity);
            patchDoc.ApplyTo(carToPatch, ModelState);
            TryValidateModel(carToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the CarForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(carToPatch, carEntity);
            await _repository.SaveAsync();
            return NoContent(); 
        }
    }
}
