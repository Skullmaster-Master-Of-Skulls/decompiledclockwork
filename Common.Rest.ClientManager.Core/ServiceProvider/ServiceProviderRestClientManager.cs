using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.ClientManager.ICore.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.ServiceProvider
{
	// Token: 0x02000019 RID: 25
	public class ServiceProviderRestClientManager : BearerTokenRestProxy<IServiceProviderClientManager>, IServiceProviderClientManager, IWebService
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00004196 File Offset: 0x00002396
		public ServiceProviderRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000041A0 File Offset: 0x000023A0
		public ServiceProviderRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000041AB File Offset: 0x000023AB
		public SPProviderDTO LoadProviderById(int SPProviderId)
		{
			return base.Get<SPProviderDTO>(string.Format("serviceprovider/providerid/{0}", SPProviderId), true);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000041C4 File Offset: 0x000023C4
		public SPProviderDTO LoadProviderByStudent_no(string Student_no)
		{
			return base.Get<SPProviderDTO>(string.Format("serviceprovider/studentno/{0}", Student_no), true);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000041D8 File Offset: 0x000023D8
		public SPProviderDTO LoadProviderByUserName(string UserName)
		{
			return base.Get<SPProviderDTO>(string.Format("serviceprovider/username/{0}", UserName), true);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000041EC File Offset: 0x000023EC
		public SPProviderDTO LoadProviderByExternalId(string ExternalId)
		{
			return base.Get<SPProviderDTO>(string.Format("serviceprovider/externalid/{0}", ExternalId), true);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004200 File Offset: 0x00002400
		public int CreateProvider(SPProviderDTO Provider)
		{
			return base.Post<SPProviderDTO, int>(Provider, "serviceprovider");
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000420E File Offset: 0x0000240E
		public void UpdateProvider(SPProviderDTO Provider)
		{
			base.Post<SPProviderDTO>(Provider, "serviceprovider");
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000421C File Offset: 0x0000241C
		public bool DeleteProvider(int SPProviderId)
		{
			base.Delete(string.Format("serviceprovider/providerid/{0}", SPProviderId));
			return true;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004235 File Offset: 0x00002435
		public int AddProviderCourseRegistration(SPProviderCourseRegistrationDTO CourseRegistration)
		{
			return base.Post<SPProviderCourseRegistrationDTO, int>(CourseRegistration, "serviceprovider/providercourseregistration");
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004243 File Offset: 0x00002443
		public void UpdateProviderCourseRegistration(SPProviderCourseRegistrationDTO CourseRegistration)
		{
			base.Put<SPProviderCourseRegistrationDTO>(CourseRegistration, "serviceprovider/providercourseregistration");
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004251 File Offset: 0x00002451
		public void DeleteProviderCourseRegistration(int SPProviderCourseRegistrationId)
		{
			base.Delete(string.Format("serviceprovider/providercourseregistrationid/{0}", SPProviderCourseRegistrationId));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004269 File Offset: 0x00002469
		public IList<SPProviderDTO> LoadAllProvidersWithAtLeastOneActiveApplication(DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<SPProviderDTO>(string.Format("serviceprovider/withatleastoneactiveapplication/range/{0}/{1}", StartDate, EndDate), true);
		}
	}
}
