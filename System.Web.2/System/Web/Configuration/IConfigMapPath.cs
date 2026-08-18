using System;

namespace System.Web.Configuration
{
	// Token: 0x02000704 RID: 1796
	public interface IConfigMapPath
	{
		// Token: 0x060056CF RID: 22223
		string GetMachineConfigFilename();

		// Token: 0x060056D0 RID: 22224
		string GetRootWebConfigFilename();

		// Token: 0x060056D1 RID: 22225
		void GetPathConfigFilename(string siteID, string path, out string directory, out string baseName);

		// Token: 0x060056D2 RID: 22226
		void GetDefaultSiteNameAndID(out string siteName, out string siteID);

		// Token: 0x060056D3 RID: 22227
		void ResolveSiteArgument(string siteArgument, out string siteName, out string siteID);

		// Token: 0x060056D4 RID: 22228
		string MapPath(string siteID, string path);

		// Token: 0x060056D5 RID: 22229
		string GetAppPathForPath(string siteID, string path);
	}
}
