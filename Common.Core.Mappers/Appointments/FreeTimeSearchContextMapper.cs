using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001B2 RID: 434
	public static class FreeTimeSearchContextMapper
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x00020662 File Offset: 0x0001E862
		static FreeTimeSearchContextMapper()
		{
			FreeTimeSearchRecurringRuleMapper.CreateMap();
			Mapper.CreateMap<FreeTimeSearchContextDTO, FreeTimeSearchContext>();
			Mapper.CreateMap<FreeTimeSearchContext, FreeTimeSearchContextDTO>();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00020678 File Offset: 0x0001E878
		public static FreeTimeSearchContext ToDomainObject(this FreeTimeSearchContextDTO dto)
		{
			return Mapper.Map<FreeTimeSearchContextDTO, FreeTimeSearchContext>(dto);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00020690 File Offset: 0x0001E890
		public static FreeTimeSearchContextDTO ToDTO(this FreeTimeSearchContext item)
		{
			return Mapper.Map<FreeTimeSearchContext, FreeTimeSearchContextDTO>(item);
		}
	}
}
