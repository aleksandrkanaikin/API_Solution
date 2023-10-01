using API_Solution.ActionFilters;
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
        public async Task<IActionResult> GetDrivers()
        {
            var drivers = await _repository.Driver.GetAllDriversAsync(trackChanges: false);
            var driversDto = _mapper.Map<IEnumerable<Driver>>(drivers);
            return Ok(driversDto);
        }

        [HttpGet("{id}", Name = "DriverById")]
        public async Task<IActionResult> GetDriverAsync(Guid id)
        {
            var driver = await _repository.Driver.GetDriverAsync(id, trackChanges: false);
            if(driver == null)
            {
                _logger.LogInfo($"Driver with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var driverDto = _mapper.Map<DriverDto>(driver);
            return Ok(driverDto);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDriverAsync([FromBody] DriverForCreatonDto driver) 
        {            
            var driverEntity = _mapper.Map<Driver>(driver);
            _repository.Driver.CreateDriver(driverEntity);
            await _repository.SaveAsync();
            var driverToReturn = _mapper.Map<DriverDto>(driverEntity);
            return CreatedAtRoute("DriverById", new { id = driverToReturn.Id }, driverToReturn);
        }

        [HttpGet("collection/({ids})", Name = "DriverCollection")]
        public async Task<IActionResult> GetDriverCollection(IEnumerable<Guid> ids) 
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var driverEntities = await _repository.Driver.GetByIdsAsync(ids, trackChanges: true);
            if (ids.Count() != driverEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var driversToReturn = _mapper.Map<IEnumerable<DriverDto>> (driverEntities);
            return Ok(driversToReturn);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateDriverCollection([ModelBinder (BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> driverCollection)
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
            await _repository.SaveAsync();
            var driverCollectionToReturn = _mapper.Map<IEnumerable<DriverDto>>(driverEntitiees);
            var ids = string.Join(",", driverCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("DriverCollection", new { ids }, driverCollectionToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateDriverExistsAtribute))]
        public async Task<IActionResult> DeleteDriver(Guid id)
        {
            var driver = HttpContext.Items["driver"] as Driver;
            _repository.Driver.DeleteDriver(driver);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateDriverExistsAtribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] DriverForUpdateDto driver)
        {            
            var driverEntity = HttpContext.Items["driver"] as Driver;
            _mapper.Map(driver, driverEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetDriversOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
    }
}
