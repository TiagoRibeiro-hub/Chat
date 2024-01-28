using ChatService.Api.DTOS;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChatService.Infrastructure.Utils;
public static class Mappings
{
    /// <summary>
    /// Maps from Dto to Domain and validates the necessary keys
    /// </summary>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static E ToDomain<D, E>(this D item) where D : BaseDTO<D, E>
    {
        item.DTOValidation();
        return item.Map<D, E>();
    }

    /// <summary>
    /// Maps from KeyDto to KeyDomain and validates the necessary keys
    /// </summary>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static E ToDomainKey<D, E>(this D key) where D : KeyDTO<D, E>
    {
        key.DTOValidation();
        return key.Map<D, E>();
    }

    /// <summary>
    /// Maps a list from Dto to Domain and validates the necessary keys
    /// </summary>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static List<E> ToDomain<D, E>(this List<D> items) where D : BaseDTO<D, E>
    {
        var res = new List<E>();
        for (int i = 0; i < items.Count; i++)
        {
            var domain = Helper.ToDomain<D, E>(items[i]);
            if (domain == null)
            {
                throw new Exception();
            }
            res.Add(domain);
        }
        return res;
    }

    /// <summary>
    /// Maps from Domain to Dto and validates the necessary keys
    /// </summary>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    public static D ToDto<D, E>(this E item) where D : BaseDTO<D, E>
    {
        var dto = item.Map<E, D>();
        dto.DTOValidation();
        return dto;
    }

    /// <summary>
    /// Maps a list from Domain to Dto and validates the necessary keys
    /// </summary>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <param name="item"></param>
    /// <returns></returns>
    public static List<D> ToDto<D, E>(this List<E> item) where D : BaseDTO<D, E>
    {
        var res = new List<D>();
        for (int i = 0; i < item.Count; i++)
        {
            res.Add(item[i].ToDto<D, E>());
        }
        return res;
    }

    /// <summary>
    /// Maps from KeyDomain to KeyDto and validates the necessary keys
    /// </summary>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public static D ToDtoKey<D, E>(this E key) where D : KeyDTO<D, E>
    {
        var dtoKey = key.Map<E, D>();
        dtoKey.DTOValidation();
        return dtoKey;
    }

    #region Auxiliar Methods
    private static T Map<F, T>(this F item)
    {
        var json = JsonSerializer.Serialize(item);
        if (json != null)
        {
            JsonSerializerOptions option = new();
            option.Converters.Add(new JsonStringEnumConverter());

            var newItem = JsonSerializer.Deserialize<T>(json, option);
            if (newItem != null)
            {
                return newItem;
            }
        }
        throw new JsonException();
    }
    #endregion
}
