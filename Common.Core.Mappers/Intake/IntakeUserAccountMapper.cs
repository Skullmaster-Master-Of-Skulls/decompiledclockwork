using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.Common.Core.Mappers.Intake
{
	// Token: 0x02000107 RID: 263
	public static class IntakeUserAccountMapper
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x00016244 File Offset: 0x00014444
		static IntakeUserAccountMapper()
		{
			Mapper.CreateMap<IntakeUserAccountDTO, IntakeUserAccount>().ForMember((IntakeUserAccount pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<IntakeUserAccountDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<IntakeUserAccount, IntakeUserAccountDTO>();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000162C0 File Offset: 0x000144C0
		public static IntakeUserAccount ToDomainObject(this IntakeUserAccountDTO dynamicDataDTO)
		{
			return Mapper.Map<IntakeUserAccountDTO, IntakeUserAccount>(dynamicDataDTO);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000162D8 File Offset: 0x000144D8
		public static IntakeUserAccountDTO ToDTO(this IntakeUserAccount dynamicData)
		{
			return Mapper.Map<IntakeUserAccount, IntakeUserAccountDTO>(dynamicData);
		}
	}
}
