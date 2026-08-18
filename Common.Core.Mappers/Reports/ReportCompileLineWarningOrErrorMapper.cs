using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x0200008B RID: 139
	public static class ReportCompileLineWarningOrErrorMapper
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0000D5F4 File Offset: 0x0000B7F4
		static ReportCompileLineWarningOrErrorMapper()
		{
			Mapper.CreateMap<ReportCompileLineWarningOrErrorDTO, ReportCompileLineWarningOrError>();
			Mapper.CreateMap<ReportCompileLineWarningOrError, ReportCompileLineWarningOrErrorDTO>();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000D604 File Offset: 0x0000B804
		public static ReportCompileLineWarningOrError ToDomainObject(this ReportCompileLineWarningOrErrorDTO dto)
		{
			return Mapper.Map<ReportCompileLineWarningOrErrorDTO, ReportCompileLineWarningOrError>(dto);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000D61C File Offset: 0x0000B81C
		public static ReportCompileLineWarningOrErrorDTO ToDTO(this ReportCompileLineWarningOrError item)
		{
			return Mapper.Map<ReportCompileLineWarningOrError, ReportCompileLineWarningOrErrorDTO>(item);
		}
	}
}
