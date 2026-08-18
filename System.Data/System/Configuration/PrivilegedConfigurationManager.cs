using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x0200038F RID: 911
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x002DCFE8 File Offset: 0x002DC3E8
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x002DD008 File Offset: 0x002DC408
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
