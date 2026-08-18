using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200005A RID: 90
	public sealed class OracleClientFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0003224C File Offset: 0x0003124C
		static OracleClientFactory()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00032264 File Offset: 0x00031264
		public override bool CanCreateDataSourceEnumerator
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00032268 File Offset: 0x00031268
		public override DbCommand CreateCommand()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClientFactory::CreateCommand()\n"
				});
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClientFactory::CreateCommand()\n"
				});
			}
			return new OracleCommand();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000322B4 File Offset: 0x000312B4
		public override DbCommandBuilder CreateCommandBuilder()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClientFactory::CreateCommandBuilder()\n"
				});
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClientFactory::CreateCommandBuilder()\n"
				});
			}
			return new OracleCommandBuilder();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00032300 File Offset: 0x00031300
		public override DbConnection CreateConnection()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClientFactory::CreateConnection()\n"
				});
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClientFactory::CreateConnection()\n"
				});
			}
			return new OracleConnection();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0003234C File Offset: 0x0003134C
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClientFactory::CreateConnectionStringBuilder()\n"
				});
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClientFactory::CreateConnectionStringBuilder()\n"
				});
			}
			return new OracleConnectionStringBuilder();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00032398 File Offset: 0x00031398
		public override DbDataAdapter CreateDataAdapter()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClientFactory::CreateDataAdapter()\n"
				});
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClientFactory::CreateDataAdapter()\n"
				});
			}
			return new OracleDataAdapter();
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000323E4 File Offset: 0x000313E4
		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClientFactory::CreateDataSourceEnumerator()\n"
				});
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClientFactory::CreateDataSourceEnumerator()\n"
				});
			}
			return new OracleDataSourceEnumerator();
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00032430 File Offset: 0x00031430
		public override DbParameter CreateParameter()
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClientFactory::CreateParameter()\n"
				});
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClientFactory::CreateParameter()\n"
				});
			}
			return new OracleParameter();
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0003247C File Offset: 0x0003147C
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClientFactory::CreatePermission()\n"
				});
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT)  OracleClientFactory::CreatePermission()\n"
				});
			}
			return new OraclePermission(state);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000324CC File Offset: 0x000314CC
		object IServiceProvider.GetService(Type serviceType)
		{
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (ENTRY) OracleClientFactory::GetService()\n"
				});
			}
			if (serviceType == typeof(DbProviderServices))
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(1U, new string[]
					{
						" (EXIT) OracleClientFactory::GetService()\n"
					});
				}
				return EFOracleProviderServices.Instance;
			}
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(1U, new string[]
				{
					" (EXIT) OracleClientFactory::GetService()\n"
				});
			}
			return null;
		}

		// Token: 0x040002C3 RID: 707
		public static OracleClientFactory Instance = new OracleClientFactory();
	}
}
