using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.ServiceProvider
{
	// Token: 0x0200001B RID: 27
	public interface IServiceProviderClientManager : IWebService
	{
		// Token: 0x060000A3 RID: 163
		SPProviderDTO LoadProviderById(int SPProviderId);

		// Token: 0x060000A4 RID: 164
		SPProviderDTO LoadProviderByStudent_no(string Student_no);

		// Token: 0x060000A5 RID: 165
		SPProviderDTO LoadProviderByUserName(string UserName);

		// Token: 0x060000A6 RID: 166
		SPProviderDTO LoadProviderByExternalId(string ExternalId);

		// Token: 0x060000A7 RID: 167
		int CreateProvider(SPProviderDTO Provider);

		// Token: 0x060000A8 RID: 168
		void UpdateProvider(SPProviderDTO Provider);

		// Token: 0x060000A9 RID: 169
		bool DeleteProvider(int SPProviderId);

		// Token: 0x060000AA RID: 170
		int AddProviderCourseRegistration(SPProviderCourseRegistrationDTO CourseRegistration);

		// Token: 0x060000AB RID: 171
		void UpdateProviderCourseRegistration(SPProviderCourseRegistrationDTO CourseRegistration);

		// Token: 0x060000AC RID: 172
		void DeleteProviderCourseRegistration(int SPProviderCourseRegistrationId);

		// Token: 0x060000AD RID: 173
		IList<SPProviderDTO> LoadAllProvidersWithAtLeastOneActiveApplication(DateTime StartDate, DateTime EndDate);
	}
}
