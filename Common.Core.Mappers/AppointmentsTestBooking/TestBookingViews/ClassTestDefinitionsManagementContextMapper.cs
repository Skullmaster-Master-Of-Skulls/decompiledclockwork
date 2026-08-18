using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x020001D0 RID: 464
	public static class ClassTestDefinitionsManagementContextMapper
	{
		// Token: 0x060007E3 RID: 2019 RVA: 0x00022240 File Offset: 0x00020440
		static ClassTestDefinitionsManagementContextMapper()
		{
			Mapper.CreateMap<ClassTestDefinitionsManagementContextDTO, ClassTestDefinitionsManagementContext>();
			Mapper.CreateMap<ClassTestDefinitionsManagementContext, ClassTestDefinitionsManagementContextDTO>();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00022250 File Offset: 0x00020450
		public static ClassTestDefinitionsManagementContext ToDomainObject(this ClassTestDefinitionsManagementContextDTO dto)
		{
			return Mapper.Map<ClassTestDefinitionsManagementContextDTO, ClassTestDefinitionsManagementContext>(dto);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00022268 File Offset: 0x00020468
		public static ClassTestDefinitionsManagementContextDTO ToDTO(this ClassTestDefinitionsManagementContext item)
		{
			return Mapper.Map<ClassTestDefinitionsManagementContext, ClassTestDefinitionsManagementContextDTO>(item);
		}
	}
}
