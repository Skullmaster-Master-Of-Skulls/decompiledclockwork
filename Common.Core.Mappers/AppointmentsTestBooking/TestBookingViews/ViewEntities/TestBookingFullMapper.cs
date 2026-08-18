using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.TestBookingViews.ViewEntities
{
	// Token: 0x020001D4 RID: 468
	public static class TestBookingFullMapper
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x000223B4 File Offset: 0x000205B4
		static TestBookingFullMapper()
		{
			TestBookingSmallMapper.CreateMap();
			Mapper.CreateMap<TestBookingFullDTO, TestBookingFull>().ForMember((TestBookingFull pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TestBookingFullDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TestBookingFull, TestBookingFullDTO>();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00022438 File Offset: 0x00020638
		public static TestBookingFull ToDomainObject(this TestBookingFullDTO dto)
		{
			return Mapper.Map<TestBookingFullDTO, TestBookingFull>(dto);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00022450 File Offset: 0x00020650
		public static TestBookingFullDTO ToDTO(this TestBookingFull item)
		{
			return Mapper.Map<TestBookingFull, TestBookingFullDTO>(item);
		}
	}
}
