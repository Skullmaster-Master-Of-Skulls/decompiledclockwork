using System;
using System.Collections;
using System.Configuration;
using System.Threading;

namespace System.Net.Configuration
{
	// Token: 0x0200064C RID: 1612
	internal sealed class ConnectionManagementSectionInternal
	{
		// Token: 0x060031ED RID: 12781 RVA: 0x000D4FD4 File Offset: 0x000D3FD4
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

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000D506C File Offset: 0x000D406C
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

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x000D508C File Offset: 0x000D408C
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

		// Token: 0x060031F0 RID: 12784 RVA: 0x000D50B8 File Offset: 0x000D40B8
		internal static ConnectionManagementSectionInternal GetSection()
		{
			ConnectionManagementSectionInternal result;
			lock (ConnectionManagementSectionInternal.ClassSyncObject)
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

		// Token: 0x04002EF6 RID: 12022
		private Hashtable connectionManagement;

		// Token: 0x04002EF7 RID: 12023
		private static object classSyncObject;
	}
}
