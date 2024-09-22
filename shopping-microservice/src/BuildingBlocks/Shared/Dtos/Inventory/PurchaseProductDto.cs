namespace Shared.Dtos.Inventory;

public class PurchaseProductDto
{
    public string DocumentNo { get; set; }
    public string ItemNo { get; set; }
    public int Quantity { get; set; }
    public string ExternalDocumentNo { get; set; }
}
