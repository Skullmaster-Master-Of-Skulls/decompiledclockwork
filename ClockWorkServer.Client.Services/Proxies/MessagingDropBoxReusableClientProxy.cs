using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000F1 RID: 241
	public class MessagingDropBoxReusableClientProxy : WCFTokenBasedReusableClientProxy<IMessagingDropBox>, IMessagingDropBox, IService
	{
		// Token: 0x06000940 RID: 2368 RVA: 0x00017BCA File Offset: 0x00015DCA
		public MessagingDropBoxReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00017BD5 File Offset: 0x00015DD5
		public MessagingDropBoxReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00017BE4 File Offset: 0x00015DE4
		public int CountIM()
		{
			return this.WrapServiceMethod<int>(() => base.Proxy.CountIM());
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00017C08 File Offset: 0x00015E08
		public IList<InstantMessage> GetAllIM()
		{
			return this.WrapServiceMethod<IList<InstantMessage>>(() => base.Proxy.GetAllIM());
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00017C2C File Offset: 0x00015E2C
		public InstantMessage GetIM(int imId)
		{
			return this.WrapServiceMethod<InstantMessage>(() => this.Proxy.GetIM(imId));
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00017C64 File Offset: 0x00015E64
		public void DeleteIM(int imID)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteIM(imID);
			});
		}
	}
}
