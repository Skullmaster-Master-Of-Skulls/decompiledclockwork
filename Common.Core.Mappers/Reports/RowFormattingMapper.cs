using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000093 RID: 147
	public static class RowFormattingMapper
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000E178 File Offset: 0x0000C378
		static RowFormattingMapper()
		{
			Mapper.CreateMap<RowFormattingDTO, RowFormatting>();
			Mapper.CreateMap<RowFormatting, RowFormattingDTO>();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000E188 File Offset: 0x0000C388
		public static RowFormatting ToDomainObject(this RowFormattingDTO executeReportResultDTO)
		{
			return Mapper.Map<RowFormattingDTO, RowFormatting>(executeReportResultDTO);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public static RowFormattingDTO ToDTO(this RowFormatting executeReportResult)
		{
			return Mapper.Map<RowFormatting, RowFormattingDTO>(executeReportResult);
		}
	}
}
