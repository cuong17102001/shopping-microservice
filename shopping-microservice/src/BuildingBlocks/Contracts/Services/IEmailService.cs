namespace BuildingBlocks.Contracts.Services;

public interface IEmailService<T> where T : class
{
    Task SendEmailAsync(T request, CancellationToken cancellationToken = default);
}