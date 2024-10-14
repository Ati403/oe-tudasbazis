
using AutoMapper;

using Microsoft.EntityFrameworkCore;

using OE.Tudasbazis.Application.DTOs.Requests;
using OE.Tudasbazis.Application.DTOs.Responses;
using OE.Tudasbazis.Application.Exceptions;
using OE.Tudasbazis.Application.Services;
using OE.Tudasbazis.DataAccess;
using OE.Tudasbazis.Domain.Entities;

using BC = BCrypt.Net.BCrypt;

namespace OE.Tudasbazis.Logic.Services
{
	/// <inheritdoc cref="IUserService"/>/>
	class UserService : IUserService
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _context;

		public UserService(IMapper mapper, AppDbContext context)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<bool> CheckUserExists(LoginOrRegisterRequestDto userCreationDto)
		{
			var user = await _context.Users
				.FirstOrDefaultAsync(u => u.Username == userCreationDto.Username);

			return user is not null;
		}

		public async Task RegisterUserAsync(LoginOrRegisterRequestDto userCreationDto)
		{
			var user = _mapper.Map<User>(userCreationDto);

			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
		}

		public async Task<LoggedInUserDto> AuthenticateUserAsync(LoginOrRegisterRequestDto loginDto)
		{
			var user = await _context.Users
				.SingleOrDefaultAsync(u => u.Username == loginDto.Username);

			if (user is null || !BC.Verify(loginDto.Password, user.Password))
			{
				throw new BusinessLogicException("Invalid username or password.") { StatusCode = 401 };
			}

			return _mapper.Map<LoggedInUserDto>(user);
		}
	}
}
