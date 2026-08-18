using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Security;

namespace System.Data.SqlClient
{
	// Token: 0x020002D2 RID: 722
	internal sealed class SqlConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x060024F9 RID: 9465 RVA: 0x002999C8 File Offset: 0x00298DC8
		internal SqlConnectionPoolGroupProviderInfo(SqlConnectionString connectionOptions)
		{
			this._failoverPartner = connectionOptions.FailoverPartner;
			if (ADP.IsEmpty(this._failoverPartner))
			{
				this._failoverPartner = null;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060024FA RID: 9466 RVA: 0x00299A08 File Offset: 0x00298E08
		internal string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x00299A28 File Offset: 0x00298E28
		internal bool UseFailoverPartner
		{
			get
			{
				return this._useFailoverPartner;
			}
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x00299A48 File Offset: 0x00298E48
		internal void AliasCheck(string server)
		{
			if (this._alias != server)
			{
				lock (this)
				{
					if (this._alias == null)
					{
						this._alias = server;
					}
					else if (this._alias != server)
					{
						Bid.Trace("<sc.SqlConnectionPoolGroupProviderInfo|INFO> alias change detected. Clearing PoolGroup\n");
						base.PoolGroup.Clear();
						this._alias = server;
					}
				}
			}
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x00299AD8 File Offset: 0x00298ED8
		private PermissionSet CreateFailoverPermission(SqlConnectionString userConnectionOptions, string actualFailoverPartner)
		{
			string keyword;
			if (userConnectionOptions["failover partner"] == null)
			{
				keyword = "data source";
			}
			else
			{
				keyword = "failover partner";
			}
			string connectionString = userConnectionOptions.ExpandKeyword(keyword, actualFailoverPartner);
			return new SqlConnectionString(connectionString).CreatePermissionSet();
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00299B18 File Offset: 0x00298F18
		internal void FailoverCheck(SqlInternalConnection connection, bool actualUseFailoverPartner, SqlConnectionString userConnectionOptions, string actualFailoverPartner)
		{
			if (this.UseFailoverPartner != actualUseFailoverPartner)
			{
				Bid.Trace("<sc.SqlConnectionPoolGroupProviderInfo|INFO> Failover detected. failover partner='%ls'. Clearing PoolGroup\n", actualFailoverPartner);
				base.PoolGroup.Clear();
				this._useFailoverPartner = actualUseFailoverPartner;
			}
			if (!this._useFailoverPartner && this._failoverPartner != actualFailoverPartner)
			{
				PermissionSet failoverPermissionSet = this.CreateFailoverPermission(userConnectionOptions, actualFailoverPartner);
				lock (this)
				{
					if (this._failoverPartner != actualFailoverPartner)
					{
						this._failoverPartner = actualFailoverPartner;
						this._failoverPermissionSet = failoverPermissionSet;
					}
				}
			}
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x00299BB8 File Offset: 0x00298FB8
		internal void FailoverPermissionDemand()
		{
			if (this._useFailoverPartner)
			{
				PermissionSet failoverPermissionSet = this._failoverPermissionSet;
				if (failoverPermissionSet != null)
				{
					failoverPermissionSet.Demand();
				}
			}
		}

		// Token: 0x04001795 RID: 6037
		private string _alias;

		// Token: 0x04001796 RID: 6038
		private PermissionSet _failoverPermissionSet;

		// Token: 0x04001797 RID: 6039
		private string _failoverPartner;

		// Token: 0x04001798 RID: 6040
		private bool _useFailoverPartner;
	}
}
