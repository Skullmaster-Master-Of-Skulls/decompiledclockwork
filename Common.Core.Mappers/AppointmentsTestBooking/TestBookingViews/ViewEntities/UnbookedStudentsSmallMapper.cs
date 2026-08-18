using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.TestBookingViews.ViewEntities
{
	// Token: 0x020001D6 RID: 470
	public static class UnbookedStudentsSmallMapper
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x000225BC File Offset: 0x000207BC
		static UnbookedStudentsSmallMapper()
		{
			Mapper.CreateMap<UnbookedStudentsSmallDTO, UnbookedStudentsSmall>();
			Mapper.CreateMap<UnbookedStudentsSmall, UnbookedStudentsSmallDTO>();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000225CC File Offset: 0x000207CC
		public static UnbookedStudentsSmall ToDomainObject(this UnbookedStudentsSmallDTO dto)
		{
			return Mapper.Map<UnbookedStudentsSmallDTO, UnbookedStudentsSmall>(dto);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x000225E4 File Offset: 0x000207E4
		public static UnbookedStudentsSmallDTO ToDTO(this UnbookedStudentsSmall item)
		{
			return Mapper.Map<UnbookedStudentsSmall, UnbookedStudentsSmallDTO>(item);
		}
	}
}
