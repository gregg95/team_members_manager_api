namespace Infrastructure.Shared.Providers;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
