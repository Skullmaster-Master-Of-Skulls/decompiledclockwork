using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000F2 RID: 242
	internal class MessagingDropBoxClientBaseProxy : ClientBase<IMessagingDropBox>, IMessagingDropBox, IService
	{
		// Token: 0x06000948 RID: 2376 RVA: 0x00017CB3 File Offset: 0x00015EB3
		public MessagingDropBoxClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00017CBE File Offset: 0x00015EBE
		public MessagingDropBoxClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00017CCC File Offset: 0x00015ECC
		public int CountIM()
		{
			return base.Channel.CountIM();
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00017CEC File Offset: 0x00015EEC
		public IList<InstantMessage> GetAllIM()
		{
			return base.Channel.GetAllIM();
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00017D0C File Offset: 0x00015F0C
		public InstantMessage GetIM(int imId)
		{
			return base.Channel.GetIM(imId);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00017D2A File Offset: 0x00015F2A
		public void DeleteIM(int imID)
		{
			base.Channel.DeleteIM(imID);
		}
	}
}
