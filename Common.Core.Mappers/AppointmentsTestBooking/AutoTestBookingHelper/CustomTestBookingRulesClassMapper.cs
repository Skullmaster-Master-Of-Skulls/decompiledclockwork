using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x020001EB RID: 491
	public static class CustomTestBookingRulesClassMapper
	{
		// Token: 0x0600084F RID: 2127 RVA: 0x000237D4 File Offset: 0x000219D4
		static CustomTestBookingRulesClassMapper()
		{
			Mapper.CreateMap<CustomTestBookingRulesClassDTO, CustomTestBookingRulesClass>();
			Mapper.CreateMap<CustomTestBookingRulesClass, CustomTestBookingRulesClassDTO>();
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000237E4 File Offset: 0x000219E4
		public static CustomTestBookingRulesClass ToDomainObject(this CustomTestBookingRulesClassDTO dto)
		{
			return Mapper.Map<CustomTestBookingRulesClassDTO, CustomTestBookingRulesClass>(dto);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000237FC File Offset: 0x000219FC
		public static CustomTestBookingRulesClassDTO ToDTO(this CustomTestBookingRulesClass item)
		{
			return Mapper.Map<CustomTestBookingRulesClass, CustomTestBookingRulesClassDTO>(item);
		}
	}
}
