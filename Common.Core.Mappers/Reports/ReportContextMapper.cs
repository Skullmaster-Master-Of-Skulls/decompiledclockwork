using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x0200008C RID: 140
	public static class ReportContextMapper
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000D634 File Offset: 0x0000B834
		static ReportContextMapper()
		{
			Mapper.CreateMap<ReportContextDTO, ReportContext>();
			Mapper.CreateMap<ReportContext, ReportContextDTO>();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000D644 File Offset: 0x0000B844
		public static ReportContext ToDomainObject(this ReportContextDTO dto)
		{
			return Mapper.Map<ReportContextDTO, ReportContext>(dto);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000D65C File Offset: 0x0000B85C
		public static ReportContextDTO ToDTO(this ReportContext item)
		{
			return Mapper.Map<ReportContext, ReportContextDTO>(item);
		}
	}
}
