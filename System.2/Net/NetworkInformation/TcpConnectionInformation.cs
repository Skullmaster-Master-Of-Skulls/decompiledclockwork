using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000305 RID: 773
	[__DynamicallyInvokable]
	public abstract class TcpConnectionInformation
	{
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001B6C RID: 7020
		[__DynamicallyInvokable]
		public abstract IPEndPoint LocalEndPoint { [__DynamicallyInvokable] get; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001B6D RID: 7021
		[__DynamicallyInvokable]
		public abstract IPEndPoint RemoteEndPoint { [__DynamicallyInvokable] get; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001B6E RID: 7022
		[__DynamicallyInvokable]
		public abstract TcpState State { [__DynamicallyInvokable] get; }

		// Token: 0x06001B6F RID: 7023 RVA: 0x000822BC File Offset: 0x000804BC
		[__DynamicallyInvokable]
		protected TcpConnectionInformation()
		{
		}
	}
}
