using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x020001CF RID: 463
	public static class BookingsManagementContextMapper
	{
		// Token: 0x060007DF RID: 2015 RVA: 0x000221FE File Offset: 0x000203FE
		static BookingsManagementContextMapper()
		{
			Mapper.CreateMap<BookingsManagementContextDTO, BookingsManagementContext>();
			Mapper.CreateMap<BookingsManagementContext, BookingsManagementContextDTO>();
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00022210 File Offset: 0x00020410
		public static BookingsManagementContext ToDomainObject(this BookingsManagementContextDTO dto)
		{
			return Mapper.Map<BookingsManagementContextDTO, BookingsManagementContext>(dto);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00022228 File Offset: 0x00020428
		public static BookingsManagementContextDTO ToDTO(this BookingsManagementContext item)
		{
			return Mapper.Map<BookingsManagementContext, BookingsManagementContextDTO>(item);
		}
	}
}
