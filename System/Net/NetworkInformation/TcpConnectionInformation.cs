using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063A RID: 1594
	public abstract class TcpConnectionInformation
	{
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06003165 RID: 12645
		public abstract IPEndPoint LocalEndPoint { get; }

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06003166 RID: 12646
		public abstract IPEndPoint RemoteEndPoint { get; }

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06003167 RID: 12647
		public abstract TcpState State { get; }
	}
}
