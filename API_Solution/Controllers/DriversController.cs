using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Solution.Controllers
{
    [Route("api/drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public DriversController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDrivers()
        {
            var drivers = _repository.Driver.GetAllDrivers(trackChanges: false);
            var driversDto = _mapper.Map<IEnumerable<Driver>>(drivers);
            return Ok(driversDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetDriver(Guid driverId)
        {
            var driver =_repository.Driver.GetDriver(driverId, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Driver with id: {driverId} doesn't exist in the database.");
                return NotFound();
            }
            var driverDto = _mapper.Map<DriverDto>(driver);
            return Ok(driverDto);
        }
    }
}
