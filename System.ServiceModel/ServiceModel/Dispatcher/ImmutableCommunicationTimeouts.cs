using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000571 RID: 1393
	internal class ImmutableCommunicationTimeouts : IDefaultCommunicationTimeouts
	{
		// Token: 0x06003616 RID: 13846 RVA: 0x000D18A1 File Offset: 0x000CFAA1
		internal ImmutableCommunicationTimeouts() : this(null)
		{
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000D18AC File Offset: 0x000CFAAC
		internal ImmutableCommunicationTimeouts(IDefaultCommunicationTimeouts timeouts)
		{
			if (timeouts == null)
			{
				this.close = ServiceDefaults.CloseTimeout;
				this.open = ServiceDefaults.OpenTimeout;
				this.receive = ServiceDefaults.ReceiveTimeout;
				this.send = ServiceDefaults.SendTimeout;
				return;
			}
			this.close = timeouts.CloseTimeout;
			this.open = timeouts.OpenTimeout;
			this.receive = timeouts.ReceiveTimeout;
			this.send = timeouts.SendTimeout;
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x000D191F File Offset: 0x000CFB1F
		TimeSpan IDefaultCommunicationTimeouts.CloseTimeout
		{
			get
			{
				return this.close;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06003619 RID: 13849 RVA: 0x000D1927 File Offset: 0x000CFB27
		TimeSpan IDefaultCommunicationTimeouts.OpenTimeout
		{
			get
			{
				return this.open;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x000D192F File Offset: 0x000CFB2F
		TimeSpan IDefaultCommunicationTimeouts.ReceiveTimeout
		{
			get
			{
				return this.receive;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x0600361B RID: 13851 RVA: 0x000D1937 File Offset: 0x000CFB37
		TimeSpan IDefaultCommunicationTimeouts.SendTimeout
		{
			get
			{
				return this.send;
			}
		}

		// Token: 0x040028A6 RID: 10406
		private TimeSpan close;

		// Token: 0x040028A7 RID: 10407
		private TimeSpan open;

		// Token: 0x040028A8 RID: 10408
		private TimeSpan receive;

		// Token: 0x040028A9 RID: 10409
		private TimeSpan send;
	}
}
