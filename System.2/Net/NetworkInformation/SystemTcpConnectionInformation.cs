using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000301 RID: 769
	internal class SystemTcpConnectionInformation : TcpConnectionInformation
	{
		// Token: 0x06001B4A RID: 6986 RVA: 0x00081DF4 File Offset: 0x0007FFF4
		internal SystemTcpConnectionInformation(MibTcpRow row)
		{
			this.state = row.state;
			int port = (int)row.localPort1 << 8 | (int)row.localPort2;
			int port2 = (this.state == TcpState.Listen) ? 0 : ((int)row.remotePort1 << 8 | (int)row.remotePort2);
			this.localEndPoint = new IPEndPoint((long)((ulong)row.localAddr), port);
			this.remoteEndPoint = new IPEndPoint((long)((ulong)row.remoteAddr), port2);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00081E68 File Offset: 0x00080068
		internal SystemTcpConnectionInformation(MibTcp6RowOwnerPid row)
		{
			this.state = row.state;
			int port = (int)row.localPort1 << 8 | (int)row.localPort2;
			int port2 = (this.state == TcpState.Listen) ? 0 : ((int)row.remotePort1 << 8 | (int)row.remotePort2);
			this.localEndPoint = new IPEndPoint(new IPAddress(row.localAddr, (long)((ulong)row.localScopeId)), port);
			this.remoteEndPoint = new IPEndPoint(new IPAddress(row.remoteAddr, (long)((ulong)row.remoteScopeId)), port2);
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x00081EEF File Offset: 0x000800EF
		public override TcpState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x00081EF7 File Offset: 0x000800F7
		public override IPEndPoint LocalEndPoint
		{
			get
			{
				return this.localEndPoint;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x00081EFF File Offset: 0x000800FF
		public override IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
		}

		// Token: 0x04001AEA RID: 6890
		private IPEndPoint localEndPoint;

		// Token: 0x04001AEB RID: 6891
		private IPEndPoint remoteEndPoint;

		// Token: 0x04001AEC RID: 6892
		private TcpState state;
	}
}
