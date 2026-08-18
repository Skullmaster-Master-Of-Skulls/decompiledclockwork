using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000088 RID: 136
	public static class FormattedReportMapper
	{
		// Token: 0x0600024E RID: 590 RVA: 0x0000D30C File Offset: 0x0000B50C
		static FormattedReportMapper()
		{
			Mapper.CreateMap<FormattedReportDTO, FormattedReport>().ForMember((FormattedReport pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<FormattedReportDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<FormattedReport, FormattedReportDTO>();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000D388 File Offset: 0x0000B588
		public static FormattedReport ToDomainObject(this FormattedReportDTO dto)
		{
			return Mapper.Map<FormattedReportDTO, FormattedReport>(dto);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		public static FormattedReportDTO ToDTO(this FormattedReport item)
		{
			return Mapper.Map<FormattedReport, FormattedReportDTO>(item);
		}
	}
}
