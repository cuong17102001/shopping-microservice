using Ordering.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Common.Events;

namespace Ordering.Domain.OrderAggregate.Events
{
    public class OrderCreatedEvent : BaseEvent
    {
        public long Id { get; set; }
        public string UserName { get; private set; }
        public string DocumentNo { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string ShippingAddress { get; private set; }
        public string EmailAddress { get; private set; }
        public string InvoiceAddress { get; private set; }

        public OrderCreatedEvent(long id, string userName, string documentNo, decimal totalPrice, string shippingAddress, string emailAddress, string invoiceAddress) { 
            Id = id;
            UserName = userName;
            DocumentNo = documentNo;
            TotalPrice = totalPrice;
            ShippingAddress = shippingAddress;
            EmailAddress = emailAddress;
            InvoiceAddress = invoiceAddress;
        }
    }
}
