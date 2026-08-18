using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000087 RID: 135
	public static class ExecuteReportResultMapper
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0000D2CA File Offset: 0x0000B4CA
		static ExecuteReportResultMapper()
		{
			Mapper.CreateMap<ExecuteReportResultDTO, ExecuteReportResult>();
			Mapper.CreateMap<ExecuteReportResult, ExecuteReportResultDTO>();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public static ExecuteReportResult ToDomainObject(this ExecuteReportResultDTO executeReportResultDTO)
		{
			return Mapper.Map<ExecuteReportResultDTO, ExecuteReportResult>(executeReportResultDTO);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		public static ExecuteReportResultDTO ToDTO(this ExecuteReportResult executeReportResult)
		{
			return Mapper.Map<ExecuteReportResult, ExecuteReportResultDTO>(executeReportResult);
		}
	}
}
