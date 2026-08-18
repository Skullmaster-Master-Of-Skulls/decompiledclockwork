using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.ServiceProviderOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.ServiceProviderOriginal
{
	// Token: 0x0200001C RID: 28
	public class ServiceProviderOriginalApplicationCourseRestClientManager : BearerTokenRestProxy<IServiceProviderOriginalApplicationCourseClientManager>, IServiceProviderOriginalApplicationCourseClientManager, IWebService
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x0000458A File Offset: 0x0000278A
		public ServiceProviderOriginalApplicationCourseRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004594 File Offset: 0x00002794
		public ServiceProviderOriginalApplicationCourseRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000459F File Offset: 0x0000279F
		public IList<LookupCourseBaseDTO> GetProviderCourses(int ServiceProviderId, DateTime StartDate, DateTime EndDate, int ServiceProviderType)
		{
			return base.GetMany<LookupCourseBaseDTO>(string.Format("serviceprovideroriginalapplicationcourse/providercourses/serviceproviderid/{0}/range/{1}/{2}/serviceprovidertype/{3}", new object[]
			{
				ServiceProviderId,
				StartDate,
				EndDate,
				ServiceProviderType
			}), true);
		}
	}
}
