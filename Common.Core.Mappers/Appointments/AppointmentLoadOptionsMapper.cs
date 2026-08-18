using System;
using System.Collections.Generic;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001A7 RID: 423
	public static class AppointmentLoadOptionsMapper
	{
		// Token: 0x0600072F RID: 1839 RVA: 0x0001F8E8 File Offset: 0x0001DAE8
		static AppointmentLoadOptionsMapper()
		{
			Mapper.CreateMap<AppointmentLoadOptionsDTO, AppointmentLoadOptions>().ForMember((AppointmentLoadOptions pb) => pb.AvailabilityGroupIdsByPersonId, delegate(IMemberConfigurationExpression<AppointmentLoadOptionsDTO> m)
			{
				m.MapFrom<IDictionary<int, IList<int>>>((AppointmentLoadOptionsDTO pbdto) => pbdto.AvailabilityGroupIdsByPersonId ?? null);
			});
			Mapper.CreateMap<AppointmentLoadOptions, AppointmentLoadOptionsDTO>().ForMember((AppointmentLoadOptionsDTO pb) => pb.AvailabilityGroupIdsByPersonId, delegate(IMemberConfigurationExpression<AppointmentLoadOptions> m)
			{
				m.MapFrom<IDictionary<int, IList<int>>>((AppointmentLoadOptions pbdto) => pbdto.AvailabilityGroupIdsByPersonId ?? null);
			});
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001F9A0 File Offset: 0x0001DBA0
		public static AppointmentLoadOptions ToDomainObject(this AppointmentLoadOptionsDTO appTypeGroupDTO)
		{
			return Mapper.Map<AppointmentLoadOptionsDTO, AppointmentLoadOptions>(appTypeGroupDTO);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001F9B8 File Offset: 0x0001DBB8
		public static AppointmentLoadOptionsDTO ToDTO(this AppointmentLoadOptions appTypeGroup)
		{
			return Mapper.Map<AppointmentLoadOptions, AppointmentLoadOptionsDTO>(appTypeGroup);
		}
	}
}
