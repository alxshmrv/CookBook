using CookBook.Abstractions;
using System.Collections.Concurrent;

namespace CookBook.Services
{
    public class JwtTokenRepository : IJwtTokensRepository
    {
        private readonly ConcurrentDictionary<int, string> _tokens = new();
        public void Remove(int userId) => _tokens.Remove(userId, out _);
        public void Update(int userId, string token) => _tokens[userId] = token;
        public bool Verify(int userId, string token) =>
            _tokens.ContainsKey(userId) && _tokens[userId] == token;

    }
}
