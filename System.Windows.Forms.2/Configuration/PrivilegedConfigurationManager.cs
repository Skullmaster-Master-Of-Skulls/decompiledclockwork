using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x020000F7 RID: 247
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000C114 File Offset: 0x0000A314
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000C11B File Offset: 0x0000A31B
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
