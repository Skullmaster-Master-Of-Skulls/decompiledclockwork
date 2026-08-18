using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.FullTest
{
	// Token: 0x020001D9 RID: 473
	public static class TestForEditClassDefinitionSpecificMapper
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x00022684 File Offset: 0x00020884
		static TestForEditClassDefinitionSpecificMapper()
		{
			BaseExtendedAppointmentMapper.CreateMap();
			AccommodationForTestMapper.CreateMap();
			Mapper.CreateMap<TestForEditClassDefinitionSpecificDTO, TestForEditClassDefinitionSpecific>();
			Mapper.CreateMap<TestForEditClassDefinitionSpecific, TestForEditClassDefinitionSpecificDTO>();
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000226A0 File Offset: 0x000208A0
		public static TestForEditClassDefinitionSpecific ToDomainObject(this TestForEditClassDefinitionSpecificDTO dto)
		{
			return Mapper.Map<TestForEditClassDefinitionSpecificDTO, TestForEditClassDefinitionSpecific>(dto);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000226B8 File Offset: 0x000208B8
		public static TestForEditClassDefinitionSpecificDTO ToDTO(this TestForEditClassDefinitionSpecific item)
		{
			return Mapper.Map<TestForEditClassDefinitionSpecific, TestForEditClassDefinitionSpecificDTO>(item);
		}
	}
}
