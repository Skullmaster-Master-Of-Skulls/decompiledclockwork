using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule
{
	// Token: 0x02000185 RID: 389
	public static class AvailabilityScheduleTimeMapper
	{
		// Token: 0x060006A9 RID: 1705 RVA: 0x0001E434 File Offset: 0x0001C634
		static AvailabilityScheduleTimeMapper()
		{
			Mapper.CreateMap<AvailabilityScheduleTimeDTO, AvailabilityScheduleTime>();
			Mapper.CreateMap<AvailabilityScheduleTime, AvailabilityScheduleTimeDTO>();
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001E444 File Offset: 0x0001C644
		public static AvailabilityScheduleTime ToDomainObject(this AvailabilityScheduleTimeDTO appTypeGroupDTO)
		{
			return Mapper.Map<AvailabilityScheduleTimeDTO, AvailabilityScheduleTime>(appTypeGroupDTO);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001E45C File Offset: 0x0001C65C
		public static AvailabilityScheduleTimeDTO ToDTO(this AvailabilityScheduleTime appTypeGroup)
		{
			return Mapper.Map<AvailabilityScheduleTime, AvailabilityScheduleTimeDTO>(appTypeGroup);
		}
	}
}
