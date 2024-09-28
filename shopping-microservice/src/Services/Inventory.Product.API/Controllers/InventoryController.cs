using BuildingBlocks.Core.SeedWork;
using Inventory.Product.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.Inventory;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IIventoryService _inventoryService;

    public InventoryController(IIventoryService inventoryService)
    {
        this._inventoryService = inventoryService;
    }

    [HttpGet]
    [Route("items/{itemNo}", Name = "GetAllByItemNo")]
    [ProducesResponseType(typeof(IEnumerable<InventoryEntryDto>), 200)]
    public async Task<ActionResult<IEnumerable<InventoryEntryDto>>> GetAllByItemNo([Required] string itemNo)
    {
        var result = await _inventoryService.GetAllByItemNoAsync(itemNo);
        return Ok(result);
    }

    [HttpGet]
    [Route("items/{itemNo}/paging", Name = "GetAllByItemNoPaging")]
    [ProducesResponseType(typeof(PagedList<InventoryEntryDto>), 200)]
    public async Task<ActionResult<IEnumerable<InventoryEntryDto>>> GetAllByItemNoPaging([Required] string itemNo, [FromQuery] GetInventoryPagingQuery query)
    {
        query.SetItemNo(itemNo);

        var result = await _inventoryService.GetAllByItemNoPagingAsync(query);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}", Name = "GetInventoryById")]
    [ProducesResponseType(typeof(InventoryEntryDto), 200)]
    public async Task<ActionResult<IEnumerable<InventoryEntryDto>>> GetById([Required] string id)
    {
        var result = await _inventoryService.GetByIdAsync(id);
        return Ok(result);
    }


    [HttpPost]
    [Route("purchase/{itemNo}", Name = "PurchaseInventory")]
    [ProducesResponseType(typeof(InventoryEntryDto), 200)]
    public async Task<ActionResult<InventoryEntryDto>> Purchase([Required] string itemNo, [FromBody] PurchaseProductDto model)
    {
        var result = await _inventoryService.PurchaseItemAsync(itemNo, model);
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}", Name = "PurchaseInventory")]
    [ProducesResponseType(typeof(InventoryEntryDto), 200)]
    public async Task<ActionResult<InventoryEntryDto>> Delete([Required] string id)
    {
        await _inventoryService.DeleteAsync(id);
        return Ok();
    }
}
