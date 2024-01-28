﻿using ChatService.Domain.Entities;

namespace ChatService.Infrastructure.Data.Abstractions;

public interface IBaseMessageService<T> where T : BaseMessage
{
    Task<bool> AddAsync(T item);
    Task<bool> DeleteAsync(Guid identifier);
}
