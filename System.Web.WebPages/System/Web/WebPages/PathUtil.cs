using System;
using System.IO;

namespace System.Web.WebPages
{
	// Token: 0x02000080 RID: 128
	internal static class PathUtil
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0000C954 File Offset: 0x0000AB54
		internal static string GetExtension(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			int num = path.Length;
			while (--num >= 0)
			{
				char c = path[num];
				if (c == '.')
				{
					if (num != path.Length - 1)
					{
						return path.Substring(num);
					}
					break;
				}
				else if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar)
				{
					break;
				}
			}
			return string.Empty;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		internal static bool IsWithinAppRoot(string appDomainAppVirtualPath, string virtualPath)
		{
			if (appDomainAppVirtualPath == null)
			{
				return true;
			}
			string virtualPath2 = virtualPath;
			if (!VirtualPathUtility.IsAbsolute(virtualPath2))
			{
				virtualPath2 = VirtualPathUtility.ToAbsolute(virtualPath2);
			}
			return VirtualPathUtility.ToAppRelative(virtualPath2, appDomainAppVirtualPath) != null;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000C9E4 File Offset: 0x0000ABE4
		internal static bool IsSimpleName(string path)
		{
			return !VirtualPathUtility.IsAbsolute(path) && !VirtualPathUtility.IsAppRelative(path) && !path.StartsWith(".", StringComparison.OrdinalIgnoreCase);
		}
	}
}
