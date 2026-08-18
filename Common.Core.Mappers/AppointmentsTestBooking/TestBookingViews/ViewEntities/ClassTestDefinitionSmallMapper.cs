using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.TestBookingViews.ViewEntities
{
	// Token: 0x020001D3 RID: 467
	public static class ClassTestDefinitionSmallMapper
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x00022300 File Offset: 0x00020500
		static ClassTestDefinitionSmallMapper()
		{
			TestBookingSmallMapper.CreateMap();
			Mapper.CreateMap<ClassTestDefinitionSmallDTO, ClassTestDefinitionSmall>().ForMember((ClassTestDefinitionSmall pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ClassTestDefinitionSmallDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ClassTestDefinitionSmall, ClassTestDefinitionSmallDTO>();
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00022384 File Offset: 0x00020584
		public static ClassTestDefinitionSmall ToDomainObject(this ClassTestDefinitionSmallDTO dto)
		{
			return Mapper.Map<ClassTestDefinitionSmallDTO, ClassTestDefinitionSmall>(dto);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0002239C File Offset: 0x0002059C
		public static ClassTestDefinitionSmallDTO ToDTO(this ClassTestDefinitionSmall item)
		{
			return Mapper.Map<ClassTestDefinitionSmall, ClassTestDefinitionSmallDTO>(item);
		}
	}
}
