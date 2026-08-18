using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x020000CD RID: 205
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x00043284 File Offset: 0x00042684
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000432A4 File Offset: 0x000426A4
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
