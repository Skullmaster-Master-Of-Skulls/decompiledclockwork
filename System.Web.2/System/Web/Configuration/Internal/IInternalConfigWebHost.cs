using System;
using System.Runtime.InteropServices;

namespace System.Web.Configuration.Internal
{
	// Token: 0x02000781 RID: 1921
	[ComVisible(false)]
	public interface IInternalConfigWebHost
	{
		// Token: 0x06005C3F RID: 23615
		void GetSiteIDAndVPathFromConfigPath(string configPath, out string siteID, out string vpath);

		// Token: 0x06005C40 RID: 23616
		string GetConfigPathFromSiteIDAndVPath(string siteID, string vpath);
	}
}
