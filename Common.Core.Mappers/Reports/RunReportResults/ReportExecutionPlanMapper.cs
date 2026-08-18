using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Mappers.Reports.RunReportResults
{
	// Token: 0x0200009A RID: 154
	public static class ReportExecutionPlanMapper
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000E48C File Offset: 0x0000C68C
		static ReportExecutionPlanMapper()
		{
			ExecuteReportPlanItemMapper.CreateMap();
			Mapper.CreateMap<ReportExecutionPlanDTO, ReportExecutionPlan>().ForMember((ReportExecutionPlan pb) => pb.ExecutionSteps, delegate(IMemberConfigurationExpression<ReportExecutionPlanDTO> m)
			{
				m.MapFrom<List<ExecuteReportPlanItem>>((ReportExecutionPlanDTO pbdto) => (pbdto.ExecutionSteps == null) ? null : pbdto.ExecutionSteps.ToList<ExecuteReportPlanItemDTO>().ConvertAll<ExecuteReportPlanItem>((ExecuteReportPlanItemDTO g) => g.ToDomainObject()));
			});
			Mapper.CreateMap<ReportExecutionPlan, ReportExecutionPlanDTO>().ForMember((ReportExecutionPlanDTO pb) => pb.ExecutionSteps, delegate(IMemberConfigurationExpression<ReportExecutionPlan> m)
			{
				m.MapFrom<List<ExecuteReportPlanItemDTO>>((ReportExecutionPlan pbdto) => (pbdto.ExecutionSteps == null) ? null : pbdto.ExecutionSteps.ToList<ExecuteReportPlanItem>().ConvertAll<ExecuteReportPlanItemDTO>((ExecuteReportPlanItem g) => g.ToDTO()));
			});
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000E548 File Offset: 0x0000C748
		public static ReportExecutionPlan ToDomainObject(this ReportExecutionPlanDTO dto)
		{
			return Mapper.Map<ReportExecutionPlanDTO, ReportExecutionPlan>(dto);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000E560 File Offset: 0x0000C760
		public static ReportExecutionPlanDTO ToDTO(this ReportExecutionPlan item)
		{
			return Mapper.Map<ReportExecutionPlan, ReportExecutionPlanDTO>(item);
		}
	}
}
