using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.SqlClient
{
	// Token: 0x020002FC RID: 764
	internal sealed class ServerInfo
	{
		// Token: 0x060027D4 RID: 10196 RVA: 0x002AD3E8 File Offset: 0x002AC7E8
		internal ServerInfo(string userProtocol, string userServerName)
		{
			this._userProtocol = userProtocol;
			this._userServerName = userServerName;
			this.PreRoutingServerName = null;
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x002AD418 File Offset: 0x002AC818
		internal ServerInfo(SqlConnectionString userOptions, RoutingInfo routing, string preRoutingServerName)
		{
			if (routing == null || routing.ServerName == null)
			{
				this.UserServerName = string.Empty;
			}
			else
			{
				this.UserServerName = string.Format(CultureInfo.InvariantCulture, "{0},{1}", new object[]
				{
					routing.ServerName,
					routing.Port
				});
			}
			this.PreRoutingServerName = preRoutingServerName;
			this.UserProtocol = "tcp";
			this.SetDerivedNames(this.UserProtocol, this.UserServerName);
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x002AD4A8 File Offset: 0x002AC8A8
		// (set) Token: 0x060027D7 RID: 10199 RVA: 0x002AD4C8 File Offset: 0x002AC8C8
		internal string ExtendedServerName
		{
			get
			{
				return this._extendedServerName;
			}
			set
			{
				this._extendedServerName = value;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x002AD4E8 File Offset: 0x002AC8E8
		// (set) Token: 0x060027D9 RID: 10201 RVA: 0x002AD508 File Offset: 0x002AC908
		internal string ResolvedServerName
		{
			get
			{
				return this._resolvedServerName;
			}
			set
			{
				this._resolvedServerName = value;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x002AD528 File Offset: 0x002AC928
		// (set) Token: 0x060027DB RID: 10203 RVA: 0x002AD548 File Offset: 0x002AC948
		internal string UserProtocol
		{
			get
			{
				return this._userProtocol;
			}
			set
			{
				this._userProtocol = value;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060027DC RID: 10204 RVA: 0x002AD568 File Offset: 0x002AC968
		// (set) Token: 0x060027DD RID: 10205 RVA: 0x002AD588 File Offset: 0x002AC988
		internal string UserServerName
		{
			get
			{
				return this._userServerName;
			}
			set
			{
				this._userServerName = value;
			}
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x002AD5A8 File Offset: 0x002AC9A8
		internal void SetDerivedNames(string protocol, string serverName)
		{
			if (!ADP.IsEmpty(protocol))
			{
				this.ExtendedServerName = protocol + ":" + serverName;
			}
			else
			{
				this.ExtendedServerName = serverName;
			}
			this.ResolvedServerName = serverName;
		}

		// Token: 0x0400191A RID: 6426
		private string _extendedServerName;

		// Token: 0x0400191B RID: 6427
		private string _resolvedServerName;

		// Token: 0x0400191C RID: 6428
		private string _userProtocol;

		// Token: 0x0400191D RID: 6429
		private string _userServerName;

		// Token: 0x0400191E RID: 6430
		internal readonly string PreRoutingServerName;
	}
}
