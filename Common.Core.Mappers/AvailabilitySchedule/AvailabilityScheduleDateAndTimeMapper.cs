using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.Mappers.AvailabilitySchedule
{
	// Token: 0x02000181 RID: 385
	public static class AvailabilityScheduleDateAndTimeMapper
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x0001E08C File Offset: 0x0001C28C
		static AvailabilityScheduleDateAndTimeMapper()
		{
			AvailabilityScheduleTimeMapper.CreateMap();
			Mapper.CreateMap<AvailabilityScheduleDateAndTimeDTO, AvailabilityScheduleDateAndTime>().ForMember((AvailabilityScheduleDateAndTime pb) => pb.Time, delegate(IMemberConfigurationExpression<AvailabilityScheduleDateAndTimeDTO> m)
			{
				m.MapFrom<AvailabilityScheduleTime>((AvailabilityScheduleDateAndTimeDTO pbdto) => (pbdto.Time == null) ? null : pbdto.Time.ToDomainObject());
			});
			Mapper.CreateMap<AvailabilityScheduleDateAndTime, AvailabilityScheduleDateAndTimeDTO>().ForMember((AvailabilityScheduleDateAndTimeDTO pb) => pb.Time, delegate(IMemberConfigurationExpression<AvailabilityScheduleDateAndTime> m)
			{
				m.MapFrom<AvailabilityScheduleTimeDTO>((AvailabilityScheduleDateAndTime pbdto) => (pbdto.Time == null) ? null : pbdto.Time.ToDTO());
			});
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001E148 File Offset: 0x0001C348
		public static AvailabilityScheduleDateAndTime ToDomainObject(this AvailabilityScheduleDateAndTimeDTO appTypeGroupDTO)
		{
			return Mapper.Map<AvailabilityScheduleDateAndTimeDTO, AvailabilityScheduleDateAndTime>(appTypeGroupDTO);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001E160 File Offset: 0x0001C360
		public static AvailabilityScheduleDateAndTimeDTO ToDTO(this AvailabilityScheduleDateAndTime appTypeGroup)
		{
			return Mapper.Map<AvailabilityScheduleDateAndTime, AvailabilityScheduleDateAndTimeDTO>(appTypeGroup);
		}
	}
}
