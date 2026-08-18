using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.Common.Core.Mappers.Intake
{
	// Token: 0x02000104 RID: 260
	public static class IntakeEntryMapper
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x00015FC0 File Offset: 0x000141C0
		static IntakeEntryMapper()
		{
			IntakeStatusMapper.CreateMap();
			Mapper.CreateMap<IntakeEntryDTO, IntakeEntry>().ForMember((IntakeEntry pb) => pb.Status, delegate(IMemberConfigurationExpression<IntakeEntryDTO> m)
			{
				m.MapFrom<IntakeStatus>((IntakeEntryDTO pbdto) => (pbdto.Status == null) ? null : pbdto.Status.ToDomainObject());
			});
			Mapper.CreateMap<IntakeEntry, IntakeEntryDTO>().ForMember((IntakeEntryDTO pb) => pb.Status, delegate(IMemberConfigurationExpression<IntakeEntry> m)
			{
				m.MapFrom<IntakeStatusDTO>((IntakeEntry pbdto) => (pbdto.Status == null) ? null : pbdto.Status.ToDTO());
			});
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001607C File Offset: 0x0001427C
		public static IntakeEntry ToDomainObject(this IntakeEntryDTO dynamicDataDTO)
		{
			return Mapper.Map<IntakeEntryDTO, IntakeEntry>(dynamicDataDTO);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00016094 File Offset: 0x00014294
		public static IntakeEntryDTO ToDTO(this IntakeEntry dynamicData)
		{
			return Mapper.Map<IntakeEntry, IntakeEntryDTO>(dynamicData);
		}
	}
}
