using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Infrastructure.Seed
{
    public class SuperUserSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            int keySize = 64;
            int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
            var password = "99par1999";
            var salt = RandomNumberGenerator.GetBytes(keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize
            );
            var hashed = Convert.ToHexString(hash);

            modelBuilder
                .Entity<User>()
                .HasData(
                    new User
                    {
                        Id = Guid.Parse("6a2c6822-3718-4171-ba19-3d15ac2fd266"),
                        FirstName = "Abdelhak",
                        LastName = "Ait Elhad",
                        Role = UserRole.SuperAdmin,
                        Email = "root@bibliolik.com",
                        DisplayName = "Aitelhad Abdelhak",
                        PasswordHash = hashed,
                        PasswordSalt = salt
                    }
                );
        }
    }
}
