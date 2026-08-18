using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000090 RID: 144
	public static class ReportOptionsMapper
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0000DD90 File Offset: 0x0000BF90
		static ReportOptionsMapper()
		{
			ColumnFormattingRuleMapper.CreateMap();
			ColumnSortingRuleMapper.CreateMap();
			RowFormattingMapper.CreateMap();
			Mapper.CreateMap<ReportOptionsDTO, ReportOptions>().ForMember((ReportOptions pb) => pb.ColumnFormattingRules, delegate(IMemberConfigurationExpression<ReportOptionsDTO> m)
			{
				m.MapFrom<List<ColumnFormattingRule>>((ReportOptionsDTO pbdto) => (pbdto.ColumnFormattingRules == null) ? null : pbdto.ColumnFormattingRules.ToList<ColumnFormattingRuleDTO>().ConvertAll<ColumnFormattingRule>((ColumnFormattingRuleDTO g) => g.ToDomainObject()));
			}).ForMember((ReportOptions pb) => pb.ColumnsToHide, delegate(IMemberConfigurationExpression<ReportOptionsDTO> m)
			{
				m.MapFrom<List<string>>((ReportOptionsDTO pbdto) => (pbdto.ColumnsToHide == null) ? null : pbdto.ColumnsToHide.ToList<string>());
			}).ForMember((ReportOptions pb) => pb.GroupingColumns, delegate(IMemberConfigurationExpression<ReportOptionsDTO> m)
			{
				m.MapFrom<List<string>>((ReportOptionsDTO pbdto) => (pbdto.GroupingColumns == null) ? null : pbdto.GroupingColumns.ToList<string>());
			}).ForMember((ReportOptions pb) => pb.TableSortingRule, delegate(IMemberConfigurationExpression<ReportOptionsDTO> m)
			{
				m.MapFrom<List<ColumnSortingRule>>((ReportOptionsDTO pbdto) => (pbdto.TableSortingRule == null) ? null : pbdto.TableSortingRule.ToList<ColumnSortingRuleDTO>().ConvertAll<ColumnSortingRule>((ColumnSortingRuleDTO g) => g.ToDomainObject()));
			}).ForMember((ReportOptions pb) => pb.RowFormattings, delegate(IMemberConfigurationExpression<ReportOptionsDTO> m)
			{
				m.MapFrom<List<RowFormattingDTO>>((ReportOptionsDTO pbdto) => (pbdto.RowFormattings == null) ? null : pbdto.RowFormattings.ToList<RowFormattingDTO>());
			});
			Mapper.CreateMap<ReportOptions, ReportOptionsDTO>().ForMember((ReportOptionsDTO pb) => pb.ColumnFormattingRules, delegate(IMemberConfigurationExpression<ReportOptions> m)
			{
				m.MapFrom<List<ColumnFormattingRuleDTO>>((ReportOptions pbdto) => (pbdto.ColumnFormattingRules == null) ? null : pbdto.ColumnFormattingRules.ToList<ColumnFormattingRule>().ConvertAll<ColumnFormattingRuleDTO>((ColumnFormattingRule g) => g.ToDTO()));
			}).ForMember((ReportOptionsDTO pb) => pb.ColumnsToHide, delegate(IMemberConfigurationExpression<ReportOptions> m)
			{
				m.MapFrom<List<string>>((ReportOptions pbdto) => (pbdto.ColumnsToHide == null) ? null : pbdto.ColumnsToHide.ToList<string>());
			}).ForMember((ReportOptionsDTO pb) => pb.GroupingColumns, delegate(IMemberConfigurationExpression<ReportOptions> m)
			{
				m.MapFrom<List<string>>((ReportOptions pbdto) => (pbdto.GroupingColumns == null) ? null : pbdto.GroupingColumns.ToList<string>());
			}).ForMember((ReportOptionsDTO pb) => pb.TableSortingRule, delegate(IMemberConfigurationExpression<ReportOptions> m)
			{
				m.MapFrom<List<ColumnSortingRuleDTO>>((ReportOptions pbdto) => (pbdto.TableSortingRule == null) ? null : pbdto.TableSortingRule.ToList<ColumnSortingRule>().ConvertAll<ColumnSortingRuleDTO>((ColumnSortingRule g) => g.ToDTO()));
			}).ForMember((ReportOptionsDTO pb) => pb.RowFormattings, delegate(IMemberConfigurationExpression<ReportOptions> m)
			{
				m.MapFrom<List<RowFormatting>>((ReportOptions pbdto) => (pbdto.RowFormattings == null) ? null : pbdto.RowFormattings.ToList<RowFormatting>());
			});
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000E0C8 File Offset: 0x0000C2C8
		public static ReportOptions ToDomainObject(this ReportOptionsDTO executeReportResultDTO)
		{
			return Mapper.Map<ReportOptionsDTO, ReportOptions>(executeReportResultDTO);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		public static ReportOptionsDTO ToDTO(this ReportOptions executeReportResult)
		{
			return Mapper.Map<ReportOptions, ReportOptionsDTO>(executeReportResult);
		}
	}
}
