using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.ICore.ServiceProviders
{
	// Token: 0x0200003F RID: 63
	public interface IServiceProviderManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600019E RID: 414
		SPProvider LoadProviderById(int SPProviderId);

		// Token: 0x0600019F RID: 415
		SPProvider LoadProviderByStudent_no(string Student_no);

		// Token: 0x060001A0 RID: 416
		SPProvider LoadProviderByUserName(string UserName);

		// Token: 0x060001A1 RID: 417
		SPProvider LoadProviderByExternalId(string ExternalId);

		// Token: 0x060001A2 RID: 418
		int CreateProvider(SPProvider Provider);

		// Token: 0x060001A3 RID: 419
		void UpdateProvider(SPProvider Provider);

		// Token: 0x060001A4 RID: 420
		bool DeleteProvider(int SPProviderId);

		// Token: 0x060001A5 RID: 421
		int AddProviderCourseRegistration(SPProviderCourseRegistration CourseRegistration);

		// Token: 0x060001A6 RID: 422
		void UpdateProviderCourseRegistration(SPProviderCourseRegistration CourseRegistration);

		// Token: 0x060001A7 RID: 423
		void DeleteProviderCourseRegistration(int SPProviderCourseRegistrationId);

		// Token: 0x060001A8 RID: 424
		IList<SPProvider> LoadAllProvidersWithAtLeastOneActiveApplication(DateTime StartDate, DateTime EndDate);
	}
}
