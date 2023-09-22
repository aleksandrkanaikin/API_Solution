using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Solution.Controllers
{
    [Route("api/cars")]
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
        public IActionResult GetCars()
        { 
            var cars = _repository.Car.GetAllCars(trackChanges: false);
            var carsDto = _mapper.Map<IEnumerable<CarDto>>(cars);
            return Ok(carsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetCar(Guid id)
        {
            var car = _repository.Car.GetCarById(id, trackChanges: false);
            if (car == null)
            {
                _logger.LogInfo($"Car with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var carDto = _mapper.Map<CarDto>(car);
                return Ok(carDto);
            }
        }
    }
}
