using Contracts.Common.Events;
using Contracts.Domains;
using Ordering.Domain.Enums;
using Ordering.Domain.OrderAggregate.Events;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Entities
{   
    public class Order : AuditableEventEntity<long>
    {
        [Required]
        public string UserName { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }
        public Guid DocumentNo { get; set; } = Guid.NewGuid();
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string ShippingAddress { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string EmailAddress { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string InvoiceAddress { get; set; }
        public EOrderStatus Status{ get; set; }

        public Order AddedOrder()
        {
            AddDomainEvent(new OrderCreatedEvent(Id, UserName, DocumentNo.ToString(), TotalPrice, ShippingAddress, EmailAddress, InvoiceAddress));
            return this;
        }

        public Order DeletedOrder()
        {
            RemoveDomainEvent(new OrderDeletedEvent(Id));
            return this;
        }
    }
}