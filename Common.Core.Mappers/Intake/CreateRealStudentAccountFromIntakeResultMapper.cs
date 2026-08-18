using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.Common.Core.Mappers.Intake
{
	// Token: 0x02000103 RID: 259
	public static class CreateRealStudentAccountFromIntakeResultMapper
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x00015F79 File Offset: 0x00014179
		static CreateRealStudentAccountFromIntakeResultMapper()
		{
			IntakeStatusMapper.CreateMap();
			Mapper.CreateMap<CreateRealStudentAccountFromIntakeResultDTO, CreateRealStudentAccountFromIntakeResult>();
			Mapper.CreateMap<CreateRealStudentAccountFromIntakeResult, CreateRealStudentAccountFromIntakeResultDTO>();
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00015F90 File Offset: 0x00014190
		public static CreateRealStudentAccountFromIntakeResult ToDomainObject(this CreateRealStudentAccountFromIntakeResultDTO dynamicDataDTO)
		{
			return Mapper.Map<CreateRealStudentAccountFromIntakeResultDTO, CreateRealStudentAccountFromIntakeResult>(dynamicDataDTO);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00015FA8 File Offset: 0x000141A8
		public static CreateRealStudentAccountFromIntakeResultDTO ToDTO(this CreateRealStudentAccountFromIntakeResult dynamicData)
		{
			return Mapper.Map<CreateRealStudentAccountFromIntakeResult, CreateRealStudentAccountFromIntakeResultDTO>(dynamicData);
		}
	}
}
