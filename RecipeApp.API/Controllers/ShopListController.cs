using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.API.Models.ShopList;
using RecipeApp.Application.Interfaces;

namespace RecipeApp.Api.Controllers;

[ApiController]
[Route("api/shop-list")]
public class ShopListController(IShopListService shopListService) : ControllerBase
{
    private readonly IShopListService _shopListService = shopListService;

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetShopList(int userId)
    {
        var shopList = await _shopListService.GetUserShopListAsync(userId);
        return Ok(shopList);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddToShopList([FromBody] ShopListRequest request)
    {
        await _shopListService.AddIngredientToShopListAsync(request.UserId,request.ItemId);
        return Ok("Item added to shop list");
    }

    [Authorize]
    [HttpDelete("{itemId}")]
    public async Task<IActionResult> RemoveFromShopList(int itemId)
    {
        await _shopListService.RemoveIngredientFromShopListAsync(itemId);
        return Ok("Item removed from shop list");
    }
}
