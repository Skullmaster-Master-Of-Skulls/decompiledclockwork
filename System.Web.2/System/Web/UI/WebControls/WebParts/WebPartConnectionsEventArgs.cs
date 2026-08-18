using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200058D RID: 1421
	public class WebPartConnectionsEventArgs : EventArgs
	{
		// Token: 0x060047DB RID: 18395 RVA: 0x000EC882 File Offset: 0x000EAA82
		public WebPartConnectionsEventArgs(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
		{
			this._provider = provider;
			this._providerConnectionPoint = providerConnectionPoint;
			this._consumer = consumer;
			this._consumerConnectionPoint = consumerConnectionPoint;
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x000EC8A7 File Offset: 0x000EAAA7
		public WebPartConnectionsEventArgs(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartConnection connection) : this(provider, providerConnectionPoint, consumer, consumerConnectionPoint)
		{
			this._connection = connection;
		}

		// Token: 0x17001537 RID: 5431
		// (get) Token: 0x060047DD RID: 18397 RVA: 0x000EC8BC File Offset: 0x000EAABC
		public WebPartConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x060047DE RID: 18398 RVA: 0x000EC8C4 File Offset: 0x000EAAC4
		public WebPart Consumer
		{
			get
			{
				return this._consumer;
			}
		}

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x060047DF RID: 18399 RVA: 0x000EC8CC File Offset: 0x000EAACC
		public ConsumerConnectionPoint ConsumerConnectionPoint
		{
			get
			{
				return this._consumerConnectionPoint;
			}
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x060047E0 RID: 18400 RVA: 0x000EC8D4 File Offset: 0x000EAAD4
		public WebPart Provider
		{
			get
			{
				return this._provider;
			}
		}

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x060047E1 RID: 18401 RVA: 0x000EC8DC File Offset: 0x000EAADC
		public ProviderConnectionPoint ProviderConnectionPoint
		{
			get
			{
				return this._providerConnectionPoint;
			}
		}

		// Token: 0x0400270F RID: 9999
		private WebPart _provider;

		// Token: 0x04002710 RID: 10000
		private ProviderConnectionPoint _providerConnectionPoint;

		// Token: 0x04002711 RID: 10001
		private WebPart _consumer;

		// Token: 0x04002712 RID: 10002
		private ConsumerConnectionPoint _consumerConnectionPoint;

		// Token: 0x04002713 RID: 10003
		private WebPartConnection _connection;
	}
}
