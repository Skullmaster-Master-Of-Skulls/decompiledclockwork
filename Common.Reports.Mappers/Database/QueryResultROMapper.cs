using System;
using AutoMapper;
using TechnoPro.Common.Public.Entities.UnivDataAccess;
using TechnoPro.Common.Reports.Public.Entities.Database;

namespace TechnoPro.Common.Reports.Mappers.Database
{
	// Token: 0x02000006 RID: 6
	public static class QueryResultROMapper
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000022AC File Offset: 0x000004AC
		static QueryResultROMapper()
		{
			Mapper.CreateMap<QueryResultRO, QueryResult>();
			Mapper.CreateMap<QueryResult, QueryResultRO>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000205F File Offset: 0x0000025F
		public static void CreateMap()
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022BC File Offset: 0x000004BC
		public static QueryResult ToDomainObject(this QueryResultRO reportObject)
		{
			return Mapper.Map<QueryResultRO, QueryResult>(reportObject);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022D4 File Offset: 0x000004D4
		public static QueryResultRO ToReportObject(this QueryResult domainObject)
		{
			return Mapper.Map<QueryResult, QueryResultRO>(domainObject);
		}
	}
}
