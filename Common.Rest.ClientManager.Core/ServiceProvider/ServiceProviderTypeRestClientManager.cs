using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.ClientManager.ICore.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ServiceProvider;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.ServiceProvider
{
	// Token: 0x0200001A RID: 26
	public class ServiceProviderTypeRestClientManager : BearerTokenRestProxy<IServiceProviderTypeClientManager>, IServiceProviderTypeClientManager, IWebService
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00004288 File Offset: 0x00002488
		public ServiceProviderTypeRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004292 File Offset: 0x00002492
		public ServiceProviderTypeRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000429D File Offset: 0x0000249D
		public SPProviderTypeDTO LoadProviderTypeById(int SPProviderTypeId)
		{
			return base.Get<SPProviderTypeDTO>(string.Format("serviceprovidertype/providertypeid/{0}", SPProviderTypeId), true);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000042B6 File Offset: 0x000024B6
		public IList<SPProviderTypeDTO> LoadProviderTypeByBehaviourCode(eProviderTypeBehaviourCode Code)
		{
			return base.GetMany<SPProviderTypeDTO>(string.Format("serviceprovidertype/behaviourcode/{0}", Code), true);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000042CF File Offset: 0x000024CF
		public IList<SPProviderTypeDTO> LoadAllProviderTypes()
		{
			return base.GetMany<SPProviderTypeDTO>("serviceprovidertype", true);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000042DD File Offset: 0x000024DD
		public int CreateProviderType(SPProviderTypeDTO ProviderType)
		{
			return base.Post<SPProviderTypeDTO, int>(ProviderType, "serviceprovidertype");
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000042EB File Offset: 0x000024EB
		public void UpdateProviderType(SPProviderTypeDTO ProviderType)
		{
			base.Put<SPProviderTypeDTO>(ProviderType, "serviceprovidertype");
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000042F9 File Offset: 0x000024F9
		public void DeleteProviderType(int SPProviderTypeId)
		{
			base.Delete(string.Format("serviceprovidertype/providertypeid/{0}", SPProviderTypeId));
		}
	}
}
