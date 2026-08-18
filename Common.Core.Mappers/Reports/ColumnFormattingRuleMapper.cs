using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000091 RID: 145
	public static class ColumnFormattingRuleMapper
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000E0F8 File Offset: 0x0000C2F8
		static ColumnFormattingRuleMapper()
		{
			Mapper.CreateMap<ColumnFormattingRuleDTO, ColumnFormattingRule>();
			Mapper.CreateMap<ColumnFormattingRule, ColumnFormattingRuleDTO>();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000E108 File Offset: 0x0000C308
		public static ColumnFormattingRule ToDomainObject(this ColumnFormattingRuleDTO executeReportResultDTO)
		{
			return Mapper.Map<ColumnFormattingRuleDTO, ColumnFormattingRule>(executeReportResultDTO);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000E120 File Offset: 0x0000C320
		public static ColumnFormattingRuleDTO ToDTO(this ColumnFormattingRule executeReportResult)
		{
			return Mapper.Map<ColumnFormattingRule, ColumnFormattingRuleDTO>(executeReportResult);
		}
	}
}
