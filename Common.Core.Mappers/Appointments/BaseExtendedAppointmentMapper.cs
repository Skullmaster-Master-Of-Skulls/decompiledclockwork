using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001B1 RID: 433
	public static class BaseExtendedAppointmentMapper
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x00020500 File Offset: 0x0001E700
		static BaseExtendedAppointmentMapper()
		{
			BaseBasicAppointmentMapper.CreateMap();
			AppTypeMapper.CreateMap();
			AppShowTimeAsTypeMapper.CreateMap();
			AttendeeMapper.CreateMap();
			AppCancelInfoMapper.CreateMap();
			AppointmentRoomMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			AppointmentRoomMapper.CreateMap();
			Mapper.CreateMap<BaseExtendedAppointmentDTO, BaseExtendedAppointment>().ForMember((BaseExtendedAppointment ar) => (object)ar.Id, delegate(IMemberConfigurationExpression<BaseExtendedAppointmentDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<BaseExtendedAppointment, BaseExtendedAppointmentDTO>();
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000205AC File Offset: 0x0001E7AC
		public static BaseExtendedAppointment ToDomainObject(this BaseExtendedAppointmentDTO dto)
		{
			return Mapper.Map<BaseExtendedAppointmentDTO, BaseExtendedAppointment>(dto);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000205C4 File Offset: 0x0001E7C4
		public static BaseExtendedAppointmentDTO ToDTO(this BaseExtendedAppointment item)
		{
			return Mapper.Map<BaseExtendedAppointment, BaseExtendedAppointmentDTO>(item);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000205DC File Offset: 0x0001E7DC
		public static IList<BaseExtendedAppointment> ToDomainObject(this IList<BaseExtendedAppointmentDTO> list)
		{
			IList<BaseExtendedAppointment> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<BaseExtendedAppointment>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00020620 File Offset: 0x0001E820
		public static IList<BaseExtendedAppointmentDTO> ToDTO(this IList<BaseExtendedAppointment> list)
		{
			IList<BaseExtendedAppointmentDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<BaseExtendedAppointmentDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
