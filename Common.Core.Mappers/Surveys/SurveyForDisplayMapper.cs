using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.Core.Mappers.Surveys
{
	// Token: 0x0200004D RID: 77
	public static class SurveyForDisplayMapper
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00009108 File Offset: 0x00007308
		static SurveyForDisplayMapper()
		{
			Mapper.CreateMap<SurveyForDisplay, SurveyForDisplayDTO>();
			Mapper.CreateMap<SurveyForDisplayDTO, SurveyForDisplay>().ForMember((SurveyForDisplay pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SurveyForDisplayDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00009184 File Offset: 0x00007384
		public static SurveyForDisplay ToDomainObject(this SurveyForDisplayDTO surveyDTO)
		{
			return Mapper.Map<SurveyForDisplayDTO, SurveyForDisplay>(surveyDTO);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000919C File Offset: 0x0000739C
		public static SurveyForDisplayDTO ToDTO(this SurveyForDisplay survey)
		{
			return Mapper.Map<SurveyForDisplay, SurveyForDisplayDTO>(survey);
		}
	}
}
