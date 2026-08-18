using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.Common.Core.Mappers.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;

namespace TechnoPro.Common.Core.Mappers.AppointmentsRecurring
{
	// Token: 0x020001F8 RID: 504
	public static class RecurringAppointmentMapper
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x000247E8 File Offset: 0x000229E8
		static RecurringAppointmentMapper()
		{
			AppointmentMapper.CreateMap();
			Mapper.CreateMap<RecurringAppointmentDTO, RecurringAppointment>().ForMember((RecurringAppointment pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<RecurringAppointmentDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<RecurringAppointment, RecurringAppointmentDTO>();
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002486C File Offset: 0x00022A6C
		public static RecurringAppointment ToDomainObject(this RecurringAppointmentDTO appointmentRecurringInfoDTO)
		{
			return Mapper.Map<RecurringAppointmentDTO, RecurringAppointment>(appointmentRecurringInfoDTO);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00024884 File Offset: 0x00022A84
		public static RecurringAppointmentDTO ToDTO(this RecurringAppointment appointmentRecurringInfo)
		{
			return Mapper.Map<RecurringAppointment, RecurringAppointmentDTO>(appointmentRecurringInfo);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002489C File Offset: 0x00022A9C
		public static IList<RecurringAppointment> ToDomainObject(this IList<RecurringAppointmentDTO> list)
		{
			IList<RecurringAppointment> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<RecurringAppointment>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000248E0 File Offset: 0x00022AE0
		public static IList<RecurringAppointmentDTO> ToDTO(this IList<RecurringAppointment> list)
		{
			IList<RecurringAppointmentDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<RecurringAppointmentDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
