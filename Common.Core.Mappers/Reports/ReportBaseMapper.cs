using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x02000089 RID: 137
	public static class ReportBaseMapper
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		static ReportBaseMapper()
		{
			Mapper.CreateMap<ReportBaseDTO, ReportBase>().ForMember((ReportBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ReportBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ReportBase, ReportBaseDTO>();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000D434 File Offset: 0x0000B634
		public static ReportBase ToDomainObject(this ReportBaseDTO dto)
		{
			return Mapper.Map<ReportBaseDTO, ReportBase>(dto);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000D44C File Offset: 0x0000B64C
		public static ReportBaseDTO ToDTO(this ReportBase item)
		{
			return Mapper.Map<ReportBase, ReportBaseDTO>(item);
		}
	}
}
