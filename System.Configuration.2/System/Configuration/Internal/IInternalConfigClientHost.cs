using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	// Token: 0x020000B1 RID: 177
	[ComVisible(false)]
	public interface IInternalConfigClientHost
	{
		// Token: 0x060006FB RID: 1787
		bool IsExeConfig(string configPath);

		// Token: 0x060006FC RID: 1788
		bool IsRoamingUserConfig(string configPath);

		// Token: 0x060006FD RID: 1789
		bool IsLocalUserConfig(string configPath);

		// Token: 0x060006FE RID: 1790
		string GetExeConfigPath();

		// Token: 0x060006FF RID: 1791
		string GetRoamingUserConfigPath();

		// Token: 0x06000700 RID: 1792
		string GetLocalUserConfigPath();
	}
}
