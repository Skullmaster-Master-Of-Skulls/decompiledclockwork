using System;
using System.Web.Configuration;

namespace System.Web.Hosting
{
	// Token: 0x020007E9 RID: 2025
	[Serializable]
	internal class SimpleConfigMapPathFactory : IConfigMapPathFactory
	{
		// Token: 0x06006098 RID: 24728 RVA: 0x0014DC24 File Offset: 0x0014BE24
		IConfigMapPath IConfigMapPathFactory.Create(string virtualPath, string physicalPath)
		{
			WebConfigurationFileMap webConfigurationFileMap = new WebConfigurationFileMap();
			VirtualPath virtualPath2 = VirtualPath.Create(virtualPath);
			webConfigurationFileMap.VirtualDirectories.Add(virtualPath2.VirtualPathStringNoTrailingSlash, new VirtualDirectoryMapping(physicalPath, true));
			webConfigurationFileMap.VirtualDirectories.Add(HttpRuntime.AspClientScriptVirtualPath, new VirtualDirectoryMapping(HttpRuntime.AspClientScriptPhysicalPathInternal, false));
			return new UserMapPath(webConfigurationFileMap);
		}
	}
}
