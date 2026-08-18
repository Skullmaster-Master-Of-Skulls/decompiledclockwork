using System;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.Common.ClientManager.ICore.CustomForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.CustomForms
{
	// Token: 0x0200005E RID: 94
	public class CustomFieldRestClientManager : BearerTokenRestProxy<ICustomFieldClientManager>, ICustomFieldClientManager, IWebService
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0000AD85 File Offset: 0x00008F85
		public CustomFieldRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000AD8F File Offset: 0x00008F8F
		public CustomFieldRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000AD9C File Offset: 0x00008F9C
		public async Task<Guid> CreateDataInstanceAsync(CustomDataInstanceDTO dataInstance)
		{
			return await this.PostAsync<CustomDataInstanceDTO, Guid>(dataInstance, "customfield").ConfigureAwait(false);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000ADEC File Offset: 0x00008FEC
		public async Task DeleteDataInstanceAsync(Guid dataInstanceId)
		{
			await this.DeleteAsync(string.Format("customfield/datainstanceid/{0}", dataInstanceId)).ConfigureAwait(false);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000AE3C File Offset: 0x0000903C
		public async Task UpdateDataInstanceAsync(CustomDataInstanceDTO dataInstance)
		{
			await this.PutAsync<CustomDataInstanceDTO>(dataInstance, "customfield").ConfigureAwait(false);
		}
	}
}
