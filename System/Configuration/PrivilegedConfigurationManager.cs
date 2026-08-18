using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x020007A5 RID: 1957
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06003C3D RID: 15421 RVA: 0x0010162C File Offset: 0x0010062C
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x00101633 File Offset: 0x00100633
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
