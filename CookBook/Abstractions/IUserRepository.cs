using CookBook.Contracts;
using CookBook.Models;

namespace CookBook.Abstractions
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(UserDto userDto);
        Task<User> LoginAsync(UserDto userDto);
    }
}
