namespace Ordering.Domain.Enums;

public enum EOrderStatus
{
    New = 1, //start with 1, 0 is used for filter All = 0
    Pending = 2,
    Paid = 3,
    Shipping = 4,
    Fullfilled = 5,
}