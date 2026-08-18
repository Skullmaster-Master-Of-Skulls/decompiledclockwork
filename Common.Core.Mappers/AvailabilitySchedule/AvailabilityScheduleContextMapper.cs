using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule
{
	// Token: 0x02000180 RID: 384
	public static class AvailabilityScheduleContextMapper
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x0001DF7C File Offset: 0x0001C17C
		static AvailabilityScheduleContextMapper()
		{
			Mapper.CreateMap<AvailabilityScheduleContextDTO, AvailabilityScheduleContext>().ForMember((AvailabilityScheduleContext pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AvailabilityScheduleContextDTO> m)
			{
				m.Ignore();
			}).ForMember((AvailabilityScheduleContext pb) => (object)pb.SecondId, delegate(IMemberConfigurationExpression<AvailabilityScheduleContextDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<AvailabilityScheduleContext, AvailabilityScheduleContextDTO>();
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001E05C File Offset: 0x0001C25C
		public static AvailabilityScheduleContext ToDomainObject(this AvailabilityScheduleContextDTO appTypeGroupDTO)
		{
			return Mapper.Map<AvailabilityScheduleContextDTO, AvailabilityScheduleContext>(appTypeGroupDTO);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001E074 File Offset: 0x0001C274
		public static AvailabilityScheduleContextDTO ToDTO(this AvailabilityScheduleContext appTypeGroup)
		{
			return Mapper.Map<AvailabilityScheduleContext, AvailabilityScheduleContextDTO>(appTypeGroup);
		}
	}
}
