using AutoMapper;
using DiceRoller.Domain.Abstractions.Repositories;
using DiceRoller.Domain.Entities;
using DiceRoller.Domain.Exceptions;
using DiceRoller.Identity.API.Models.Constants;
using DiceRoller.Identity.API.Models.Requests;
using DiceRoller.Identity.API.Models.Responses;
using DiceRoller.Identity.API.Services.Abstractions;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = DiceRoller.Identity.API.Models.Requests.LoginRequest;
using RegisterRequest = DiceRoller.Identity.API.Models.Requests.RegisterRequest;

namespace DiceRoller.Identity.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHelper passwordHelper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHelper = passwordHelper;
            _tokenService = tokenService;
        }

        public async Task CreateUserAsync(RegisterRequest registerRequest)
        {
            var existingUser = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == registerRequest.Email.ToLower().Trim());
            if (existingUser != null)
                throw new BadRequestException(new FailureError(FailureErrorTypes.ExistingEntity, "User already exist",
                    "Email"));

            var user = _mapper.Map<User>(registerRequest);
            user.PasswordHash = _passwordHelper.GeneratePassword(user, registerRequest.Password);

            await _userRepository.AddAsync(user);
        }

        public async Task<TokenResponse> LoginUserAsync(LoginRequest request)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == request.Email.ToLower().Trim());
            if (user == null)
                throw new NotFoundException(new FailureError(FailureErrorTypes.NotFound, "User not found"));

            if (_passwordHelper.VerifyPassword(user, user.PasswordHash, request.Password))
            {
                return _tokenService.GenerateToken(user);
            };

            throw new BadRequestException(new FailureError(FailureErrorTypes.WrongCredentials, "Invalid Credentials"));
        }

    }
}
