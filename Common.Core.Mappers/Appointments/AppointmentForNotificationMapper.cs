using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001A5 RID: 421
	public static class AppointmentForNotificationMapper
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		static AppointmentForNotificationMapper()
		{
			Mapper.CreateMap<AppointmentForNotificationDTO, AppointmentForNotification>();
			Mapper.CreateMap<AppointmentForNotification, AppointmentForNotificationDTO>();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001F704 File Offset: 0x0001D904
		public static AppointmentForNotification ToDomainObject(this AppointmentForNotificationDTO appCancelInfoDTO)
		{
			return Mapper.Map<AppointmentForNotificationDTO, AppointmentForNotification>(appCancelInfoDTO);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001F71C File Offset: 0x0001D91C
		public static AppointmentForNotificationDTO ToDTO(this AppointmentForNotification appCancelInfo)
		{
			return Mapper.Map<AppointmentForNotification, AppointmentForNotificationDTO>(appCancelInfo);
		}
	}
}
