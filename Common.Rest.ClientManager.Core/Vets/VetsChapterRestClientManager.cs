using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.ClientManager.ICore.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Vets
{
	// Token: 0x02000003 RID: 3
	public class VetsChapterRestClientManager : BearerTokenRestProxy<IVetsChapterClientManager>, IVetsChapterClientManager, IWebService
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000023BD File Offset: 0x000005BD
		public VetsChapterRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023C7 File Offset: 0x000005C7
		public VetsChapterRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023D4 File Offset: 0x000005D4
		public async Task<IList<VetsChapterDTO>> GetChaptersAsync()
		{
			return await this.GetManyAsync<VetsChapterDTO>("vetschapter", true).ConfigureAwait(false);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002419 File Offset: 0x00000619
		public IList<VetsChapterDTO> GetChapters()
		{
			return base.GetMany<VetsChapterDTO>("vetschapter", true);
		}
	}
}
