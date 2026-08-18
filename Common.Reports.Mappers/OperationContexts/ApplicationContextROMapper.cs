using System;
using AutoMapper;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Reports.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Reports.Mappers.OperationContexts
{
	// Token: 0x02000002 RID: 2
	public static class ApplicationContextROMapper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		static ApplicationContextROMapper()
		{
			Mapper.CreateMap<ApplicationContextRO, ApplicationContext>();
			Mapper.CreateMap<ApplicationContext, ApplicationContextRO>();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205F File Offset: 0x0000025F
		public static void CreateMap()
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		public static ApplicationContext ToDomainObject(this ApplicationContextRO reportObject)
		{
			return Mapper.Map<ApplicationContextRO, ApplicationContext>(reportObject);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000207C File Offset: 0x0000027C
		public static ApplicationContextRO ToReportObject(this ApplicationContext domainObject)
		{
			return Mapper.Map<ApplicationContext, ApplicationContextRO>(domainObject);
		}
	}
}
