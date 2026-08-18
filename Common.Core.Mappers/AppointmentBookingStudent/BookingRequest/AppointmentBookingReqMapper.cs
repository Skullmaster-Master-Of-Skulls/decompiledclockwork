using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.Mappers.AppointmentBookingStudent.BookingRequest
{
	// Token: 0x0200020D RID: 525
	public static class AppointmentBookingReqMapper
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x000262F0 File Offset: 0x000244F0
		static AppointmentBookingReqMapper()
		{
			Mapper.CreateMap<AppointmentBookingReqDTO, AppointmentBookingReq>();
			Mapper.CreateMap<AppointmentBookingReq, AppointmentBookingReqDTO>();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00026300 File Offset: 0x00024500
		public static AppointmentBookingReq ToDomainObject(this AppointmentBookingReqDTO dto)
		{
			return Mapper.Map<AppointmentBookingReqDTO, AppointmentBookingReq>(dto);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00026318 File Offset: 0x00024518
		public static AppointmentBookingReqDTO ToDTO(this AppointmentBookingReq item)
		{
			return Mapper.Map<AppointmentBookingReq, AppointmentBookingReqDTO>(item);
		}
	}
}
