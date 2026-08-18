using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule
{
	// Token: 0x02000182 RID: 386
	public static class AvailabilityScheduleItemActionResultMapper
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x0001E178 File Offset: 0x0001C378
		static AvailabilityScheduleItemActionResultMapper()
		{
			Mapper.CreateMap<AvailabilityScheduleItemActionResultDTO, AvailabilityScheduleItemActionResult>();
			Mapper.CreateMap<AvailabilityScheduleItemActionResult, AvailabilityScheduleItemActionResultDTO>();
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001E188 File Offset: 0x0001C388
		public static AvailabilityScheduleItemActionResult ToDomainObject(this AvailabilityScheduleItemActionResultDTO appTypeGroupDTO)
		{
			return Mapper.Map<AvailabilityScheduleItemActionResultDTO, AvailabilityScheduleItemActionResult>(appTypeGroupDTO);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		public static AvailabilityScheduleItemActionResultDTO ToDTO(this AvailabilityScheduleItemActionResult appTypeGroup)
		{
			return Mapper.Map<AvailabilityScheduleItemActionResult, AvailabilityScheduleItemActionResultDTO>(appTypeGroup);
		}
	}
}
