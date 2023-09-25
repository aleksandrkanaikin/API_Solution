using API_Solution.ModelBinders;
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

        [HttpGet("{id}", Name = "DriverById")]
        public IActionResult GetDriver(Guid id)
        {
            var driver =_repository.Driver.GetDriver(id, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Driver with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var driverDto = _mapper.Map<DriverDto>(driver);
            return Ok(driverDto);
        }

        [HttpPost]
        public IActionResult CreateDriver([FromBody] DriverForCreatonDto driver) 
        {
            if(driver == null)
            {
                _logger.LogError("DriverForCreatonDto object sent from client is  null.");
                return BadRequest("DriverForCreatonDto object is null");
            }
            var driverEntity = _mapper.Map<Driver>(driver);
            _repository.Driver.CreateDriver(driverEntity);
            _repository.Save();
            var driverToReturn = _mapper.Map<DriverDto>(driverEntity);
            return CreatedAtRoute("DriverById", new { id = driverToReturn.Id }, driverToReturn);
        }

        [HttpGet("collection/({ids})", Name = "DriverCollection")]
        public IActionResult GetDriverCollection(IEnumerable<Guid> ids) 
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var driverEntities = _repository.Driver.GetByIds(ids, trackChanges: false);
            if (ids.Count() != driverEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var driversToReturn = _mapper.Map<IEnumerable<DriverDto>> (driverEntities);
            return Ok(driversToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateDriverCollection([ModelBinder (BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> driverCollection)
        {
            if (driverCollection == null)
            {
                _logger.LogError("Driver collection sent from client is null.");
                return BadRequest("Driver collection is null");
            }
            var driverEntitiees = _mapper.Map<IEnumerable<Driver>>(driverCollection);
            foreach (var driver in driverEntitiees)
            {
                _repository.Driver.CreateDriver(driver);
            }
            _repository.Save();
            var driverCollectionToReturn = _mapper.Map<IEnumerable<DriverDto>>(driverEntitiees);
            var ids = string.Join(",", driverCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("DriverCollection", new { ids }, driverCollectionToReturn);
        }
    }
}
