using AutoMapper;

using OE.Tudasbazis.Application.DTOs.Requests;
using OE.Tudasbazis.Application.DTOs.Responses;
using OE.Tudasbazis.Domain.Entities;

using BC = BCrypt.Net.BCrypt;

namespace OE.Tudasbazis.Logic.MapperConfigs
{
	public class UserMapperConfig : Profile
	{
		public UserMapperConfig()
		{
			CreateMap<LoginOrRegisterRequestDto, User>()
				.ForMember(opt => opt.Id, dest => dest.MapFrom(src => Guid.NewGuid()))
				.ForMember(opt => opt.Password, dest => dest.MapFrom(src => BC.HashPassword(src.Password)))
				.ForMember(opt => opt.Role, dest => dest.Ignore());

			CreateMap<User, LoggedInUserDto>();
		}
	}
}
