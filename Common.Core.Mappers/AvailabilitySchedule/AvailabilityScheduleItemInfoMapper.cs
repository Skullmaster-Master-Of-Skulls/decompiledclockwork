using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule
{
	// Token: 0x02000183 RID: 387
	public static class AvailabilityScheduleItemInfoMapper
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x0001E1B8 File Offset: 0x0001C3B8
		static AvailabilityScheduleItemInfoMapper()
		{
			AvailabilityScheduleDateAndTimeMapper.CreateMap();
			Mapper.CreateMap<AvailabilityScheduleItemInfoDTO, AvailabilityScheduleItemInfo>().ForMember((AvailabilityScheduleItemInfo pb) => pb.DayAndTime, delegate(IMemberConfigurationExpression<AvailabilityScheduleItemInfoDTO> m)
			{
				m.MapFrom<AvailabilityScheduleDateAndTime>((AvailabilityScheduleItemInfoDTO pbdto) => (pbdto.DayAndTime == null) ? null : pbdto.DayAndTime.ToDomainObject());
			});
			Mapper.CreateMap<AvailabilityScheduleItemInfo, AvailabilityScheduleItemInfoDTO>().ForMember((AvailabilityScheduleItemInfoDTO pb) => pb.DayAndTime, delegate(IMemberConfigurationExpression<AvailabilityScheduleItemInfo> m)
			{
				m.MapFrom<AvailabilityScheduleDateAndTimeDTO>((AvailabilityScheduleItemInfo pbdto) => (pbdto.DayAndTime == null) ? null : pbdto.DayAndTime.ToDTO());
			});
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001E274 File Offset: 0x0001C474
		public static AvailabilityScheduleItemInfo ToDomainObject(this AvailabilityScheduleItemInfoDTO appTypeGroupDTO)
		{
			return Mapper.Map<AvailabilityScheduleItemInfoDTO, AvailabilityScheduleItemInfo>(appTypeGroupDTO);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001E28C File Offset: 0x0001C48C
		public static AvailabilityScheduleItemInfoDTO ToDTO(this AvailabilityScheduleItemInfo appTypeGroup)
		{
			return Mapper.Map<AvailabilityScheduleItemInfo, AvailabilityScheduleItemInfoDTO>(appTypeGroup);
		}
	}
}
