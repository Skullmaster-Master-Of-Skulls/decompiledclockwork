using System;
using System.Collections;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x0200032E RID: 814
	internal sealed class ConnectionManagementSectionInternal
	{
		// Token: 0x06001D33 RID: 7475 RVA: 0x0008B03C File Offset: 0x0008923C
		internal ConnectionManagementSectionInternal(ConnectionManagementSection section)
		{
			if (section.ConnectionManagement.Count > 0)
			{
				this.connectionManagement = new Hashtable(section.ConnectionManagement.Count);
				foreach (object obj in section.ConnectionManagement)
				{
					ConnectionManagementElement connectionManagementElement = (ConnectionManagementElement)obj;
					this.connectionManagement[connectionManagementElement.Address] = connectionManagementElement.MaxConnection;
				}
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x0008B0D4 File Offset: 0x000892D4
		internal Hashtable ConnectionManagement
		{
			get
			{
				Hashtable hashtable = this.connectionManagement;
				if (hashtable == null)
				{
					hashtable = new Hashtable();
				}
				return hashtable;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0008B0F4 File Offset: 0x000892F4
		internal static object ClassSyncObject
		{
			get
			{
				if (ConnectionManagementSectionInternal.classSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref ConnectionManagementSectionInternal.classSyncObject, value, null);
				}
				return ConnectionManagementSectionInternal.classSyncObject;
			}
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0008B120 File Offset: 0x00089320
		internal static ConnectionManagementSectionInternal GetSection()
		{
			object obj = ConnectionManagementSectionInternal.ClassSyncObject;
			ConnectionManagementSectionInternal result;
			lock (obj)
			{
				ConnectionManagementSection connectionManagementSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.ConnectionManagementSectionPath) as ConnectionManagementSection;
				if (connectionManagementSection == null)
				{
					result = null;
				}
				else
				{
					result = new ConnectionManagementSectionInternal(connectionManagementSection);
				}
			}
			return result;
		}

		// Token: 0x04001C2C RID: 7212
		private Hashtable connectionManagement;

		// Token: 0x04001C2D RID: 7213
		private static object classSyncObject;
	}
}
