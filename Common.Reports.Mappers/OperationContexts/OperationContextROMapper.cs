using System;
using AutoMapper;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Reports.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Reports.Mappers.OperationContexts
{
	// Token: 0x02000003 RID: 3
	public static class OperationContextROMapper
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002094 File Offset: 0x00000294
		static OperationContextROMapper()
		{
			ApplicationContextROMapper.CreateMap();
			Mapper.CreateMap<OperationContextRO, OperationContext>().ForMember((OperationContext pb) => pb.AppContext, delegate(IMemberConfigurationExpression<OperationContextRO> m)
			{
				m.MapFrom<ApplicationContext>((OperationContextRO pbdto) => (pbdto.AppContext == null) ? null : pbdto.AppContext.ToDomainObject());
			});
			Mapper.CreateMap<OperationContext, OperationContextRO>().ForMember((OperationContextRO pb) => pb.AppContext, delegate(IMemberConfigurationExpression<OperationContext> m)
			{
				m.MapFrom<ApplicationContextRO>((OperationContext pbdto) => (pbdto.AppContext == null) ? null : pbdto.AppContext.ToReportObject());
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000205F File Offset: 0x0000025F
		public static void CreateMap()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002150 File Offset: 0x00000350
		public static OperationContext ToDomainObject(this OperationContextRO reportObject)
		{
			return Mapper.Map<OperationContextRO, OperationContext>(reportObject);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002168 File Offset: 0x00000368
		public static OperationContextRO ToReportObject(this OperationContext domainObject)
		{
			return Mapper.Map<OperationContext, OperationContextRO>(domainObject);
		}
	}
}
