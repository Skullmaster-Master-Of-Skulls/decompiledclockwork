using System;
using Databases;
using Databases.Exceptions;
using TechnoPro.Common.Public.Entities.InstanceInfo;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x02000185 RID: 389
	public static class ServerInstanceInfoAdapter
	{
		// Token: 0x06000B75 RID: 2933 RVA: 0x000793BC File Offset: 0x000775BC
		public static DatabaseLayer GetDatabaseLayer(this ServerInstanceInfo serverInfo, eDatabaseConnectionStringName role = eDatabaseConnectionStringName.ClockWork)
		{
			switch (role)
			{
			case eDatabaseConnectionStringName.ClockWorkFiles:
			{
				bool flag = serverInfo.ClockWorkFilesDbConnectionInfo == null;
				if (flag)
				{
					throw new DbNotSupportedException("ClockWorkFiles database role is not supported by your system");
				}
				return serverInfo.ClockWorkFilesDbConnectionInfo.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkFiles);
			}
			case eDatabaseConnectionStringName.ClockWorkTracking:
			{
				bool flag2 = serverInfo.ClockWorkTrackingDbConnectionInfo == null;
				if (flag2)
				{
					throw new DbNotSupportedException("ClockWorkTracking database role is not supported by your system");
				}
				return serverInfo.ClockWorkTrackingDbConnectionInfo.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkTracking);
			}
			}
			bool flag3 = serverInfo.ClockWorkServerDbConnectionInfo == null;
			if (flag3)
			{
				throw new DbNotSupportedException("Primary ClockWork database not found in your system");
			}
			return serverInfo.ClockWorkServerDbConnectionInfo.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0007945C File Offset: 0x0007765C
		public static DatabaseLayer GetPatchDatabaseLayer(this ServerInstanceInfo serverInfo, eDatabaseConnectionStringName role)
		{
			switch (role)
			{
			case eDatabaseConnectionStringName.ClockWorkFiles:
			{
				bool flag = serverInfo.ClockWorkFilesDbConnectionInfo == null;
				if (flag)
				{
					throw new DbNotSupportedException("ClockWorkFiles database role is not supported by your system");
				}
				return serverInfo.ClockWorkFilesDbConnectionInfo.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkFiles).ChangeDatabaseLayerCredentials(serverInfo.PatchUsername, serverInfo.PatchPassword);
			}
			case eDatabaseConnectionStringName.ClockWorkTracking:
			{
				bool flag2 = serverInfo.ClockWorkTrackingDbConnectionInfo == null;
				if (flag2)
				{
					throw new DbNotSupportedException("ClockWorkTracking database role is not supported by your system");
				}
				return serverInfo.ClockWorkTrackingDbConnectionInfo.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWorkTracking).ChangeDatabaseLayerCredentials(serverInfo.PatchUsername, serverInfo.PatchPassword);
			}
			}
			bool flag3 = serverInfo.ClockWorkServerDbConnectionInfo == null;
			if (flag3)
			{
				throw new DbNotSupportedException("Primary ClockWork database not found in your system");
			}
			return serverInfo.ClockWorkServerDbConnectionInfo.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork).ChangeDatabaseLayerCredentials(serverInfo.PatchUsername, serverInfo.PatchPassword);
		}
	}
}
