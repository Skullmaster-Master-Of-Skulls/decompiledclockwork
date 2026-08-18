using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.Common.Public.Entities.UnivDataAccess;
using TechnoPro.Common.Reports.Public.Entities.Database;

namespace TechnoPro.Common.Reports.Mappers.Database
{
	// Token: 0x02000005 RID: 5
	public static class QueryRequestROMapper
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021C0 File Offset: 0x000003C0
		static QueryRequestROMapper()
		{
			CommonParameterROMapper.CreateMap();
			Mapper.CreateMap<QueryRequestRO, QueryRequest>().ForMember((QueryRequest pb) => pb.Parameters, delegate(IMemberConfigurationExpression<QueryRequestRO> m)
			{
				m.MapFrom<List<CommonParameter>>((QueryRequestRO pbdto) => (pbdto.Parameters == null) ? null : (from g in pbdto.Parameters
				select g.ToDomainObject()).ToList<CommonParameter>());
			});
			Mapper.CreateMap<QueryRequest, QueryRequestRO>().ForMember((QueryRequestRO pb) => pb.Parameters, delegate(IMemberConfigurationExpression<QueryRequest> m)
			{
				m.MapFrom<List<CommonParameterRO>>((QueryRequest pbdto) => (pbdto.Parameters == null) ? null : (from g in pbdto.Parameters
				select g.ToReportObject()).ToList<CommonParameterRO>());
			});
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000205F File Offset: 0x0000025F
		public static void CreateMap()
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000227C File Offset: 0x0000047C
		public static QueryRequest ToDomainObject(this QueryRequestRO reportObject)
		{
			return Mapper.Map<QueryRequestRO, QueryRequest>(reportObject);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002294 File Offset: 0x00000494
		public static QueryRequestRO ToReportObject(this QueryRequest domainObject)
		{
			return Mapper.Map<QueryRequest, QueryRequestRO>(domainObject);
		}
	}
}
