using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.ServiceProvider
{
	// Token: 0x02000018 RID: 24
	public class ServiceProviderCourseRegistrationRestClientManager : BearerTokenRestProxy<IServiceProviderCourseRegistrationClientManager>, IServiceProviderCourseRegistrationClientManager, IWebService
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x000040DA File Offset: 0x000022DA
		public ServiceProviderCourseRegistrationRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000040E4 File Offset: 0x000022E4
		public ServiceProviderCourseRegistrationRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000040EF File Offset: 0x000022EF
		public IList<SPProviderCourseRegistrationDTO> LoadCourseRegistrationsByProvider(int SPProviderId, DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<SPProviderCourseRegistrationDTO>(string.Format("serviceprovidercourseregistration/spproviderid/{0}/range/{1}/{2}", SPProviderId, StartDate, EndDate), true);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004114 File Offset: 0x00002314
		public SPProviderCourseRegistrationDTO LoadCourseRegistrationById(int SPProviderCourseRegistrationId)
		{
			return base.Get<SPProviderCourseRegistrationDTO>(string.Format("serviceprovidercourseregistration/spprovidercourseregistrationid/{0}", SPProviderCourseRegistrationId), true);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004130 File Offset: 0x00002330
		public void UpdateCourseRegistrationStatus(int SPProviderCourseRegistrationId, CourseRegistrationStatusDTO NewStatus)
		{
			UpdateCourseRegistrationStatusReq updateCourseRegistrationStatusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCourseRegistrationStatusReq>();
			updateCourseRegistrationStatusReq.SPProviderCourseRegistrationId = SPProviderCourseRegistrationId;
			updateCourseRegistrationStatusReq.NewCourseRegistrationStatus = NewStatus;
			base.Put<UpdateCourseRegistrationStatusReq>(updateCourseRegistrationStatusReq, "serviceprovidercourseregistration/status");
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004162 File Offset: 0x00002362
		public void UpdateCourseRegistration(SPProviderCourseRegistrationDTO ProviderCourseRegistration)
		{
			base.Put<SPProviderCourseRegistrationDTO>(ProviderCourseRegistration, "serviceprovidercourseregistration");
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004170 File Offset: 0x00002370
		public void DeleteCourseRegistration(int SPProviderCourseRegistrationId)
		{
			base.Delete(string.Format("serviceprovidercourseregistration/spprovidercourseregistrationid/{0}", SPProviderCourseRegistrationId));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004188 File Offset: 0x00002388
		public int CreateCourseRegistration(SPProviderCourseRegistrationDTO ProviderCourseRegistration)
		{
			return base.Post<SPProviderCourseRegistrationDTO, int>(ProviderCourseRegistration, "serviceprovidercourseregistration");
		}
	}
}
