using ChatService.Core.Abstractions;
using ChatService.Infrastructure.Data;

namespace ChatService.Core.Repositories;
public class RepositoryBase
{
    public readonly IBaseRepository<ChatDbContext> _baseRepository;

    public RepositoryBase(IBaseRepository<ChatDbContext> baseRepository)
    {
        _baseRepository = baseRepository;
    }
}

