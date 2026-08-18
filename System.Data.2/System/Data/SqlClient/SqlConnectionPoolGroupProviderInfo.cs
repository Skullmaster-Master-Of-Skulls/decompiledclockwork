using System;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Security;

namespace System.Data.SqlClient
{
	// Token: 0x020001BC RID: 444
	internal sealed class SqlConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x06001ADF RID: 6879 RVA: 0x000BDC28 File Offset: 0x000BD028
		internal SqlConnectionPoolGroupProviderInfo(SqlConnectionString connectionOptions)
		{
			this._failoverPartner = connectionOptions.FailoverPartner;
			if (ADP.IsEmpty(this._failoverPartner))
			{
				this._failoverPartner = null;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001AE0 RID: 6880 RVA: 0x000BDC5C File Offset: 0x000BD05C
		internal string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x000BDC70 File Offset: 0x000BD070
		internal bool UseFailoverPartner
		{
			get
			{
				return this._useFailoverPartner;
			}
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x000BDC84 File Offset: 0x000BD084
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

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000BDD10 File Offset: 0x000BD110
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

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000BDD4C File Offset: 0x000BD14C
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

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000BDDF4 File Offset: 0x000BD1F4
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

		// Token: 0x04000F96 RID: 3990
		private string _alias;

		// Token: 0x04000F97 RID: 3991
		private PermissionSet _failoverPermissionSet;

		// Token: 0x04000F98 RID: 3992
		private string _failoverPartner;

		// Token: 0x04000F99 RID: 3993
		private bool _useFailoverPartner;
	}
}
