using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001C0 RID: 448
	public static class ClassTestForEditMapper
	{
		// Token: 0x060007A1 RID: 1953 RVA: 0x000212B0 File Offset: 0x0001F4B0
		static ClassTestForEditMapper()
		{
			TestMapper.CreateMap();
			Mapper.CreateMap<ClassTestForEditDTO, ClassTestForEdit>().ForMember((ClassTestForEdit pb) => pb.ClassTest, delegate(IMemberConfigurationExpression<ClassTestForEditDTO> m)
			{
				m.MapFrom<ClassTest>((ClassTestForEditDTO pbdto) => (pbdto.ClassTest == null) ? null : pbdto.ClassTest.ToDomainObject());
			});
			Mapper.CreateMap<ClassTestForEdit, ClassTestForEditDTO>().ForMember((ClassTestForEditDTO pb) => pb.ClassTest, delegate(IMemberConfigurationExpression<ClassTestForEdit> m)
			{
				m.MapFrom<ClassTestDTO>((ClassTestForEdit pbdto) => (pbdto.ClassTest == null) ? null : pbdto.ClassTest.ToDTO());
			});
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0002136C File Offset: 0x0001F56C
		public static ClassTestForEdit ToDomainObject(this ClassTestForEditDTO classTestDTO)
		{
			return Mapper.Map<ClassTestForEditDTO, ClassTestForEdit>(classTestDTO);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00021384 File Offset: 0x0001F584
		public static ClassTestForEditDTO ToDTO(this ClassTestForEdit classTest)
		{
			return Mapper.Map<ClassTestForEdit, ClassTestForEditDTO>(classTest);
		}
	}
}
