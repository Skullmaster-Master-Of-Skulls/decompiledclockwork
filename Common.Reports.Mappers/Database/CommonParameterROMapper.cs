using System;
using AutoMapper;
using TechnoPro.Common.Public.Entities.UnivDataAccess;
using TechnoPro.Common.Reports.Public.Entities.Database;

namespace TechnoPro.Common.Reports.Mappers.Database
{
	// Token: 0x02000004 RID: 4
	public static class CommonParameterROMapper
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002180 File Offset: 0x00000380
		static CommonParameterROMapper()
		{
			Mapper.CreateMap<CommonParameterRO, CommonParameter>();
			Mapper.CreateMap<CommonParameter, CommonParameterRO>();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000205F File Offset: 0x0000025F
		public static void CreateMap()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002190 File Offset: 0x00000390
		public static CommonParameter ToDomainObject(this CommonParameterRO reportObject)
		{
			return Mapper.Map<CommonParameterRO, CommonParameter>(reportObject);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021A8 File Offset: 0x000003A8
		public static CommonParameterRO ToReportObject(this CommonParameter domainObject)
		{
			return Mapper.Map<CommonParameter, CommonParameterRO>(domainObject);
		}
	}
}
