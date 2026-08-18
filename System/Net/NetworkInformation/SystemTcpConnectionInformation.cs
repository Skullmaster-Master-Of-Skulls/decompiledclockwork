using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200063B RID: 1595
	internal class SystemTcpConnectionInformation : TcpConnectionInformation
	{
		// Token: 0x06003169 RID: 12649 RVA: 0x000D4448 File Offset: 0x000D3448
		internal SystemTcpConnectionInformation(MibTcpRow row)
		{
			this.state = row.state;
			int port = (int)row.localPort3 << 24 | (int)row.localPort4 << 16 | (int)row.localPort1 << 8 | (int)row.localPort2;
			int port2 = (this.state == TcpState.Listen) ? 0 : ((int)row.remotePort3 << 24 | (int)row.remotePort4 << 16 | (int)row.remotePort1 << 8 | (int)row.remotePort2);
			this.localEndPoint = new IPEndPoint((long)((ulong)row.localAddr), port);
			this.remoteEndPoint = new IPEndPoint((long)((ulong)row.remoteAddr), port2);
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000D44EC File Offset: 0x000D34EC
		public override TcpState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000D44F4 File Offset: 0x000D34F4
		public override IPEndPoint LocalEndPoint
		{
			get
			{
				return this.localEndPoint;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x000D44FC File Offset: 0x000D34FC
		public override IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
		}

		// Token: 0x04002E8C RID: 11916
		private IPEndPoint localEndPoint;

		// Token: 0x04002E8D RID: 11917
		private IPEndPoint remoteEndPoint;

		// Token: 0x04002E8E RID: 11918
		private TcpState state;
	}
}
