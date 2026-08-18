using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Intake;
using TechnoPro.Common.Public.Entities.Intake;

namespace TechnoPro.Common.Core.Mappers.Intake
{
	// Token: 0x02000105 RID: 261
	public static class IntakeEntryQueueItemMapper
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x000160AC File Offset: 0x000142AC
		static IntakeEntryQueueItemMapper()
		{
			IntakeStatusMapper.CreateMap();
			Mapper.CreateMap<IntakeEntryQueueItemDTO, IntakeEntryQueueItem>().ForMember((IntakeEntryQueueItem pb) => pb.Status, delegate(IMemberConfigurationExpression<IntakeEntryQueueItemDTO> m)
			{
				m.MapFrom<IntakeStatus>((IntakeEntryQueueItemDTO pbdto) => (pbdto.Status == null) ? null : pbdto.Status.ToDomainObject());
			});
			Mapper.CreateMap<IntakeEntryQueueItem, IntakeEntryQueueItemDTO>().ForMember((IntakeEntryQueueItemDTO pb) => pb.Status, delegate(IMemberConfigurationExpression<IntakeEntryQueueItem> m)
			{
				m.MapFrom<IntakeStatusDTO>((IntakeEntryQueueItem pbdto) => (pbdto.Status == null) ? null : pbdto.Status.ToDTO());
			});
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00016168 File Offset: 0x00014368
		public static IntakeEntryQueueItem ToDomainObject(this IntakeEntryQueueItemDTO dynamicDataDTO)
		{
			return Mapper.Map<IntakeEntryQueueItemDTO, IntakeEntryQueueItem>(dynamicDataDTO);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00016180 File Offset: 0x00014380
		public static IntakeEntryQueueItemDTO ToDTO(this IntakeEntryQueueItem dynamicData)
		{
			return Mapper.Map<IntakeEntryQueueItem, IntakeEntryQueueItemDTO>(dynamicData);
		}
	}
}
