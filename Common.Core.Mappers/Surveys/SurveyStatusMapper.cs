using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Surveys;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.Core.Mappers.Surveys
{
	// Token: 0x02000050 RID: 80
	public static class SurveyStatusMapper
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00009814 File Offset: 0x00007A14
		static SurveyStatusMapper()
		{
			Mapper.CreateMap<SurveyStatus, SurveyStatusDTO>();
			Mapper.CreateMap<SurveyStatusDTO, SurveyStatus>().ForMember((SurveyStatus pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SurveyStatusDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009890 File Offset: 0x00007A90
		public static SurveyStatus ToDomainObject(this SurveyStatusDTO surveyDTO)
		{
			return Mapper.Map<SurveyStatusDTO, SurveyStatus>(surveyDTO);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000098A8 File Offset: 0x00007AA8
		public static SurveyStatusDTO ToDTO(this SurveyStatus survey)
		{
			return Mapper.Map<SurveyStatus, SurveyStatusDTO>(survey);
		}
	}
}
