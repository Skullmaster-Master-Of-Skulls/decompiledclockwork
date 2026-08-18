using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.Data.ProviderBase;
using System.IO;
using System.Reflection;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001BA RID: 442
	internal sealed class SqlConnectionFactory : DbConnectionFactory
	{
		// Token: 0x06001AC9 RID: 6857 RVA: 0x000BD6B8 File Offset: 0x000BCAB8
		private SqlConnectionFactory() : base(SqlPerformanceCounters.SingletonInstance)
		{
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x000BD6D0 File Offset: 0x000BCAD0
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000BD6E4 File Offset: 0x000BCAE4
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection)
		{
			return this.CreateConnection(options, poolKey, poolGroupProviderInfo, pool, owningConnection, null);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000BD700 File Offset: 0x000BCB00
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)options;
			SqlConnectionPoolKey sqlConnectionPoolKey = (SqlConnectionPoolKey)poolKey;
			SessionData reconnectSessionData = null;
			SqlConnection sqlConnection = owningConnection as SqlConnection;
			bool applyTransientFaultHandling = sqlConnection != null && sqlConnection._applyTransientFaultHandling;
			SqlConnectionString userConnectionOptions = null;
			if (userOptions != null)
			{
				userConnectionOptions = (SqlConnectionString)userOptions;
			}
			else if (sqlConnection != null)
			{
				userConnectionOptions = (SqlConnectionString)sqlConnection.UserConnectionOptions;
			}
			if (sqlConnection != null)
			{
				reconnectSessionData = sqlConnection._recoverySessionData;
			}
			SqlInternalConnection result;
			if (sqlConnectionString.ContextConnection)
			{
				result = this.GetContextConnection(sqlConnectionString, poolGroupProviderInfo);
			}
			else
			{
				bool redirectedUserInstance = false;
				DbConnectionPoolIdentity identity = null;
				if (sqlConnectionString.IntegratedSecurity || sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated)
				{
					if (pool != null)
					{
						identity = pool.Identity;
					}
					else
					{
						identity = DbConnectionPoolIdentity.GetCurrent();
					}
				}
				if (sqlConnectionString.UserInstance)
				{
					redirectedUserInstance = true;
					string instanceName;
					if (pool == null || (pool != null && pool.Count <= 0))
					{
						SqlInternalConnectionTds sqlInternalConnectionTds = null;
						try
						{
							SqlConnectionString connectionOptions = new SqlConnectionString(sqlConnectionString, sqlConnectionString.DataSource, true, new bool?(false));
							sqlInternalConnectionTds = new SqlInternalConnectionTds(identity, connectionOptions, sqlConnectionPoolKey.Credential, null, "", null, false, null, null, null, null, applyTransientFaultHandling, null);
							instanceName = sqlInternalConnectionTds.InstanceName;
							if (!instanceName.StartsWith("\\\\.\\", StringComparison.Ordinal))
							{
								throw SQL.NonLocalSSEInstance();
							}
							if (pool != null)
							{
								SqlConnectionPoolProviderInfo sqlConnectionPoolProviderInfo = (SqlConnectionPoolProviderInfo)pool.ProviderInfo;
								sqlConnectionPoolProviderInfo.InstanceName = instanceName;
							}
							goto IL_150;
						}
						finally
						{
							if (sqlInternalConnectionTds != null)
							{
								sqlInternalConnectionTds.Dispose();
							}
						}
					}
					SqlConnectionPoolProviderInfo sqlConnectionPoolProviderInfo2 = (SqlConnectionPoolProviderInfo)pool.ProviderInfo;
					instanceName = sqlConnectionPoolProviderInfo2.InstanceName;
					IL_150:
					sqlConnectionString = new SqlConnectionString(sqlConnectionString, instanceName, false, null);
					poolGroupProviderInfo = null;
				}
				result = new SqlInternalConnectionTds(identity, sqlConnectionString, sqlConnectionPoolKey.Credential, poolGroupProviderInfo, "", null, redirectedUserInstance, userConnectionOptions, reconnectSessionData, pool, sqlConnectionPoolKey.AccessToken, applyTransientFaultHandling, null);
			}
			return result;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000BD8BC File Offset: 0x000BCCBC
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new SqlConnectionString(connectionString);
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000BD8D4 File Offset: 0x000BCCD4
		internal override DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			DbConnectionPoolProviderInfo result = null;
			if (((SqlConnectionString)connectionOptions).UserInstance)
			{
				result = new SqlConnectionPoolProviderInfo();
			}
			return result;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000BD8F8 File Offset: 0x000BCCF8
		protected override DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions connectionOptions)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)connectionOptions;
			DbConnectionPoolGroupOptions result = null;
			if (!sqlConnectionString.ContextConnection && sqlConnectionString.Pooling)
			{
				int num = sqlConnectionString.ConnectTimeout;
				if (0 < num && num < 2147483)
				{
					num *= 1000;
				}
				else if (num >= 2147483)
				{
					num = int.MaxValue;
				}
				if (sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryInteractive)
				{
					if (num >= 214748364)
					{
						num = int.MaxValue;
					}
					else
					{
						num *= 10;
					}
					Bid.Trace(string.Format("<sc.SqlConnectionFactory.CreateConnectionPoolGroupOptions>Set connection pool CreateTimeout={0} when AD Interactive is in use.\n", num));
				}
				result = new DbConnectionPoolGroupOptions(sqlConnectionString.IntegratedSecurity || sqlConnectionString.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated, sqlConnectionString.MinPoolSize, sqlConnectionString.MaxPoolSize, num, sqlConnectionString.LoadBalanceTimeout, sqlConnectionString.Enlist);
			}
			return result;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000BD9B8 File Offset: 0x000BCDB8
		protected override DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			cacheMetaDataFactory = false;
			if (internalConnection is SqlInternalConnectionSmi)
			{
				throw SQL.NotAvailableOnContextConnection();
			}
			NameValueCollection nameValueCollection = (NameValueCollection)PrivilegedConfigurationManager.GetSection("system.data.sqlclient");
			Stream stream = null;
			if (nameValueCollection != null)
			{
				string[] values = nameValueCollection.GetValues("MetaDataXml");
				if (values != null)
				{
					stream = ADP.GetXmlStreamFromValues(values, "MetaDataXml");
				}
			}
			if (stream == null)
			{
				stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("System.Data.SqlClient.SqlMetaData.xml");
				cacheMetaDataFactory = true;
			}
			return new SqlMetaDataFactory(stream, internalConnection.ServerVersion, internalConnection.ServerVersion);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000BDA30 File Offset: 0x000BCE30
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new SqlConnectionPoolGroupProviderInfo((SqlConnectionString)connectionOptions);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000BDA48 File Offset: 0x000BCE48
		internal static SqlConnectionString FindSqlConnectionOptions(SqlConnectionPoolKey key)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)SqlConnectionFactory.SingletonInstance.FindConnectionOptions(key);
			if (sqlConnectionString == null)
			{
				sqlConnectionString = new SqlConnectionString(key.ConnectionString);
			}
			if (sqlConnectionString.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			return sqlConnectionString;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x000BDA84 File Offset: 0x000BCE84
		private SqlInternalConnectionSmi GetContextConnection(SqlConnectionString options, object providerInfo)
		{
			SmiContext currentContext = SmiContextFactory.Instance.GetCurrentContext();
			SqlInternalConnectionSmi sqlInternalConnectionSmi = (SqlInternalConnectionSmi)currentContext.GetContextValue(0);
			if (sqlInternalConnectionSmi == null || sqlInternalConnectionSmi.IsConnectionDoomed)
			{
				if (sqlInternalConnectionSmi != null)
				{
					sqlInternalConnectionSmi.Dispose();
				}
				sqlInternalConnectionSmi = new SqlInternalConnectionSmi(options, currentContext);
				currentContext.SetContextValue(0, sqlInternalConnectionSmi);
			}
			sqlInternalConnectionSmi.Activate();
			return sqlInternalConnectionSmi;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x000BDAD4 File Offset: 0x000BCED4
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x000BDAF4 File Offset: 0x000BCEF4
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x000BDB14 File Offset: 0x000BCF14
		protected override int GetObjectId(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.ObjectID;
			}
			return 0;
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000BDB34 File Offset: 0x000BCF34
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PermissionDemand();
			}
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000BDB54 File Offset: 0x000BCF54
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000BDB74 File Offset: 0x000BCF74
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x000BDB94 File Offset: 0x000BCF94
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			return sqlConnection != null && sqlConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000BDBB8 File Offset: 0x000BCFB8
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x04000F91 RID: 3985
		public static readonly SqlConnectionFactory SingletonInstance = new SqlConnectionFactory();

		// Token: 0x04000F92 RID: 3986
		private const string _metaDataXml = "MetaDataXml";
	}
}
