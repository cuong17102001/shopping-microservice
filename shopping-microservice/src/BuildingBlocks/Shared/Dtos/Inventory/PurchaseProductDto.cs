using Shared.Enums;

namespace Shared.Dtos.Inventory;

public class PurchaseProductDto
{
    public EDocumentType DocumentType { get; set; } = EDocumentType.Purchase;
    public string DocumentNo { get; set; }
    public string ItemNo { get; set; }
    public int Quantity { get; set; }
    public string ExternalDocumentNo { get; set; }
}
