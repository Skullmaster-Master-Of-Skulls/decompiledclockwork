using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.ICore.ServiceProviders
{
	// Token: 0x0200003D RID: 61
	public interface IServiceProviderCourseRegistrationManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000192 RID: 402
		IList<SPProviderCourseRegistration> LoadCourseRegistrationsByProvider(int SPProviderId, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000193 RID: 403
		SPProviderCourseRegistration LoadCourseRegistrationById(int SPProviderCourseRegistrationId);

		// Token: 0x06000194 RID: 404
		void UpdateCourseRegistrationStatus(int SPProviderCourseRegistrationId, CourseRegistrationStatus NewStatus);

		// Token: 0x06000195 RID: 405
		void UpdateCourseRegistration(SPProviderCourseRegistration ProviderCourseRegistration);

		// Token: 0x06000196 RID: 406
		void DeleteCourseRegistration(int SPProviderCourseRegistrationId);

		// Token: 0x06000197 RID: 407
		int CreateCourseRegistration(SPProviderCourseRegistration ProviderCourseRegistration);
	}
}
