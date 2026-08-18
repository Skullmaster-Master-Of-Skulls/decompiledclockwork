using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Mappers.Reports.RunReportResults
{
	// Token: 0x0200009C RID: 156
	public static class RunFunctionResultMapper
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000E72A File Offset: 0x0000C92A
		static RunFunctionResultMapper()
		{
			ReportFunctionMapper.CreateMap();
			RunStatusMapper.CreateMap();
			Mapper.CreateMap<RunFunctionResultDTO, RunFunctionResult>();
			Mapper.CreateMap<RunFunctionResult, RunFunctionResultDTO>();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000E748 File Offset: 0x0000C948
		public static RunFunctionResult ToDomainObject(this RunFunctionResultDTO dto)
		{
			return Mapper.Map<RunFunctionResultDTO, RunFunctionResult>(dto);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000E760 File Offset: 0x0000C960
		public static RunFunctionResultDTO ToDTO(this RunFunctionResult item)
		{
			return Mapper.Map<RunFunctionResult, RunFunctionResultDTO>(item);
		}
	}
}
