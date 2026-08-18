using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x0200008A RID: 138
	public static class ReportCollectionMapper
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000D464 File Offset: 0x0000B664
		static ReportCollectionMapper()
		{
			ReportMapper.CreateMap();
			ReportGroupMapper.CreateMap();
			Mapper.CreateMap<ReportCollectionDTO, ReportCollection>().ForMember((ReportCollection pb) => pb.Reports, delegate(IMemberConfigurationExpression<ReportCollectionDTO> m)
			{
				m.MapFrom<List<Report>>((ReportCollectionDTO pbdto) => (pbdto.Reports == null) ? null : pbdto.Reports.ToList<ReportDTO>().ConvertAll<Report>((ReportDTO g) => g.ToDomainObject()));
			}).ForMember((ReportCollection pb) => pb.ReportGroups, delegate(IMemberConfigurationExpression<ReportCollectionDTO> m)
			{
				m.MapFrom<List<ReportGroup>>((ReportCollectionDTO pbdto) => (pbdto.ReportGroups == null) ? null : pbdto.ReportGroups.ToList<ReportGroupDTO>().ConvertAll<ReportGroup>((ReportGroupDTO g) => g.ToDomainObject()));
			});
			Mapper.CreateMap<ReportCollection, ReportCollectionDTO>().ForMember((ReportCollectionDTO pb) => pb.Reports, delegate(IMemberConfigurationExpression<ReportCollection> m)
			{
				m.MapFrom<List<ReportDTO>>((ReportCollection pbdto) => (pbdto.Reports == null) ? null : pbdto.Reports.ToList<Report>().ConvertAll<ReportDTO>((Report g) => g.ToDTO()));
			}).ForMember((ReportCollectionDTO pb) => pb.ReportGroups, delegate(IMemberConfigurationExpression<ReportCollection> m)
			{
				m.MapFrom<List<ReportGroupDTO>>((ReportCollection pbdto) => (pbdto.ReportGroups == null) ? null : pbdto.ReportGroups.ToList<ReportGroup>().ConvertAll<ReportGroupDTO>((ReportGroup g) => g.ToDTO()));
			});
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		public static ReportCollection ToDomainObject(this ReportCollectionDTO dto)
		{
			return Mapper.Map<ReportCollectionDTO, ReportCollection>(dto);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		public static ReportCollectionDTO ToDTO(this ReportCollection item)
		{
			return Mapper.Map<ReportCollection, ReportCollectionDTO>(item);
		}
	}
}
