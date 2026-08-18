using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000092 RID: 146
	public static class ColumnSortingRuleMapper
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000E138 File Offset: 0x0000C338
		static ColumnSortingRuleMapper()
		{
			Mapper.CreateMap<ColumnSortingRuleDTO, ColumnSortingRule>();
			Mapper.CreateMap<ColumnSortingRule, ColumnSortingRuleDTO>();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000E148 File Offset: 0x0000C348
		public static ColumnSortingRule ToDomainObject(this ColumnSortingRuleDTO executeReportResultDTO)
		{
			return Mapper.Map<ColumnSortingRuleDTO, ColumnSortingRule>(executeReportResultDTO);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000E160 File Offset: 0x0000C360
		public static ColumnSortingRuleDTO ToDTO(this ColumnSortingRule executeReportResult)
		{
			return Mapper.Map<ColumnSortingRule, ColumnSortingRuleDTO>(executeReportResult);
		}
	}
}
