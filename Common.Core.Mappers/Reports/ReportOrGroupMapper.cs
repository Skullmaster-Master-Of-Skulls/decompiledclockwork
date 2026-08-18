using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000095 RID: 149
	public static class ReportOrGroupMapper
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000E340 File Offset: 0x0000C540
		static ReportOrGroupMapper()
		{
			ReportGroupMapper.CreateMap();
			ReportMapper.CreateMap();
			Mapper.CreateMap<ReportOrGroupDTO, ReportOrGroup>();
			Mapper.CreateMap<ReportOrGroup, ReportOrGroupDTO>();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000E35C File Offset: 0x0000C55C
		public static ReportOrGroup ToDomainObject(this ReportOrGroupDTO dto)
		{
			return Mapper.Map<ReportOrGroupDTO, ReportOrGroup>(dto);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000E374 File Offset: 0x0000C574
		public static ReportOrGroupDTO ToDTO(this ReportOrGroup item)
		{
			return Mapper.Map<ReportOrGroup, ReportOrGroupDTO>(item);
		}
	}
}
