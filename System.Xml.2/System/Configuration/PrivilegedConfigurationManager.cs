using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x020002F6 RID: 758
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x000EC9EC File Offset: 0x000EABEC
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000EC9F3 File Offset: 0x000EABF3
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
