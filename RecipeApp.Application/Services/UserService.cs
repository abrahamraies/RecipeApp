﻿using RecipeApp.Application.Interfaces;
using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Intefaces;

namespace RecipeApp.Application.Services;

public class UserService(IUserRepository userRepository, IEmailService emailService) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmailService _emailService = emailService;
    public async Task<User?> GetUserByIdAsync(int id)
        => await _userRepository.GetByIdAsync(id);

    public async Task<User?> GetUserByEmailAsync(string email)
        => await _userRepository.GetByEmailAsync(email);

    public async Task RegisterUserAsync(User user)
    {
        var existingUser = await _userRepository.GetByEmailAsync(user.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("User with this email already exists.");
        }
        await _userRepository.AddAsync(user);

        var verificationLink = $"https://localhost:7253/api/users/verify-email?token={user.VerificationToken}";
        await _emailService.SendEmailAsync(user.Email, "Verify Your Email", $"Click here to verify your email: {verificationLink}");
    }

    public async Task UpdateUserAsync(User user)
    {
        var existingUser = await _userRepository.GetByIdAsync(user.Id);
        if (existingUser == null)
        {
            throw new InvalidOperationException("User not found.");
        }
        await _userRepository.UpdateAsync(user);
    }

    public async Task<User?> GetUserByVerificationTokenAsync(string token)
        => await _userRepository.GetUserByVerificationTokenAsync(token);
}
