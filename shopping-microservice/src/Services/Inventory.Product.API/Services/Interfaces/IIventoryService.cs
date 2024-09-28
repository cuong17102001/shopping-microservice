using BuildingBlocks.Core.SeedWork;
using Inventory.Product.API.Entities;
using Inventory.Product.API.Repositories.Abstraction;
using Shared.Dtos.Inventory;

namespace Inventory.Product.API.Services.Interfaces;

public interface IIventoryService : IMongoRepositoryBase<InventoryEntry>
{
    Task<IEnumerable<InventoryEntryDto>> GetAllByItemNoAsync(string itemNo);
    Task<PagedList<InventoryEntryDto>> GetAllByItemNoPagingAsync(GetInventoryPagingQuery query);
    Task<InventoryEntryDto> GetByIdAsync(string id);
    Task<InventoryEntryDto> PurchaseItemAsync(string itemNo, PurchaseProductDto model);
}
