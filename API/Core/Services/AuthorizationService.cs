using DataLayer.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services
{
    public class AuthorizationService
    {
        private readonly string _securityKey;

        private readonly int _pbkdf2IterCount = 1000;
        private readonly int _pbkdf22SubkeyLength = 256 / 8;
        private readonly int _saltSize = 128 / 8;

        public AuthorizationService(IConfiguration config)
        {
            _securityKey = config["JWT:SecurityKey"] ?? throw new Exception();

            for (int i = 0; i < 600; i++)
            {
                Debug.WriteLine(_securityKey);
            }
        }

        public string GetToken(User user)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new();

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_securityKey));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            Claim idClaim = new("id", user.Id.ToString());
            Claim emailClaim = new("email", user.Email);
            Claim usernameClaim = new("username", user.Username);
            Claim roleClaim = new("role", user.Role.ToString());

            SecurityTokenDescriptor tokenDescriptior = new()
            {
                Issuer = "Backend",
                Audience = "Frontend",
                Subject = new ClaimsIdentity(new[] { roleClaim, idClaim, emailClaim, usernameClaim }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptior);
            var tokenString = jwtTokenHandler.WriteToken(token);

            return tokenString;
        }

        public bool ValidateToken(string tokenString)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new();
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_securityKey));

            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuer = false,
                IssuerSigningKey = key,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
            };

            if (!jwtTokenHandler.CanReadToken(tokenString.Replace("Bearer ", string.Empty)))
            {
                Console.WriteLine("Invalid Token");
                return false;
            }

            jwtTokenHandler.ValidateToken(tokenString, tokenValidationParameters, out var validatedToken);
            return validatedToken != null;
        }

        public string HashPassword(string password)
        {
            if (password == null)
            {
                throw new Exception();
            }

            byte[] salt;
            byte[] subkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, _saltSize, _pbkdf2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(_pbkdf22SubkeyLength);
            }

            var outputBytes = new byte[1 + _saltSize + _pbkdf22SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, _saltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + _saltSize, _pbkdf22SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            if (hashedPasswordBytes.Length != (1 + _saltSize + _pbkdf22SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                return false;
            }

            var salt = new byte[_saltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, _saltSize);
            var storedSubkey = new byte[_pbkdf22SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + _saltSize, storedSubkey, 0, _pbkdf22SubkeyLength);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, _pbkdf2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(_pbkdf22SubkeyLength);
            }
            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= a[i] == b[i];
            }
            return areSame;
        }
    }
}
