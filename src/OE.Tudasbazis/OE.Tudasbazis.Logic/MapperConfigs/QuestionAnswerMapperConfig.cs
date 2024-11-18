using AutoMapper;

using OE.Tudasbazis.Application.DTOs.Responses;

using OE.Tudasbazis.Domain.Entities;

namespace OE.Tudasbazis.Logic.MapperConfigs
{
	public class QuestionAnswerMapperConfig : Profile
	{
		public QuestionAnswerMapperConfig()
		{
			CreateMap<QuestionAnswerLog, QuestionAnswerHistoryDto>()
				.ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.CreatedAt));
		}
	}
}
