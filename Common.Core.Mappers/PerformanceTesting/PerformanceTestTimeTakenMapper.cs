using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting;
using TechnoPro.Common.Public.Entities.PerformanceTesting;

namespace TechnoPro.Common.Core.Mappers.PerformanceTesting
{
	// Token: 0x020000AE RID: 174
	public static class PerformanceTestTimeTakenMapper
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x0000F494 File Offset: 0x0000D694
		static PerformanceTestTimeTakenMapper()
		{
			Mapper.CreateMap<PerformanceTestTimeTakenDTO, PerformanceTestTimeTaken>();
			Mapper.CreateMap<PerformanceTestTimeTaken, PerformanceTestTimeTakenDTO>();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000F4A4 File Offset: 0x0000D6A4
		public static PerformanceTestTimeTaken ToDomainObject(this PerformanceTestTimeTakenDTO dto)
		{
			return Mapper.Map<PerformanceTestTimeTakenDTO, PerformanceTestTimeTaken>(dto);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000F4BC File Offset: 0x0000D6BC
		public static PerformanceTestTimeTakenDTO ToDTO(this PerformanceTestTimeTaken item)
		{
			return Mapper.Map<PerformanceTestTimeTaken, PerformanceTestTimeTakenDTO>(item);
		}
	}
}
