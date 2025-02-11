﻿namespace RecipeApp.Infrastructure.Security
{
    public class PasswordHasher
    {
        public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool Verify(string hash, string password) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
