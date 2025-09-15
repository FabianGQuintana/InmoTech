// InmoTech/Security/PasswordHasher.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace InmoTech.Security
{
    public static class PasswordHasher
    {
        // Ajustá el WorkFactor si querés más/menos costo (por defecto 10-12 va bien)
        public static string Hash(string plainText) => BCrypt.Net.BCrypt.HashPassword(plainText);
        public static bool Verify(string plainText, string hash) => BCrypt.Net.BCrypt.Verify(plainText, hash);
    }
}
