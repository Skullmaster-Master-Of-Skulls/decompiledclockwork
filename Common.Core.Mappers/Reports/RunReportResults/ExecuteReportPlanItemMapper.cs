using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Mappers.Reports.RunReportResults
{
	// Token: 0x02000099 RID: 153
	public static class ExecuteReportPlanItemMapper
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0000E44C File Offset: 0x0000C64C
		static ExecuteReportPlanItemMapper()
		{
			Mapper.CreateMap<ExecuteReportPlanItemDTO, ExecuteReportPlanItem>();
			Mapper.CreateMap<ExecuteReportPlanItem, ExecuteReportPlanItemDTO>();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000E45C File Offset: 0x0000C65C
		public static ExecuteReportPlanItem ToDomainObject(this ExecuteReportPlanItemDTO dto)
		{
			return Mapper.Map<ExecuteReportPlanItemDTO, ExecuteReportPlanItem>(dto);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000E474 File Offset: 0x0000C674
		public static ExecuteReportPlanItemDTO ToDTO(this ExecuteReportPlanItem item)
		{
			return Mapper.Map<ExecuteReportPlanItem, ExecuteReportPlanItemDTO>(item);
		}
	}
}
