using ChatService.Infrastructure.Data;
using ChatService.Infrastructure.Data.Abstractions;

namespace ChatService.Core.Repositories;
public class RepositoryBase
{
    public readonly IBaseRepository<ChatDbContext> _baseRepository;

    public RepositoryBase(IBaseRepository<ChatDbContext> baseRepository)
    {
        _baseRepository = baseRepository;
    }
}

