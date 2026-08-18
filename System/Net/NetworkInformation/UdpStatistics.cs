using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063E RID: 1598
	public abstract class UdpStatistics
	{
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600318C RID: 12684
		public abstract long DatagramsReceived { get; }

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x0600318D RID: 12685
		public abstract long DatagramsSent { get; }

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600318E RID: 12686
		public abstract long IncomingDatagramsDiscarded { get; }

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600318F RID: 12687
		public abstract long IncomingDatagramsWithErrors { get; }

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06003190 RID: 12688
		public abstract int UdpListeners { get; }
	}
}
