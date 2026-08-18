using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.Mappers.AppointmentBookingStudent.BookingRequest
{
	// Token: 0x0200020E RID: 526
	public static class AppointmentBookingResultMapper
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x00026330 File Offset: 0x00024530
		static AppointmentBookingResultMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<AppointmentBookingResDTO, AppointmentBookingRes>();
			Mapper.CreateMap<AppointmentBookingRes, AppointmentBookingResDTO>();
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00026348 File Offset: 0x00024548
		public static AppointmentBookingRes ToDomainObject(this AppointmentBookingResDTO dto)
		{
			return Mapper.Map<AppointmentBookingResDTO, AppointmentBookingRes>(dto);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00026360 File Offset: 0x00024560
		public static AppointmentBookingResDTO ToDTO(this AppointmentBookingRes item)
		{
			return Mapper.Map<AppointmentBookingRes, AppointmentBookingResDTO>(item);
		}
	}
}
