using System;

namespace System.Web.Optimization
{
	// Token: 0x0200003B RID: 59
	internal static class UrlUtil
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x0000637E File Offset: 0x0000457E
		internal static string Url(string basePath, string path)
		{
			if (basePath != null)
			{
				path = VirtualPathUtility.Combine(basePath, path);
			}
			path = VirtualPathUtility.ToAbsolute(path);
			return HttpUtility.UrlPathEncode(path);
		}
	}
}
