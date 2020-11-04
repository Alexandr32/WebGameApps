using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace WebApplicationGame.Core
{
    public class AuthOption
    {
        /// <summary>
        /// Тот кто сгенерировал код
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Для кого сгенерирован код
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// строка для генерации ключа секретного семмитричного шифрования
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Время жизни токена в секундах
        /// </summary>
        public int TokenLifeTime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
