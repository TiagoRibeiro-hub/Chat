using ChatService.Infrastructure.Data;

namespace ChatService.Core.Abstractions;
public class RepositoryBaseService
{
    public readonly IBaseRepository<ChatDbContext> _baseRepository;

    public RepositoryBaseService(IBaseRepository<ChatDbContext> baseRepository)
    {
        _baseRepository = baseRepository;
    }
}

