using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000334 RID: 820
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x0013D854 File Offset: 0x0013CC54
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x0013D868 File Offset: 0x0013CC68
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
