using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.Core.Mappers.AppointmentsWorkshops
{
	// Token: 0x0200019A RID: 410
	public static class AppointmentWorkshopInfoMapper
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		static AppointmentWorkshopInfoMapper()
		{
			WorkshopDefinitionMapper.CreateMap();
			Mapper.CreateMap<AppointmentWorkshopInfoDTO, AppointmentWorkshopInfo>();
			Mapper.CreateMap<AppointmentWorkshopInfo, AppointmentWorkshopInfoDTO>();
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001EFE4 File Offset: 0x0001D1E4
		public static AppointmentWorkshopInfo ToDomainObject(this AppointmentWorkshopInfoDTO appointmentWorkshopInfoDTO)
		{
			return Mapper.Map<AppointmentWorkshopInfoDTO, AppointmentWorkshopInfo>(appointmentWorkshopInfoDTO);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001EFFC File Offset: 0x0001D1FC
		public static AppointmentWorkshopInfoDTO ToDTO(this AppointmentWorkshopInfo appointmentWorkshopInfo)
		{
			return Mapper.Map<AppointmentWorkshopInfo, AppointmentWorkshopInfoDTO>(appointmentWorkshopInfo);
		}
	}
}
