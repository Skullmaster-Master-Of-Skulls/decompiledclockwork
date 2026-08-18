using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x0200008E RID: 142
	public static class ReportGroupMapper
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000D728 File Offset: 0x0000B928
		static ReportGroupMapper()
		{
			Mapper.CreateMap<ReportGroupDTO, ReportGroup>().ForMember((ReportGroup pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ReportGroupDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ReportGroup, ReportGroupDTO>();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		public static ReportGroup ToDomainObject(this ReportGroupDTO dto)
		{
			return Mapper.Map<ReportGroupDTO, ReportGroup>(dto);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		public static ReportGroupDTO ToDTO(this ReportGroup item)
		{
			return Mapper.Map<ReportGroup, ReportGroupDTO>(item);
		}
	}
}
