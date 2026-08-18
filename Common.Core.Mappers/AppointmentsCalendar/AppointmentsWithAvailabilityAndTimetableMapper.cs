using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.AvailabilitySchedule;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AvailabilitySchedule;

namespace TechnoPro.Common.Core.Mappers.AppointmentsCalendar
{
	// Token: 0x020001FF RID: 511
	public static class AppointmentsWithAvailabilityAndTimetableMapper
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x00024FF0 File Offset: 0x000231F0
		static AppointmentsWithAvailabilityAndTimetableMapper()
		{
			AppointmentMapper.CreateMap();
			AppointmentTimetableItemMapper.CreateMap();
			AvailabilityScheduleItemsForContextMapper.CreateMap();
			HolidayMapper.CreateMap();
			Mapper.CreateMap<AppointmentsWithAvailabilityAndTimetableDTO, AppointmentsWithAvailabilityAndTimetable>().ForMember((AppointmentsWithAvailabilityAndTimetable pb) => pb.Appointments, delegate(IMemberConfigurationExpression<AppointmentsWithAvailabilityAndTimetableDTO> m)
			{
				m.MapFrom<List<Appointment>>((AppointmentsWithAvailabilityAndTimetableDTO pbdto) => (pbdto.Appointments == null) ? null : pbdto.Appointments.ToList<AppointmentDTO>().ConvertAll<Appointment>((AppointmentDTO g) => g.ToDomainObject()));
			}).ForMember((AppointmentsWithAvailabilityAndTimetable pb) => pb.TimetableItems, delegate(IMemberConfigurationExpression<AppointmentsWithAvailabilityAndTimetableDTO> m)
			{
				m.MapFrom<IDictionary<int, IList<AppointmentTimetableItem>>>((AppointmentsWithAvailabilityAndTimetableDTO pbdto) => (pbdto.TimetableItems == null) ? null : pbdto.TimetableItems.ToDomainObject());
			}).ForMember((AppointmentsWithAvailabilityAndTimetable pb) => pb.Holidays, delegate(IMemberConfigurationExpression<AppointmentsWithAvailabilityAndTimetableDTO> m)
			{
				m.MapFrom<IEnumerable<Holiday>>((AppointmentsWithAvailabilityAndTimetableDTO pbdto) => (pbdto.Holidays == null) ? null : (from g in pbdto.Holidays
				select g.ToDomainObject()));
			}).ForMember((AppointmentsWithAvailabilityAndTimetable pb) => pb.AvailabilitySchedules, delegate(IMemberConfigurationExpression<AppointmentsWithAvailabilityAndTimetableDTO> m)
			{
				m.MapFrom<List<AvailabilityScheduleItemsForContext>>((AppointmentsWithAvailabilityAndTimetableDTO pbdto) => (pbdto.AvailabilitySchedules == null) ? null : (from g in pbdto.AvailabilitySchedules
				select g.ToDomainObject()).ToList<AvailabilityScheduleItemsForContext>());
			});
			Mapper.CreateMap<AppointmentsWithAvailabilityAndTimetable, AppointmentsWithAvailabilityAndTimetableDTO>().ForMember((AppointmentsWithAvailabilityAndTimetableDTO pb) => pb.Appointments, delegate(IMemberConfigurationExpression<AppointmentsWithAvailabilityAndTimetable> m)
			{
				m.MapFrom<List<AppointmentDTO>>((AppointmentsWithAvailabilityAndTimetable pbdto) => (pbdto.Appointments == null) ? null : pbdto.Appointments.ToList<Appointment>().ConvertAll<AppointmentDTO>((Appointment g) => g.ToDTO()));
			}).ForMember((AppointmentsWithAvailabilityAndTimetableDTO pb) => pb.TimetableItems, delegate(IMemberConfigurationExpression<AppointmentsWithAvailabilityAndTimetable> m)
			{
				m.MapFrom<IDictionary<int, IList<AppointmentTimetableItemDTO>>>((AppointmentsWithAvailabilityAndTimetable pbdto) => (pbdto.TimetableItems == null) ? null : pbdto.TimetableItems.ToDTO());
			}).ForMember((AppointmentsWithAvailabilityAndTimetableDTO pb) => pb.Holidays, delegate(IMemberConfigurationExpression<AppointmentsWithAvailabilityAndTimetable> m)
			{
				m.MapFrom<IEnumerable<HolidayDTO>>((AppointmentsWithAvailabilityAndTimetable pbdto) => (pbdto.Holidays == null) ? null : (from g in pbdto.Holidays
				select g.ToDTO()));
			}).ForMember((AppointmentsWithAvailabilityAndTimetableDTO pb) => pb.AvailabilitySchedules, delegate(IMemberConfigurationExpression<AppointmentsWithAvailabilityAndTimetable> m)
			{
				m.MapFrom<List<AvailabilityScheduleItemsForContextDTO>>((AppointmentsWithAvailabilityAndTimetable pbdto) => (pbdto.AvailabilitySchedules == null) ? null : (from g in pbdto.AvailabilitySchedules
				select g.ToDTO()).ToList<AvailabilityScheduleItemsForContextDTO>());
			});
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00025294 File Offset: 0x00023494
		public static AppointmentsWithAvailabilityAndTimetable ToDomainObject(this AppointmentsWithAvailabilityAndTimetableDTO appTypeGroupDTO)
		{
			return Mapper.Map<AppointmentsWithAvailabilityAndTimetableDTO, AppointmentsWithAvailabilityAndTimetable>(appTypeGroupDTO);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x000252AC File Offset: 0x000234AC
		public static AppointmentsWithAvailabilityAndTimetableDTO ToDTO(this AppointmentsWithAvailabilityAndTimetable appTypeGroup)
		{
			return Mapper.Map<AppointmentsWithAvailabilityAndTimetable, AppointmentsWithAvailabilityAndTimetableDTO>(appTypeGroup);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000252C4 File Offset: 0x000234C4
		public static IDictionary<int, IList<AppointmentTimetableItem>> ToDomainObject(this IDictionary<int, IList<AppointmentTimetableItemDTO>> dtos)
		{
			bool flag = dtos == null;
			IDictionary<int, IList<AppointmentTimetableItem>> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Dictionary<int, IList<AppointmentTimetableItem>> dictionary = new Dictionary<int, IList<AppointmentTimetableItem>>();
				foreach (KeyValuePair<int, IList<AppointmentTimetableItemDTO>> keyValuePair in dtos)
				{
					Dictionary<int, IList<AppointmentTimetableItem>> dictionary2 = dictionary;
					int key = keyValuePair.Key;
					IList<AppointmentTimetableItem> value;
					if (keyValuePair.Value != null)
					{
						value = keyValuePair.Value.ToList<AppointmentTimetableItemDTO>().ConvertAll<AppointmentTimetableItem>((AppointmentTimetableItemDTO g) => g.ToDomainObject());
					}
					else
					{
						value = null;
					}
					dictionary2.Add(key, value);
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002536C File Offset: 0x0002356C
		public static IDictionary<int, IList<AppointmentTimetableItemDTO>> ToDTO(this IDictionary<int, IList<AppointmentTimetableItem>> domainObjects)
		{
			bool flag = domainObjects == null;
			IDictionary<int, IList<AppointmentTimetableItemDTO>> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Dictionary<int, IList<AppointmentTimetableItemDTO>> dictionary = new Dictionary<int, IList<AppointmentTimetableItemDTO>>();
				foreach (KeyValuePair<int, IList<AppointmentTimetableItem>> keyValuePair in domainObjects)
				{
					Dictionary<int, IList<AppointmentTimetableItemDTO>> dictionary2 = dictionary;
					int key = keyValuePair.Key;
					IList<AppointmentTimetableItemDTO> value;
					if (keyValuePair.Value != null)
					{
						value = keyValuePair.Value.ToList<AppointmentTimetableItem>().ConvertAll<AppointmentTimetableItemDTO>((AppointmentTimetableItem g) => g.ToDTO());
					}
					else
					{
						value = null;
					}
					dictionary2.Add(key, value);
				}
				result = dictionary;
			}
			return result;
		}
	}
}
