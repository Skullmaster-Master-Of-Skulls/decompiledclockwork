using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200083E RID: 2110
	internal class NamedPipeChannelFactory<TChannel> : ConnectionOrientedTransportChannelFactory<TChannel>, IPipeTransportFactorySettings, IConnectionOrientedTransportChannelFactorySettings, IConnectionOrientedTransportFactorySettings, ITransportFactorySettings, IDefaultCommunicationTimeouts, IConnectionOrientedConnectionSettings
	{
		// Token: 0x06004ED8 RID: 20184 RVA: 0x0011F679 File Offset: 0x0011D879
		public NamedPipeChannelFactory(NamedPipeTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context, NamedPipeChannelFactory<TChannel>.GetConnectionGroupName(bindingElement), bindingElement.ConnectionPoolSettings.IdleTimeout, bindingElement.ConnectionPoolSettings.MaxOutboundConnectionsPerEndpoint, false)
		{
			if (bindingElement.PipeSettings != null)
			{
				this.PipeSettings = bindingElement.PipeSettings.Clone();
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06004ED9 RID: 20185 RVA: 0x0011F6B9 File Offset: 0x0011D8B9
		public override string Scheme
		{
			get
			{
				return Uri.UriSchemeNetPipe;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06004EDA RID: 20186 RVA: 0x0011F6C0 File Offset: 0x0011D8C0
		// (set) Token: 0x06004EDB RID: 20187 RVA: 0x0011F6C8 File Offset: 0x0011D8C8
		public NamedPipeSettings PipeSettings { get; private set; }

		// Token: 0x06004EDC RID: 20188 RVA: 0x0011F6D1 File Offset: 0x0011D8D1
		private static string GetConnectionGroupName(NamedPipeTransportBindingElement bindingElement)
		{
			return bindingElement.ConnectionPoolSettings.GroupName + bindingElement.PipeSettings.ApplicationContainerSettings.GetConnectionGroupSuffix();
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x0011F6F4 File Offset: 0x0011D8F4
		internal override IConnectionInitiator GetConnectionInitiator()
		{
			IConnectionInitiator connectionInitiator = new PipeConnectionInitiator(base.ConnectionBufferSize, this);
			return new BufferedConnectionInitiator(connectionInitiator, base.MaxOutputDelay, base.ConnectionBufferSize);
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x0011F720 File Offset: 0x0011D920
		internal override ConnectionPool GetConnectionPool()
		{
			return NamedPipeChannelFactory<TChannel>.connectionPoolRegistry.Lookup(this);
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x0011F72D File Offset: 0x0011D92D
		internal override void ReleaseConnectionPool(ConnectionPool pool, TimeSpan timeout)
		{
			NamedPipeChannelFactory<TChannel>.connectionPoolRegistry.Release(pool, timeout);
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x0011F73B File Offset: 0x0011D93B
		protected override bool SupportsUpgrade(StreamUpgradeBindingElement upgradeBindingElement)
		{
			return !(upgradeBindingElement is SslStreamSecurityBindingElement);
		}

		// Token: 0x0400310B RID: 12555
		private static NamedPipeConnectionPoolRegistry connectionPoolRegistry = new NamedPipeConnectionPoolRegistry();
	}
}
