using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000324 RID: 804
	internal class RoutingInfo
	{
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06002A56 RID: 10838 RVA: 0x002BE638 File Offset: 0x002BDA38
		internal byte Protocol
		{
			get
			{
				return this._protocol;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x002BE658 File Offset: 0x002BDA58
		internal ushort Port
		{
			get
			{
				return this._port;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x002BE678 File Offset: 0x002BDA78
		internal string ServerName
		{
			get
			{
				return this._serverName;
			}
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x002BE698 File Offset: 0x002BDA98
		internal RoutingInfo(byte protocol, ushort port, string servername)
		{
			this._protocol = protocol;
			this._port = port;
			this._serverName = servername;
		}

		// Token: 0x04001B9A RID: 7066
		private byte _protocol;

		// Token: 0x04001B9B RID: 7067
		private ushort _port;

		// Token: 0x04001B9C RID: 7068
		private string _serverName;
	}
}
