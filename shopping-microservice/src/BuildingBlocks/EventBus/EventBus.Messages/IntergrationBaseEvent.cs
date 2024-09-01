namespace EventBus.Messages;

public record IntergrationBaseEvent() : IIntergrationEvent
{
    public DateTime CreationDate { get; } = DateTime.Now;
    public Guid Id { get; set; }
}
