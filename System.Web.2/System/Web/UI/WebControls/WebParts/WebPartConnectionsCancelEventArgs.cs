using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000586 RID: 1414
	public class WebPartConnectionsCancelEventArgs : CancelEventArgs
	{
		// Token: 0x060047B7 RID: 18359 RVA: 0x000EC617 File Offset: 0x000EA817
		public WebPartConnectionsCancelEventArgs(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
		{
			this._provider = provider;
			this._providerConnectionPoint = providerConnectionPoint;
			this._consumer = consumer;
			this._consumerConnectionPoint = consumerConnectionPoint;
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x000EC63C File Offset: 0x000EA83C
		public WebPartConnectionsCancelEventArgs(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartConnection connection) : this(provider, providerConnectionPoint, consumer, consumerConnectionPoint)
		{
			this._connection = connection;
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x060047B9 RID: 18361 RVA: 0x000EC651 File Offset: 0x000EA851
		public WebPartConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x060047BA RID: 18362 RVA: 0x000EC659 File Offset: 0x000EA859
		public WebPart Consumer
		{
			get
			{
				return this._consumer;
			}
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x060047BB RID: 18363 RVA: 0x000EC661 File Offset: 0x000EA861
		public ConsumerConnectionPoint ConsumerConnectionPoint
		{
			get
			{
				return this._consumerConnectionPoint;
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x060047BC RID: 18364 RVA: 0x000EC669 File Offset: 0x000EA869
		public WebPart Provider
		{
			get
			{
				return this._provider;
			}
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x060047BD RID: 18365 RVA: 0x000EC671 File Offset: 0x000EA871
		public ProviderConnectionPoint ProviderConnectionPoint
		{
			get
			{
				return this._providerConnectionPoint;
			}
		}

		// Token: 0x0400270A RID: 9994
		private WebPart _provider;

		// Token: 0x0400270B RID: 9995
		private ProviderConnectionPoint _providerConnectionPoint;

		// Token: 0x0400270C RID: 9996
		private WebPart _consumer;

		// Token: 0x0400270D RID: 9997
		private ConsumerConnectionPoint _consumerConnectionPoint;

		// Token: 0x0400270E RID: 9998
		private WebPartConnection _connection;
	}
}
