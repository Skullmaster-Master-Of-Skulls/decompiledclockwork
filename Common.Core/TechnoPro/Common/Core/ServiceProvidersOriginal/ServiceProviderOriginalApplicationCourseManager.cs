using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000051 RID: 81
	public class ServiceProviderOriginalApplicationCourseManager : IServiceProviderOriginalApplicationCourseManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00011E92 File Offset: 0x00010092
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00011E9A File Offset: 0x0001009A
		public IServiceProviderOriginalApplicationCourseDAO dao { get; set; }

		// Token: 0x06000353 RID: 851 RVA: 0x00011EA3 File Offset: 0x000100A3
		public ServiceProviderOriginalApplicationCourseManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderOriginalApplicationCourseDAO(this.OpContext.GetProviderTypes());
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00011ECC File Offset: 0x000100CC
		// (set) Token: 0x06000355 RID: 853 RVA: 0x00011ED4 File Offset: 0x000100D4
		public OperationContext OpContext { get; set; }

		// Token: 0x06000356 RID: 854 RVA: 0x00011EE0 File Offset: 0x000100E0
		public IList<LookupCourseBase> GetProviderCourses(int ServiceProviderId, DateTime StartDate, DateTime EndDate, int ServiceProviderType)
		{
			return this.dao.GetProviderCourses(ServiceProviderId, StartDate, EndDate, ServiceProviderType);
		}
	}
}
