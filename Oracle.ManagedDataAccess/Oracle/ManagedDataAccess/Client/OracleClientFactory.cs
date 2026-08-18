using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;
using OracleInternal.Common;
using OracleInternal.EntityFramework;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000051 RID: 81
	public sealed class OracleClientFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x06000319 RID: 793 RVA: 0x00015C7C File Offset: 0x00013E7C
		object IServiceProvider.GetService(Type serviceType)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			object result;
			try
			{
				if (serviceType == typeof(DbProviderServices))
				{
					if (EFProviderSettings.Instance == null)
					{
						EFProviderSettings.InitializeProviderSettings<EntityFrameworkProviderSettings>();
					}
					result = EFOracleProviderServices.Instance;
				}
				else
				{
					result = null;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00015D14 File Offset: 0x00013F14
		public override bool CanCreateDataSourceEnumerator
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00015D18 File Offset: 0x00013F18
		public override DbCommand CreateCommand()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DbCommand result;
			try
			{
				result = new OracleCommand();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00015D8C File Offset: 0x00013F8C
		public override DbCommandBuilder CreateCommandBuilder()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DbCommandBuilder result;
			try
			{
				result = new OracleCommandBuilder();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00015E00 File Offset: 0x00014000
		public override DbConnection CreateConnection()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DbConnection result;
			try
			{
				result = new OracleConnection();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00015E74 File Offset: 0x00014074
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DbConnectionStringBuilder result;
			try
			{
				result = new OracleConnectionStringBuilder();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00015EE8 File Offset: 0x000140E8
		public override DbDataAdapter CreateDataAdapter()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DbDataAdapter result;
			try
			{
				result = new OracleDataAdapter();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00015F5C File Offset: 0x0001415C
		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DbDataSourceEnumerator result;
			try
			{
				result = new OracleDataSourceEnumerator();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00015FD0 File Offset: 0x000141D0
		public override DbParameter CreateParameter()
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			DbParameter result;
			try
			{
				result = new OracleParameter();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00016044 File Offset: 0x00014244
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			if (ProviderConfig.m_bTraceLevelPublic)
			{
				Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Entry, new string[0]);
			}
			CodeAccessPermission result;
			try
			{
				result = new OraclePermission(state);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Public, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPublic)
				{
					Trace.Write(OracleTraceLevel.Public, OracleTraceTag.Exit, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x04000537 RID: 1335
		public static readonly OracleClientFactory Instance = new OracleClientFactory();
	}
}
