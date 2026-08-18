using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000096 RID: 150
	public static class ReportParameterFormMapper
	{
		// Token: 0x06000286 RID: 646 RVA: 0x0000E38C File Offset: 0x0000C58C
		static ReportParameterFormMapper()
		{
			Mapper.CreateMap<ReportParameterFormDTO, ReportParameterForm>();
			Mapper.CreateMap<ReportParameterForm, ReportParameterFormDTO>();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E39C File Offset: 0x0000C59C
		public static ReportParameterForm ToDomainObject(this ReportParameterFormDTO dto)
		{
			return Mapper.Map<ReportParameterFormDTO, ReportParameterForm>(dto);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		public static ReportParameterFormDTO ToDTO(this ReportParameterForm item)
		{
			return Mapper.Map<ReportParameterForm, ReportParameterFormDTO>(item);
		}
	}
}
