using System;
using System.Resources;

namespace System.Security
{
	// Token: 0x02000008 RID: 8
	internal static class SecurityResources
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002472 File Offset: 0x00000672
		internal static string GetResourceString(string key)
		{
			if (SecurityResources.s_resMgr == null)
			{
				SecurityResources.s_resMgr = new ResourceManager("system.security", typeof(SecurityResources).Assembly);
			}
			return SecurityResources.s_resMgr.GetString(key, null);
		}

		// Token: 0x0400005D RID: 93
		private static volatile ResourceManager s_resMgr;
	}
}
