using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000308 RID: 776
	[__DynamicallyInvokable]
	public abstract class UdpStatistics
	{
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001B7F RID: 7039
		[__DynamicallyInvokable]
		public abstract long DatagramsReceived { [__DynamicallyInvokable] get; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001B80 RID: 7040
		[__DynamicallyInvokable]
		public abstract long DatagramsSent { [__DynamicallyInvokable] get; }

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001B81 RID: 7041
		[__DynamicallyInvokable]
		public abstract long IncomingDatagramsDiscarded { [__DynamicallyInvokable] get; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001B82 RID: 7042
		[__DynamicallyInvokable]
		public abstract long IncomingDatagramsWithErrors { [__DynamicallyInvokable] get; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001B83 RID: 7043
		[__DynamicallyInvokable]
		public abstract int UdpListeners { [__DynamicallyInvokable] get; }

		// Token: 0x06001B84 RID: 7044 RVA: 0x000822CC File Offset: 0x000804CC
		[__DynamicallyInvokable]
		protected UdpStatistics()
		{
		}
	}
}
