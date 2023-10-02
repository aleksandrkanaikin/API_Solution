using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Solution.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/drivers")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]

    public class DriversV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public DriversV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetDrivers()
        {
            var drivers = await _repository.Driver.GetAllDriversAsync(trackChanges: false);
            return Ok(drivers);
        }
    }
}
