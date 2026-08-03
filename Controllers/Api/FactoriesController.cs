using account_web.Models;
using account_web.Services;
using Microsoft.AspNetCore.Mvc;

namespace account_web.Controllers.Api;

[ApiController]
[Route("api/[controller]/[action]")]
public class FactoriesController : ControllerBase
{
    private readonly ILogger<FactoriesController> _logger;
    private readonly FactoryServices _services;
    public FactoriesController(ILogger<FactoriesController> logger, FactoryServices services)
    {
        _logger = logger;
        _services = services;
    }
    
    // 取得所有廠區
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var factories = await _services.GetFactories();
        return Ok(factories);
    }
    
    // 取得個別廠區
    [HttpGet("{factoryId}")]
    public async Task<IActionResult> GetByFactoryId(string factoryId)
    {
        var factory = await _services.GetFactoryByFactoryId(factoryId);
        if (factory == null)
            return NotFound();
        return Ok(factory); 
    }
    // 新增廠區
    [HttpPost]
    public async Task<IActionResult> CreateFactory([FromBody] Factory factory)
    {
        // check factory if exist
        var existFactory = await _services.GetFactoryByFactoryId(factory.FactoryId);
        if (existFactory != null)
            return BadRequest(new { message = "Factory already exists." });  
        await _services.InsertFactory(factory);
        return CreatedAtAction(nameof(Get), new { factoryId = factory.FactoryId }, factory);
    }
    
    // 更新廠區
    
    // 刪除廠區
}