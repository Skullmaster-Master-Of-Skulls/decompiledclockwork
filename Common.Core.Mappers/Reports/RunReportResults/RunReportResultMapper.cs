using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Mappers.Reports.RunReportResults
{
	// Token: 0x0200009E RID: 158
	public static class RunReportResultMapper
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x0000E870 File Offset: 0x0000CA70
		static RunReportResultMapper()
		{
			RunFunctionDataMapper.CreateMap();
			RunFunctionResultMapper.CreateMap();
			ReportMapper.CreateMap();
			RunStatusMapper.CreateMap();
			ReportParameterMapper.CreateMap();
			ReportExecutionPlanMapper.CreateMap();
			Mapper.CreateMap<RunReportResultDTO, RunReportResult>().ForMember((RunReportResult pb) => pb.CurrentReportParameters, delegate(IMemberConfigurationExpression<RunReportResultDTO> m)
			{
				m.MapFrom<List<ReportParameter>>((RunReportResultDTO pbdto) => (pbdto.CurrentReportParameters == null) ? null : pbdto.CurrentReportParameters.ToList<ReportParameterDTO>().ConvertAll<ReportParameter>((ReportParameterDTO g) => g.ToDomainObject()));
			}).ForMember((RunReportResult pb) => pb.ExecutionPlan, delegate(IMemberConfigurationExpression<RunReportResultDTO> m)
			{
				m.MapFrom<ReportExecutionPlan>((RunReportResultDTO pbdto) => (pbdto.ExecutionPlan == null) ? null : pbdto.ExecutionPlan.ToDomainObject());
			}).ForMember((RunReportResult pb) => pb.PrimaryData, delegate(IMemberConfigurationExpression<RunReportResultDTO> m)
			{
				m.MapFrom<RunFunctionData>((RunReportResultDTO pbdto) => (pbdto.PrimaryData == null) ? null : pbdto.PrimaryData.ToDomainObject());
			});
			Mapper.CreateMap<RunReportResult, RunReportResultDTO>().ForMember((RunReportResultDTO pb) => pb.CurrentReportParameters, delegate(IMemberConfigurationExpression<RunReportResult> m)
			{
				m.MapFrom<List<ReportParameterDTO>>((RunReportResult pbdto) => (pbdto.CurrentReportParameters == null) ? null : pbdto.CurrentReportParameters.ToList<ReportParameter>().ConvertAll<ReportParameterDTO>((ReportParameter g) => g.ToDTO()));
			}).ForMember((RunReportResultDTO pb) => pb.ExecutionPlan, delegate(IMemberConfigurationExpression<RunReportResult> m)
			{
				m.MapFrom<ReportExecutionPlanDTO>((RunReportResult pbdto) => (pbdto.ExecutionPlan == null) ? null : pbdto.ExecutionPlan.ToDTO());
			}).ForMember((RunReportResultDTO pb) => pb.PrimaryData, delegate(IMemberConfigurationExpression<RunReportResult> m)
			{
				m.MapFrom<RunFunctionDataDTO>((RunReportResult pbdto) => (pbdto.PrimaryData == null) ? null : pbdto.PrimaryData.ToDTO());
			});
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000EA84 File Offset: 0x0000CC84
		public static RunReportResult ToDomainObject(this RunReportResultDTO dto)
		{
			return Mapper.Map<RunReportResultDTO, RunReportResult>(dto);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		public static RunReportResultDTO ToDTO(this RunReportResult item)
		{
			try
			{
				return Mapper.Map<RunReportResult, RunReportResultDTO>(item);
			}
			catch (Exception ex)
			{
			}
			return null;
		}
	}
}
