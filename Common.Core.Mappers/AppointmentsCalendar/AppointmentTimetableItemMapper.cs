using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.AppointmentsCalendar
{
	// Token: 0x02000200 RID: 512
	public static class AppointmentTimetableItemMapper
	{
		// Token: 0x060008A9 RID: 2217 RVA: 0x00025414 File Offset: 0x00023614
		static AppointmentTimetableItemMapper()
		{
			LookupTimetableItemMapper.CreateMap();
			Mapper.CreateMap<AppointmentTimetableItemDTO, AppointmentTimetableItem>().ForMember((AppointmentTimetableItem pb) => pb.TimetableItem, delegate(IMemberConfigurationExpression<AppointmentTimetableItemDTO> m)
			{
				m.MapFrom<LookupTimetableItem>((AppointmentTimetableItemDTO pbdto) => (pbdto.TimetableItem == null) ? null : pbdto.TimetableItem.ToDomainObject());
			});
			Mapper.CreateMap<AppointmentTimetableItem, AppointmentTimetableItemDTO>().ForMember((AppointmentTimetableItemDTO pb) => pb.TimetableItem, delegate(IMemberConfigurationExpression<AppointmentTimetableItem> m)
			{
				m.MapFrom<LookupTimetableItemDTO>((AppointmentTimetableItem pbdto) => (pbdto.TimetableItem == null) ? null : pbdto.TimetableItem.ToDTO());
			});
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000254D0 File Offset: 0x000236D0
		public static AppointmentTimetableItem ToDomainObject(this AppointmentTimetableItemDTO appTypeGroupDTO)
		{
			return Mapper.Map<AppointmentTimetableItemDTO, AppointmentTimetableItem>(appTypeGroupDTO);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x000254E8 File Offset: 0x000236E8
		public static AppointmentTimetableItemDTO ToDTO(this AppointmentTimetableItem appTypeGroup)
		{
			return Mapper.Map<AppointmentTimetableItem, AppointmentTimetableItemDTO>(appTypeGroup);
		}
	}
}
