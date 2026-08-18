using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000004 RID: 4
	public interface IServiceProviderOriginalApplicationCourseDAO : IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x06000001 RID: 1
		IList<LookupCourseBase> GetProviderCourses(int ServiceProviderId, DateTime StartDate, DateTime EndDate, int ServiceProviderType);
	}
}
