using AutoMapper;
using CookBook.Abstractions;
using CookBook.Contracts;
using CookBook.CookBook_Database;
using CookBook.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CookBook.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly CookBookDbContext _cookBookDbContext;

        private readonly IMapper _mapper;
        public UserRepository(CookBookDbContext cookBookDbContext, IMapper mapper)
        {
            _cookBookDbContext = cookBookDbContext;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _cookBookDbContext.Users.AddAsync(user);
            await _cookBookDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> LoginAsync(UserDto userDto)
        {
            var user = await _cookBookDbContext.Users.FirstOrDefaultAsync(user => user.Name == userDto.Name);
            if (user is null)
            {
                throw new ArgumentException(nameof(userDto.Name));
            }
            if (user.Password != userDto.Password)
            {
                throw new ArgumentException(nameof(userDto.Password));
            }
            return user;
        }
    }
}
