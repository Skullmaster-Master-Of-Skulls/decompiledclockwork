using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Security
{
	// Token: 0x0200027C RID: 636
	public class InfocardInteractiveChannelInitializer : IInteractiveChannelInitializer
	{
		// Token: 0x06001225 RID: 4645 RVA: 0x000432E5 File Offset: 0x000414E5
		public InfocardInteractiveChannelInitializer(ClientCredentials credentials, Binding binding)
		{
			this.credentials = credentials;
			this.binding = binding;
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x000432FB File Offset: 0x000414FB
		public Binding Binding
		{
			get
			{
				return this.binding;
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00043303 File Offset: 0x00041503
		public virtual IAsyncResult BeginDisplayInitializationUI(IClientChannel channel, AsyncCallback callback, object state)
		{
			return new GetTokenUIAsyncResult(this.binding, channel, this.credentials, callback, state);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00043319 File Offset: 0x00041519
		public virtual void EndDisplayInitializationUI(IAsyncResult result)
		{
			GetTokenUIAsyncResult.End(result);
		}

		// Token: 0x040019D3 RID: 6611
		private ClientCredentials credentials;

		// Token: 0x040019D4 RID: 6612
		private Binding binding;
	}
}
