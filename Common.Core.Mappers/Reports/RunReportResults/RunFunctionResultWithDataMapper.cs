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
	// Token: 0x0200009D RID: 157
	public static class RunFunctionResultWithDataMapper
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000E778 File Offset: 0x0000C978
		static RunFunctionResultWithDataMapper()
		{
			RunFunctionDataMapper.CreateMap();
			RunFunctionResultMapper.CreateMap();
			ReportParameterMapper.CreateMap();
			Mapper.CreateMap<RunFunctionResultWithDataDTO, RunFunctionResultWithData>().ForMember((RunFunctionResultWithData pb) => pb.ReportParametersOut, delegate(IMemberConfigurationExpression<RunFunctionResultWithDataDTO> m)
			{
				m.MapFrom<List<ReportParameter>>((RunFunctionResultWithDataDTO pbdto) => (pbdto.ReportParametersOut == null) ? null : pbdto.ReportParametersOut.ToList<ReportParameterDTO>().ConvertAll<ReportParameter>((ReportParameterDTO g) => g.ToDomainObject()));
			});
			Mapper.CreateMap<RunFunctionResultWithData, RunFunctionResultWithDataDTO>().ForMember((RunFunctionResultWithDataDTO pb) => pb.ReportParametersOut, delegate(IMemberConfigurationExpression<RunFunctionResultWithData> m)
			{
				m.MapFrom<List<ReportParameterDTO>>((RunFunctionResultWithData pbdto) => (pbdto.ReportParametersOut == null) ? null : pbdto.ReportParametersOut.ToList<ReportParameter>().ConvertAll<ReportParameterDTO>((ReportParameter g) => g.ToDTO()));
			});
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000E840 File Offset: 0x0000CA40
		public static RunFunctionResultWithData ToDomainObject(this RunFunctionResultWithDataDTO dto)
		{
			return Mapper.Map<RunFunctionResultWithDataDTO, RunFunctionResultWithData>(dto);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000E858 File Offset: 0x0000CA58
		public static RunFunctionResultWithDataDTO ToDTO(this RunFunctionResultWithData item)
		{
			return Mapper.Map<RunFunctionResultWithData, RunFunctionResultWithDataDTO>(item);
		}
	}
}
