using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001CC RID: 460
	public static class TestForEditMapper
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x00021DC0 File Offset: 0x0001FFC0
		static TestForEditMapper()
		{
			TestMapper.CreateMap();
			Mapper.CreateMap<TestForEditDTO, TestForEdit>().ForMember((TestForEdit pb) => pb.Test, delegate(IMemberConfigurationExpression<TestForEditDTO> m)
			{
				m.MapFrom<Test>((TestForEditDTO pbdto) => (pbdto.Test == null) ? null : pbdto.Test.ToDomainObject());
			});
			Mapper.CreateMap<TestForEdit, TestForEditDTO>().ForMember((TestForEditDTO pb) => pb.Test, delegate(IMemberConfigurationExpression<TestForEdit> m)
			{
				m.MapFrom<TestDTO>((TestForEdit pbdto) => (pbdto.Test == null) ? null : pbdto.Test.ToDTO());
			});
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00021E7C File Offset: 0x0002007C
		public static TestForEdit ToDomainObject(this TestForEditDTO classTestDTO)
		{
			return Mapper.Map<TestForEditDTO, TestForEdit>(classTestDTO);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00021E94 File Offset: 0x00020094
		public static TestForEditDTO ToDTO(this TestForEdit classTest)
		{
			return Mapper.Map<TestForEdit, TestForEditDTO>(classTest);
		}
	}
}
