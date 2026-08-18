using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007E0 RID: 2016
	internal abstract class ConnectionPool : IdlingCommunicationPool<string, IConnection>
	{
		// Token: 0x06004C4C RID: 19532 RVA: 0x0011688C File Offset: 0x00114A8C
		protected ConnectionPool(IConnectionOrientedTransportChannelFactorySettings settings, TimeSpan leaseTimeout) : base(settings.MaxOutboundConnectionsPerEndpoint, settings.IdleTimeout, leaseTimeout)
		{
			this.connectionBufferSize = settings.ConnectionBufferSize;
			this.maxOutputDelay = settings.MaxOutputDelay;
			this.name = settings.ConnectionPoolGroupName;
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06004C4D RID: 19533 RVA: 0x001168C5 File Offset: 0x00114AC5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x001168CD File Offset: 0x00114ACD
		protected override void AbortItem(IConnection item)
		{
			item.Abort();
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x001168D5 File Offset: 0x00114AD5
		protected override void CloseItem(IConnection item, TimeSpan timeout)
		{
			item.Close(timeout, false);
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x001168DF File Offset: 0x00114ADF
		protected override void CloseItemAsync(IConnection item, TimeSpan timeout)
		{
			item.Close(timeout, true);
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x001168EC File Offset: 0x00114AEC
		public virtual bool IsCompatible(IConnectionOrientedTransportChannelFactorySettings settings)
		{
			return this.name == settings.ConnectionPoolGroupName && this.connectionBufferSize == settings.ConnectionBufferSize && base.MaxIdleConnectionPoolCount == settings.MaxOutboundConnectionsPerEndpoint && base.IdleTimeout == settings.IdleTimeout && this.maxOutputDelay == settings.MaxOutputDelay;
		}

		// Token: 0x04002FA3 RID: 12195
		private int connectionBufferSize;

		// Token: 0x04002FA4 RID: 12196
		private TimeSpan maxOutputDelay;

		// Token: 0x04002FA5 RID: 12197
		private string name;
	}
}
