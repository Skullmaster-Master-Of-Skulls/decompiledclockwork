using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.ServiceProvider
{
	// Token: 0x02000017 RID: 23
	public class ServiceProviderApplicationRestClientManager : BearerTokenRestProxy<IServiceProviderApplicationClientManager>, IServiceProviderApplicationClientManager, IWebService
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003FA9 File Offset: 0x000021A9
		public ServiceProviderApplicationRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003FB3 File Offset: 0x000021B3
		public ServiceProviderApplicationRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003FBE File Offset: 0x000021BE
		public SPApplicationDTO LoadApplicationById(int SPApplicationId)
		{
			return base.Get<SPApplicationDTO>(string.Format("serviceproviderapplication/spapplicationid/{0}", SPApplicationId), true);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003FD7 File Offset: 0x000021D7
		public SPApplicationDTO LoadApplicationByProviderAndType(int SPProviderId, int SPProviderTypeId)
		{
			return base.Get<SPApplicationDTO>(string.Format("serviceproviderapplication/spproviderid/{0}/spprovidertypeid/{1}", SPProviderId, SPProviderTypeId), true);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003FF6 File Offset: 0x000021F6
		public int CreateApplication(SPApplicationDTO Application)
		{
			return base.Post<SPApplicationDTO, int>(Application, "serviceproviderapplication");
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004004 File Offset: 0x00002204
		public void UpdateApplication(SPApplicationDTO Application)
		{
			base.Put<SPApplicationDTO>(Application, "serviceproviderapplication");
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004012 File Offset: 0x00002212
		public bool DeleteApplication(int SPApplicationId)
		{
			base.Delete(string.Format("serviceproviderapplication/spapplicationid/{0}", SPApplicationId));
			return true;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000402C File Offset: 0x0000222C
		public void UpdateApplicationAvailabilityType(int SPApplicationId, SPApplicationAvailabilityTypeDTO NewAvailabilityType)
		{
			UpdateApplicationAvailabilityTypeReq updateApplicationAvailabilityTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateApplicationAvailabilityTypeReq>();
			updateApplicationAvailabilityTypeReq.SPApplicationId = SPApplicationId;
			updateApplicationAvailabilityTypeReq.ApplicationAvailabilityType = NewAvailabilityType;
			base.Put<UpdateApplicationAvailabilityTypeReq>(updateApplicationAvailabilityTypeReq, "serviceproviderapplication/availabilitytype");
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000405E File Offset: 0x0000225E
		public IList<SPApplicationDTO> LoadApplicationsBySPProviderType(int SPProviderTypeId, DateTime StartDate, DateTime EndDate, bool IncludeInactiveApplications)
		{
			return base.GetMany<SPApplicationDTO>(string.Format("serviceproviderapplication/spprovidertypeid/{0}/range/{1}/{2}?includeinactiveapplications={3}", new object[]
			{
				SPProviderTypeId,
				StartDate,
				EndDate,
				IncludeInactiveApplications
			}), true);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000409C File Offset: 0x0000229C
		public IList<SPApplicationDTO> LoadApplicationsBySPProvider(int SPProviderId, DateTime StartDate, DateTime EndDate, bool IncludeInactiveApplications)
		{
			return base.GetMany<SPApplicationDTO>(string.Format("serviceproviderapplication/spproviderid/{0}/range/{1}/{2}?includeinactiveapplications={3}", new object[]
			{
				SPProviderId,
				StartDate,
				EndDate,
				IncludeInactiveApplications
			}), true);
		}
	}
}
