using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001B3 RID: 435
	public static class FreeTimeSearchRecurringRuleMapper
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x000206A8 File Offset: 0x0001E8A8
		static FreeTimeSearchRecurringRuleMapper()
		{
			Mapper.CreateMap<FreeTimeSearchRecurringRuleDTO, FreeTimeSearchRecurringRule>();
			Mapper.CreateMap<FreeTimeSearchRecurringRule, FreeTimeSearchRecurringRuleDTO>();
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000206B8 File Offset: 0x0001E8B8
		public static FreeTimeSearchRecurringRule ToDomainObject(this FreeTimeSearchRecurringRuleDTO dto)
		{
			return Mapper.Map<FreeTimeSearchRecurringRuleDTO, FreeTimeSearchRecurringRule>(dto);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000206D0 File Offset: 0x0001E8D0
		public static FreeTimeSearchRecurringRuleDTO ToDTO(this FreeTimeSearchRecurringRule item)
		{
			return Mapper.Map<FreeTimeSearchRecurringRule, FreeTimeSearchRecurringRuleDTO>(item);
		}
	}
}
