using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.SqlClient
{
	// Token: 0x020001D7 RID: 471
	internal sealed class ServerInfo
	{
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x000D2E30 File Offset: 0x000D2230
		// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x000D2E44 File Offset: 0x000D2244
		internal string ExtendedServerName { get; private set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x000D2E58 File Offset: 0x000D2258
		// (set) Token: 0x06001DE6 RID: 7654 RVA: 0x000D2E6C File Offset: 0x000D226C
		internal string ResolvedServerName { get; private set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x000D2E80 File Offset: 0x000D2280
		// (set) Token: 0x06001DE8 RID: 7656 RVA: 0x000D2E94 File Offset: 0x000D2294
		internal string ResolvedDatabaseName { get; private set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x000D2EA8 File Offset: 0x000D22A8
		// (set) Token: 0x06001DEA RID: 7658 RVA: 0x000D2EBC File Offset: 0x000D22BC
		internal string UserProtocol { get; private set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x000D2ED0 File Offset: 0x000D22D0
		// (set) Token: 0x06001DEC RID: 7660 RVA: 0x000D2EE4 File Offset: 0x000D22E4
		internal string UserServerName
		{
			get
			{
				return this.m_userServerName;
			}
			private set
			{
				this.m_userServerName = value;
			}
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x000D2EF8 File Offset: 0x000D22F8
		internal ServerInfo(SqlConnectionString userOptions) : this(userOptions, userOptions.DataSource)
		{
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000D2F14 File Offset: 0x000D2314
		internal ServerInfo(SqlConnectionString userOptions, string serverName)
		{
			this.UserServerName = (serverName ?? string.Empty);
			this.UserProtocol = userOptions.NetworkLibrary;
			this.ResolvedDatabaseName = userOptions.InitialCatalog;
			this.PreRoutingServerName = null;
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000D2F58 File Offset: 0x000D2358
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
			this.ResolvedDatabaseName = userOptions.InitialCatalog;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x000D2FE8 File Offset: 0x000D23E8
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

		// Token: 0x0400110F RID: 4367
		private string m_userServerName;

		// Token: 0x04001110 RID: 4368
		internal readonly string PreRoutingServerName;
	}
}
