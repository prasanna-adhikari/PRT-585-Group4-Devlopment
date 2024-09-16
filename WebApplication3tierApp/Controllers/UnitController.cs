using _2DataAccessLayer.Services;
using _3BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3tierApp.Models;

namespace WebApplication3tierApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UnitController : BaseController
    {

        private readonly IUnitService _UnitService;
        private readonly ILogger<UnitController> _logger;

        public UnitController(IUnitService UnitService, ILogger<UnitController> logger)
        {
            _UnitService = UnitService;
            _logger = logger;
        }

        [HttpGet("", Name = "GetAllUnits")]
        public async Task<List<UnitDto>> GetAll()
        {
            var result = await _UnitService.GetAll();
            return result.Select(x => x.ToUnitDto()).ToList();
        }

        [HttpGet("{UnitId}", Name = "GetUnit")]
        public async Task<UnitDto?> Get(int UnitId)
        {
            var result = await _UnitService.GetById(UnitId);
            return result?.ToUnitDto();
        }

        [HttpPost, Route("")]
        public async Task<int> Create([FromBody] UnitDto requestDto)
        {

            var UnitModel = requestDto.ToUnitModel();
            return await _UnitService.CreateUnit(UnitModel);
        }

        [HttpPut, Route("update")]
        public async Task<IActionResult> Update([FromBody] UnitDto requestDto)
        {
            await _UnitService.UpdateUnit(requestDto.ToUnitModel());
            return Ok();
        }

        [HttpDelete, Route("{UnitId}")]
        public async Task<IActionResult> Delete(int UnitId)
        {
            await _UnitService.DeleteUnit(UnitId);
            return Ok();
        }
    }
}
