using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketBffController : Controller
{
    private readonly ILogger<BasketBffController> _logger;

    public BasketBffController(ILogger<BasketBffController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult LogTestMessage()
    {
        _logger.LogInformation("This is the anonymous message");
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult GetBrands()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;

        if (userId is not null)
        {
            _logger.LogInformation($"User Id {userId}");
        }
        else
        {
            _logger.LogWarning("User Id not found in claims");
        }

        return Ok();
    }
}
