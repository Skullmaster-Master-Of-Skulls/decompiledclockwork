using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002A4 RID: 676
	[__DynamicallyInvokable]
	public abstract class IPInterfaceProperties
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001947 RID: 6471
		[__DynamicallyInvokable]
		public abstract bool IsDnsEnabled { [__DynamicallyInvokable] get; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001948 RID: 6472
		[__DynamicallyInvokable]
		public abstract string DnsSuffix { [__DynamicallyInvokable] get; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001949 RID: 6473
		[__DynamicallyInvokable]
		public abstract bool IsDynamicDnsEnabled { [__DynamicallyInvokable] get; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600194A RID: 6474
		[__DynamicallyInvokable]
		public abstract UnicastIPAddressInformationCollection UnicastAddresses { [__DynamicallyInvokable] get; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600194B RID: 6475
		[__DynamicallyInvokable]
		public abstract MulticastIPAddressInformationCollection MulticastAddresses { [__DynamicallyInvokable] get; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x0600194C RID: 6476
		[__DynamicallyInvokable]
		public abstract IPAddressInformationCollection AnycastAddresses { [__DynamicallyInvokable] get; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600194D RID: 6477
		[__DynamicallyInvokable]
		public abstract IPAddressCollection DnsAddresses { [__DynamicallyInvokable] get; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600194E RID: 6478
		[__DynamicallyInvokable]
		public abstract GatewayIPAddressInformationCollection GatewayAddresses { [__DynamicallyInvokable] get; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600194F RID: 6479
		[__DynamicallyInvokable]
		public abstract IPAddressCollection DhcpServerAddresses { [__DynamicallyInvokable] get; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001950 RID: 6480
		[__DynamicallyInvokable]
		public abstract IPAddressCollection WinsServersAddresses { [__DynamicallyInvokable] get; }

		// Token: 0x06001951 RID: 6481
		[__DynamicallyInvokable]
		public abstract IPv4InterfaceProperties GetIPv4Properties();

		// Token: 0x06001952 RID: 6482
		[__DynamicallyInvokable]
		public abstract IPv6InterfaceProperties GetIPv6Properties();

		// Token: 0x06001953 RID: 6483 RVA: 0x0007E032 File Offset: 0x0007C232
		[__DynamicallyInvokable]
		protected IPInterfaceProperties()
		{
		}
	}
}
