using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001B0 RID: 432
	public static class BaseBasicAppointmentMapper
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x000203B8 File Offset: 0x0001E5B8
		static BaseBasicAppointmentMapper()
		{
			AppTypeMapper.CreateMap();
			AppShowTimeAsTypeMapper.CreateMap();
			AttendeeMapper.CreateMap();
			Mapper.CreateMap<BaseBasicAppointmentDTO, BaseBasicAppointment>().ForMember((BaseBasicAppointment ar) => (object)ar.Id, delegate(IMemberConfigurationExpression<BaseBasicAppointmentDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<BaseBasicAppointment, BaseBasicAppointmentDTO>();
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00020448 File Offset: 0x0001E648
		public static BaseBasicAppointment ToDomainObject(this BaseBasicAppointmentDTO dto)
		{
			return Mapper.Map<BaseBasicAppointmentDTO, BaseBasicAppointment>(dto);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00020460 File Offset: 0x0001E660
		public static BaseBasicAppointmentDTO ToDTO(this BaseBasicAppointment item)
		{
			return Mapper.Map<BaseBasicAppointment, BaseBasicAppointmentDTO>(item);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00020478 File Offset: 0x0001E678
		public static IList<BaseBasicAppointment> ToDomainObject(this IList<BaseBasicAppointmentDTO> list)
		{
			IList<BaseBasicAppointment> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<BaseBasicAppointment>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000204BC File Offset: 0x0001E6BC
		public static IList<BaseBasicAppointmentDTO> ToDTO(this IList<BaseBasicAppointment> list)
		{
			IList<BaseBasicAppointmentDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<BaseBasicAppointmentDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
