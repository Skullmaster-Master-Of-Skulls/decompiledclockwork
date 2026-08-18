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
	// Token: 0x020002D0 RID: 720
	internal sealed class SqlConnectionFactory : DbConnectionFactory
	{
		// Token: 0x060024E4 RID: 9444 RVA: 0x002994F8 File Offset: 0x002988F8
		private SqlConnectionFactory() : base(SqlPerformanceCounters.SingletonInstance)
		{
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060024E5 RID: 9445 RVA: 0x00299518 File Offset: 0x00298918
		public override DbProviderFactory ProviderFactory
		{
			get
			{
				return SqlClientFactory.Instance;
			}
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x00299538 File Offset: 0x00298938
		protected override DbConnectionInternal CreateConnection(DbConnectionOptions options, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)options;
			SqlInternalConnection result;
			if (sqlConnectionString.ContextConnection)
			{
				result = this.GetContextConnection(sqlConnectionString, poolGroupProviderInfo, owningConnection);
			}
			else
			{
				bool redirectedUserInstance = false;
				DbConnectionPoolIdentity identity = null;
				if (sqlConnectionString.IntegratedSecurity)
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
							sqlInternalConnectionTds = new SqlInternalConnectionTds(identity, sqlConnectionString, null, "", null, false);
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
							goto IL_C5;
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
					IL_C5:
					sqlConnectionString = new SqlConnectionString(sqlConnectionString, false, instanceName);
					poolGroupProviderInfo = null;
				}
				result = new SqlInternalConnectionTds(identity, sqlConnectionString, poolGroupProviderInfo, "", (SqlConnection)owningConnection, redirectedUserInstance);
			}
			return result;
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x00299658 File Offset: 0x00298A58
		protected override DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous)
		{
			return new SqlConnectionString(connectionString);
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x00299678 File Offset: 0x00298A78
		internal override DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			DbConnectionPoolProviderInfo result = null;
			if (((SqlConnectionString)connectionOptions).UserInstance)
			{
				result = new SqlConnectionPoolProviderInfo();
			}
			return result;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x002996A8 File Offset: 0x00298AA8
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
				result = new DbConnectionPoolGroupOptions(sqlConnectionString.IntegratedSecurity, sqlConnectionString.MinPoolSize, sqlConnectionString.MaxPoolSize, num, sqlConnectionString.LoadBalanceTimeout, sqlConnectionString.Enlist, false);
			}
			return result;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00299728 File Offset: 0x00298B28
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

		// Token: 0x060024EB RID: 9451 RVA: 0x002997A8 File Offset: 0x00298BA8
		internal override DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return new SqlConnectionPoolGroupProviderInfo((SqlConnectionString)connectionOptions);
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x002997C8 File Offset: 0x00298BC8
		internal static SqlConnectionString FindSqlConnectionOptions(string connectionString)
		{
			SqlConnectionString sqlConnectionString = (SqlConnectionString)SqlConnectionFactory.SingletonInstance.FindConnectionOptions(connectionString);
			if (sqlConnectionString == null)
			{
				sqlConnectionString = new SqlConnectionString(connectionString);
			}
			if (sqlConnectionString.IsEmpty)
			{
				throw ADP.NoConnectionString();
			}
			return sqlConnectionString;
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00299808 File Offset: 0x00298C08
		private SqlInternalConnectionSmi GetContextConnection(SqlConnectionString options, object providerInfo, DbConnection owningConnection)
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

		// Token: 0x060024EE RID: 9454 RVA: 0x00299858 File Offset: 0x00298C58
		internal override DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.PoolGroup;
			}
			return null;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x00299878 File Offset: 0x00298C78
		internal override DbConnectionInternal GetInnerConnection(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.InnerConnection;
			}
			return null;
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00299898 File Offset: 0x00298C98
		protected override int GetObjectId(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection != null)
			{
				return sqlConnection.ObjectID;
			}
			return 0;
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x002998B8 File Offset: 0x00298CB8
		internal override void PermissionDemand(DbConnection outerConnection)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PermissionDemand();
			}
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x002998D8 File Offset: 0x00298CD8
		internal override void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup)
		{
			SqlConnection sqlConnection = outerConnection as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.PoolGroup = poolGroup;
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x002998F8 File Offset: 0x00298CF8
		internal override void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionEvent(to);
			}
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x00299918 File Offset: 0x00298D18
		internal override bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			return sqlConnection != null && sqlConnection.SetInnerConnectionFrom(to, from);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00299948 File Offset: 0x00298D48
		internal override void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to)
		{
			SqlConnection sqlConnection = owningObject as SqlConnection;
			if (sqlConnection != null)
			{
				sqlConnection.SetInnerConnectionTo(to);
			}
		}

		// Token: 0x04001790 RID: 6032
		private const string _metaDataXml = "MetaDataXml";

		// Token: 0x04001791 RID: 6033
		public static readonly SqlConnectionFactory SingletonInstance = new SqlConnectionFactory();
	}
}
