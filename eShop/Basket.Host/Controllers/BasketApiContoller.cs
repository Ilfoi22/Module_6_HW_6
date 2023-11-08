using Infrastructure.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("basket.basketitem")]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketApiContoller : Controller
{
    private readonly ILogger<BasketApiContoller> _logger;
    private readonly ICatalogItemService _catalogItemService;

    public BasketApiContoller(
    ILogger<BasketApiContoller> logger,
    ICatalogItemService catalogItemService)
    {
        _logger = logger;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateProductRequest request)
    {
        var result = await _catalogItemService.AddAsync(request.Name, request.Description, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogTypeId, request.PictureFileName);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }
}
