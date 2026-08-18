using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.ServiceProvider
{
	// Token: 0x0200001C RID: 28
	public interface IServiceProviderCourseRegistrationClientManager : IWebService
	{
		// Token: 0x060000AE RID: 174
		IList<SPProviderCourseRegistrationDTO> LoadCourseRegistrationsByProvider(int SPProviderId, DateTime StartDate, DateTime EndDate);

		// Token: 0x060000AF RID: 175
		SPProviderCourseRegistrationDTO LoadCourseRegistrationById(int SPProviderCourseRegistrationId);

		// Token: 0x060000B0 RID: 176
		void UpdateCourseRegistrationStatus(int SPProviderCourseRegistrationId, CourseRegistrationStatusDTO NewStatus);

		// Token: 0x060000B1 RID: 177
		void UpdateCourseRegistration(SPProviderCourseRegistrationDTO ProviderCourseRegistration);

		// Token: 0x060000B2 RID: 178
		void DeleteCourseRegistration(int SPProviderCourseRegistrationId);

		// Token: 0x060000B3 RID: 179
		int CreateCourseRegistration(SPProviderCourseRegistrationDTO ProviderCourseRegistration);
	}
}
