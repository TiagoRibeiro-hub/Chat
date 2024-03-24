using ChatService.Infrastructure.Data.Abstractions;
using ChatService.Infrastructure.Data;

namespace ChatService.Core.Services;
internal class ServiceBase
{
    public readonly IBaseRepository<ChatDbContext> _baseRepository;

    public ServiceBase(IBaseRepository<ChatDbContext> baseRepository)
    {
        _baseRepository = baseRepository;
    }
}

