using IdentityServer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace IdentityServer.Services
{
    public class TokenService
    {
        private static RandomNumberGenerator RNG = RandomNumberGenerator.Create();
        private static readonly string MyJwkLocation = Path.Combine(Environment.CurrentDirectory, "secreyKey.json");
        public Token CreateToken(CustomIdentityUser usuario, string role)
        {
            var key = LoadKey();

            var tokenRequirements = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.UserName),
                    new Claim("id",usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.DateOfBirth, usuario.BirthDate.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = new JwtSecurityTokenHandler().CreateToken(tokenRequirements);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }

        private static byte[] GenerateKey(int bytes)
        {
            var data = new byte[bytes];
            RNG.GetBytes(data);
            return data;
        }

        private static SecurityKey LoadKey()
        {
            if (File.Exists(MyJwkLocation))
                return JsonSerializer.Deserialize<JsonWebKey>(File.ReadAllText(MyJwkLocation));
            var newKey = CreateJWK();
            File.WriteAllText(MyJwkLocation, JsonSerializer.Serialize(newKey));
            return newKey;
        }

        private static JsonWebKey CreateJWK()
        {
            var symmetricKey = new HMACSHA256(GenerateKey(64));
            var jwk = JsonWebKeyConverter
                .ConvertFromSymmetricSecurityKey(new SymmetricSecurityKey(symmetricKey.Key));
            jwk.KeyId = Base64UrlEncoder.Encode(GenerateKey(16));
            return jwk;
        }


    }
}
