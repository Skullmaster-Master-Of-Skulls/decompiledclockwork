using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000853 RID: 2131
	internal class TcpChannelFactory<TChannel> : ConnectionOrientedTransportChannelFactory<TChannel>, ITcpChannelFactorySettings, IConnectionOrientedTransportChannelFactorySettings, IConnectionOrientedTransportFactorySettings, ITransportFactorySettings, IDefaultCommunicationTimeouts, IConnectionOrientedConnectionSettings
	{
		// Token: 0x06004FF7 RID: 20471 RVA: 0x00125962 File Offset: 0x00123B62
		public TcpChannelFactory(TcpTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context, bindingElement.ConnectionPoolSettings.GroupName, bindingElement.ConnectionPoolSettings.IdleTimeout, bindingElement.ConnectionPoolSettings.MaxOutboundConnectionsPerEndpoint, true)
		{
			this.leaseTimeout = bindingElement.ConnectionPoolSettings.LeaseTimeout;
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x06004FF8 RID: 20472 RVA: 0x0012599F File Offset: 0x00123B9F
		public TimeSpan LeaseTimeout
		{
			get
			{
				return this.leaseTimeout;
			}
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06004FF9 RID: 20473 RVA: 0x001259A7 File Offset: 0x00123BA7
		public override string Scheme
		{
			get
			{
				return Uri.UriSchemeNetTcp;
			}
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x001259B0 File Offset: 0x00123BB0
		internal override IConnectionInitiator GetConnectionInitiator()
		{
			IConnectionInitiator connectionInitiator = new SocketConnectionInitiator(base.ConnectionBufferSize);
			return new BufferedConnectionInitiator(connectionInitiator, base.MaxOutputDelay, base.ConnectionBufferSize);
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x001259DB File Offset: 0x00123BDB
		internal override ConnectionPool GetConnectionPool()
		{
			return TcpChannelFactory<TChannel>.connectionPoolRegistry.Lookup(this);
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x001259E8 File Offset: 0x00123BE8
		internal override void ReleaseConnectionPool(ConnectionPool pool, TimeSpan timeout)
		{
			TcpChannelFactory<TChannel>.connectionPoolRegistry.Release(pool, timeout);
		}

		// Token: 0x04003196 RID: 12694
		private static TcpConnectionPoolRegistry connectionPoolRegistry = new TcpConnectionPoolRegistry();

		// Token: 0x04003197 RID: 12695
		private TimeSpan leaseTimeout;
	}
}
