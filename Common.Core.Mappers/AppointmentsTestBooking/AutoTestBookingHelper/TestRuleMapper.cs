using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001F6 RID: 502
	public static class TestRuleMapper
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x000246C0 File Offset: 0x000228C0
		static TestRuleMapper()
		{
			Mapper.CreateMap<TestRuleDTO, TestRule>().ForMember((TestRule pb) => pb.RoomIdsToExclud, delegate(IMemberConfigurationExpression<TestRuleDTO> m)
			{
				m.MapFrom<List<int>>((TestRuleDTO pbdto) => (pbdto.RoomIdsToExclud == null) ? null : pbdto.RoomIdsToExclud.ToList<int>());
			});
			Mapper.CreateMap<TestRule, TestRuleDTO>().ForMember((TestRuleDTO pb) => pb.RoomIdsToExclud, delegate(IMemberConfigurationExpression<TestRule> m)
			{
				m.MapFrom<List<int>>((TestRule pbdto) => (pbdto.RoomIdsToExclud == null) ? null : pbdto.RoomIdsToExclud.ToList<int>());
			});
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00024778 File Offset: 0x00022978
		public static TestRule ToDomainObject(this TestRuleDTO accommodationForTestDTO)
		{
			return Mapper.Map<TestRuleDTO, TestRule>(accommodationForTestDTO);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00024790 File Offset: 0x00022990
		public static TestRuleDTO ToDTO(this TestRule accommodationForTest)
		{
			return Mapper.Map<TestRule, TestRuleDTO>(accommodationForTest);
		}
	}
}
