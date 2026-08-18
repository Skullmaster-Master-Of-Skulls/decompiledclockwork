using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000098 RID: 152
	public static class ReportParametersLegacyMapper
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000E40C File Offset: 0x0000C60C
		static ReportParametersLegacyMapper()
		{
			Mapper.CreateMap<ReportParametersLegacyDTO, ReportParametersLegacy>();
			Mapper.CreateMap<ReportParametersLegacy, ReportParametersLegacyDTO>();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000E41C File Offset: 0x0000C61C
		public static ReportParametersLegacy ToDomainObject(this ReportParametersLegacyDTO dto)
		{
			return Mapper.Map<ReportParametersLegacyDTO, ReportParametersLegacy>(dto);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000E434 File Offset: 0x0000C634
		public static ReportParametersLegacyDTO ToDTO(this ReportParametersLegacy item)
		{
			return Mapper.Map<ReportParametersLegacy, ReportParametersLegacyDTO>(item);
		}
	}
}
