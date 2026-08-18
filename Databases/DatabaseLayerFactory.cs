using System;
using System.Collections.Generic;
using System.Data.Common;
using ClockWorkLogger;
using Databases.Exceptions;
using EncryptionClassLibrary;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Win32;

namespace Databases
{
	// Token: 0x02000002 RID: 2
	public static class DatabaseLayerFactory
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("Use GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, OpContext?.TenantId) instead.")]
		public static DatabaseLayer ClockWork
		{
			get
			{
				return DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public static DatabaseLayer ClockWorkFiles
		{
			get
			{
				return DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkFiles);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public static DatabaseLayer ClockWorkTracking
		{
			get
			{
				return DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkTracking);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002060 File Offset: 0x00000260
		public static DatabaseLayer ClockWorkArchive
		{
			get
			{
				return DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkTracking);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002068 File Offset: 0x00000268
		public static void Clear()
		{
			DatabaseLayerFactory.DatabaseConnections.Clear();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002078 File Offset: 0x00000278
		public static DatabaseLayer GetPatchDatabaseLayer(string serverVirtualDir, eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			string key = csName.ToString() + "Patch";
			bool flag = DatabaseLayerFactory.DatabaseConnections.ContainsKey(key) && DatabaseLayerFactory.DatabaseConnections[key] != null;
			DatabaseLayer result;
			if (flag)
			{
				result = DatabaseLayerFactory.DatabaseConnections[key];
			}
			else
			{
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName);
				bool flag2 = databaseLayer != null;
				if (!flag2)
				{
					throw new DbNotSupportedException(string.Format("Database Role '{0}' is not supported in your system", csName));
				}
				string text = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("patch_username");
				bool flag3 = string.IsNullOrEmpty(text);
				if (flag3)
				{
					string text2 = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
					{
						"ClockWorkServer Application",
						serverVirtualDir,
						"patch_username"
					});
					bool flag4 = !string.IsNullOrEmpty(text2);
					if (flag4)
					{
						text = DPAPIEncryptionV2.UnProtectDataBase64String(text2, ProtectionScope.LocalMachine);
					}
				}
				string text3 = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("patch_password");
				bool flag5 = string.IsNullOrEmpty(text3);
				if (flag5)
				{
					string text4 = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
					{
						"ClockWorkServer Application",
						serverVirtualDir,
						"patch_password"
					});
					bool flag6 = !string.IsNullOrEmpty(text4);
					if (flag6)
					{
						text3 = DPAPIEncryptionV2.UnProtectDataBase64String(text4, ProtectionScope.LocalMachine);
					}
				}
				bool flag7 = string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text3);
				if (flag7)
				{
					string text5 = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
					{
						"ClockWorkServer Application",
						serverVirtualDir,
						"DbPatch_cs"
					});
					bool flag8 = !string.IsNullOrEmpty(text5);
					if (flag8)
					{
						DbConnectionStringBuilder dbConnectionStringBuilder = DbProviderFactories.GetFactory(ProviderNames.SqlClient).CreateConnectionStringBuilder();
						dbConnectionStringBuilder.ConnectionString = DPAPIEncryptionV2.UnProtectDataBase64String(text5, ProtectionScope.LocalMachine);
						object obj = dbConnectionStringBuilder["User ID"];
						text = ((obj != null) ? obj.ToString() : null);
						object obj2 = dbConnectionStringBuilder["Password"];
						text3 = ((obj2 != null) ? obj2.ToString() : null);
					}
				}
				result = (DatabaseLayerFactory.DatabaseConnections[key] = databaseLayer.ChangeDatabaseLayerCredentials(text, text3));
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002288 File Offset: 0x00000488
		public static DatabaseLayer GetDatabaseLayer(string tenantId)
		{
			bool flag = DatabaseLayerFactory.DatabaseConnections.ContainsKey(tenantId) && DatabaseLayerFactory.DatabaseConnections[tenantId] != null;
			if (flag)
			{
				return DatabaseLayerFactory.DatabaseConnections[tenantId];
			}
			throw new DbNotSupportedException("There is not database connection setup for '" + tenantId + "'");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022E0 File Offset: 0x000004E0
		public static DatabaseLayer GetDatabaseLayer(eDatabaseConnectionStringName csName, string tenantId)
		{
			return (string.IsNullOrEmpty(tenantId) || csName != eDatabaseConnectionStringName.ClockWork) ? DatabaseLayerFactory.GetDatabaseLayer(csName) : DatabaseLayerFactory.GetDatabaseLayer(tenantId);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000230C File Offset: 0x0000050C
		public static DatabaseLayer GetDatabaseLayer(eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork)
		{
			bool flag = DatabaseLayerFactory.DatabaseConnections.ContainsKey(csName.ToString()) && DatabaseLayerFactory.DatabaseConnections[csName.ToString()] != null;
			if (!flag)
			{
				DatabaseLayer databaseLayer = new DatabaseLayer(csName);
				try
				{
					bool flag2 = databaseLayer.TestDatabaseConnectivity(null);
					if (flag2)
					{
						return DatabaseLayerFactory.DatabaseConnections[csName.ToString()] = databaseLayer;
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("DatabaseLayerFactory::GetDatabaseLayer: csName={0}, {1}", csName, ex.ToString()), ex);
					throw new DbNotSupportedException(string.Format("Database Role '{0}' is not supported in your system or you do not have enough privileges to connect to it", csName), ex);
				}
				throw new DbNotSupportedException(string.Format("Database Role '{0}' is not supported in your system or you do not have enough privileges to connect to it", csName));
			}
			return DatabaseLayerFactory.DatabaseConnections[csName.ToString()];
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002410 File Offset: 0x00000610
		public static void SetDatabaseLayer(eDatabaseConnectionStringName csName, DatabaseLayer db)
		{
			bool flag = db != null;
			if (flag)
			{
				DatabaseLayerFactory.DatabaseConnections[csName.ToString()] = db;
			}
			else
			{
				DatabaseLayerFactory.DatabaseConnections.Remove(csName.ToString());
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002459 File Offset: 0x00000659
		public static void SetDatabaseLayer(string tenantId, string providerName, string connectionString, string dbPwd)
		{
			DatabaseLayerFactory.DatabaseConnections[tenantId] = new DatabaseLayer(providerName, connectionString, dbPwd);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002470 File Offset: 0x00000670
		public static void SetPatchDatabaseLayer(eDatabaseConnectionStringName csName, DatabaseLayer db)
		{
			string key = csName.ToString() + "Patch";
			bool flag = db != null;
			if (flag)
			{
				DatabaseLayerFactory.DatabaseConnections[key] = db;
			}
			else
			{
				DatabaseLayerFactory.DatabaseConnections.Remove(key);
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly IDictionary<string, DatabaseLayer> DatabaseConnections = new Dictionary<string, DatabaseLayer>();
	}
}
