using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x0200008D RID: 141
	public static class ReportFunctionMapper
	{
		// Token: 0x06000262 RID: 610 RVA: 0x0000D674 File Offset: 0x0000B874
		static ReportFunctionMapper()
		{
			ReportParameterMapper.CreateMap();
			Mapper.CreateMap<ReportFunctionDTO, ReportFunction>().ForMember((ReportFunction pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ReportFunctionDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ReportFunction, ReportFunctionDTO>();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		public static ReportFunction ToDomainObject(this ReportFunctionDTO dto)
		{
			return Mapper.Map<ReportFunctionDTO, ReportFunction>(dto);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D710 File Offset: 0x0000B910
		public static ReportFunctionDTO ToDTO(this ReportFunction item)
		{
			return Mapper.Map<ReportFunction, ReportFunctionDTO>(item);
		}
	}
}
