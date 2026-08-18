using System;
using System.Globalization;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000859 RID: 2137
	internal class TcpConnectionPoolRegistry : ConnectionPoolRegistry
	{
		// Token: 0x06005026 RID: 20518 RVA: 0x00126110 File Offset: 0x00124310
		protected override ConnectionPool CreatePool(IConnectionOrientedTransportChannelFactorySettings settings)
		{
			ITcpChannelFactorySettings settings2 = (ITcpChannelFactorySettings)settings;
			return new TcpConnectionPoolRegistry.TcpConnectionPool(settings2);
		}

		// Token: 0x02000D3D RID: 3389
		private class TcpConnectionPool : ConnectionPool
		{
			// Token: 0x06007C4C RID: 31820 RVA: 0x001D0AAA File Offset: 0x001CECAA
			public TcpConnectionPool(ITcpChannelFactorySettings settings) : base(settings, settings.LeaseTimeout)
			{
			}

			// Token: 0x06007C4D RID: 31821 RVA: 0x001D0ABC File Offset: 0x001CECBC
			protected override string GetPoolKey(EndpointAddress address, Uri via)
			{
				int num = via.Port;
				if (num == -1)
				{
					num = 808;
				}
				string text = via.DnsSafeHost.ToUpperInvariant();
				return string.Format(CultureInfo.InvariantCulture, "[{0}, {1}]", new object[]
				{
					text,
					num
				});
			}

			// Token: 0x06007C4E RID: 31822 RVA: 0x001D0B08 File Offset: 0x001CED08
			public override bool IsCompatible(IConnectionOrientedTransportChannelFactorySettings settings)
			{
				ITcpChannelFactorySettings tcpChannelFactorySettings = (ITcpChannelFactorySettings)settings;
				return base.LeaseTimeout == tcpChannelFactorySettings.LeaseTimeout && base.IsCompatible(settings);
			}
		}
	}
}
