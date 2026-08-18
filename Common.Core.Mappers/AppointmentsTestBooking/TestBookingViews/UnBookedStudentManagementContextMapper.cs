using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x020001D1 RID: 465
	public static class UnBookedStudentManagementContextMapper
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x00022280 File Offset: 0x00020480
		static UnBookedStudentManagementContextMapper()
		{
			Mapper.CreateMap<UnBookedStudentMmanagementContextDTO, UnBookedStudentMmanagementContext>();
			Mapper.CreateMap<UnBookedStudentMmanagementContext, UnBookedStudentMmanagementContextDTO>();
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00022290 File Offset: 0x00020490
		public static UnBookedStudentMmanagementContext ToDomainObject(this UnBookedStudentMmanagementContextDTO dto)
		{
			return Mapper.Map<UnBookedStudentMmanagementContextDTO, UnBookedStudentMmanagementContext>(dto);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000222A8 File Offset: 0x000204A8
		public static UnBookedStudentMmanagementContextDTO ToDTO(this UnBookedStudentMmanagementContext item)
		{
			return Mapper.Map<UnBookedStudentMmanagementContext, UnBookedStudentMmanagementContextDTO>(item);
		}
	}
}
