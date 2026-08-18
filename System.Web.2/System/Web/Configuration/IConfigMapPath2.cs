using System;

namespace System.Web.Configuration
{
	// Token: 0x02000705 RID: 1797
	internal interface IConfigMapPath2
	{
		// Token: 0x060056D6 RID: 22230
		void GetPathConfigFilename(string siteID, VirtualPath path, out string directory, out string baseName);

		// Token: 0x060056D7 RID: 22231
		string MapPath(string siteID, VirtualPath path);

		// Token: 0x060056D8 RID: 22232
		VirtualPath GetAppPathForPath(string siteID, VirtualPath path);
	}
}
