using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000097 RID: 151
	public static class ReportParameterMapper
	{
		// Token: 0x0600028A RID: 650 RVA: 0x0000E3CC File Offset: 0x0000C5CC
		static ReportParameterMapper()
		{
			Mapper.CreateMap<ReportParameterDTO, ReportParameter>();
			Mapper.CreateMap<ReportParameter, ReportParameterDTO>();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000E3DC File Offset: 0x0000C5DC
		public static ReportParameter ToDomainObject(this ReportParameterDTO dto)
		{
			return Mapper.Map<ReportParameterDTO, ReportParameter>(dto);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		public static ReportParameterDTO ToDTO(this ReportParameter item)
		{
			return Mapper.Map<ReportParameter, ReportParameterDTO>(item);
		}
	}
}
