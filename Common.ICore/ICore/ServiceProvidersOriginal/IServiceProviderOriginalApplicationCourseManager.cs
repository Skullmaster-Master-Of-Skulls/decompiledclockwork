using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.ICore.ServiceProvidersOriginal
{
	// Token: 0x02000044 RID: 68
	public interface IServiceProviderOriginalApplicationCourseManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001BC RID: 444
		IList<LookupCourseBase> GetProviderCourses(int ServiceProviderId, DateTime StartDate, DateTime EndDate, int ServiceProviderType);
	}
}
