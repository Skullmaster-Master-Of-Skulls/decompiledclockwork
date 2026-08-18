using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.Common.Core.Mappers.Intake
{
	// Token: 0x02000106 RID: 262
	public static class IntakeStatusMapper
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x00016198 File Offset: 0x00014398
		static IntakeStatusMapper()
		{
			Mapper.CreateMap<IntakeStatusDTO, IntakeStatus>().ForMember((IntakeStatus pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<IntakeStatusDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<IntakeStatus, IntakeStatusDTO>();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00016214 File Offset: 0x00014414
		public static IntakeStatus ToDomainObject(this IntakeStatusDTO dynamicDataDTO)
		{
			return Mapper.Map<IntakeStatusDTO, IntakeStatus>(dynamicDataDTO);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001622C File Offset: 0x0001442C
		public static IntakeStatusDTO ToDTO(this IntakeStatus dynamicData)
		{
			return Mapper.Map<IntakeStatus, IntakeStatusDTO>(dynamicData);
		}
	}
}
