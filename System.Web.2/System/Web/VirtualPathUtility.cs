using System;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200010A RID: 266
	public static class VirtualPathUtility
	{
		// Token: 0x0600108D RID: 4237 RVA: 0x0002E00C File Offset: 0x0002C20C
		public static bool IsAbsolute(string virtualPath)
		{
			VirtualPath virtualPath2 = VirtualPath.Create(virtualPath);
			return !virtualPath2.IsRelative && virtualPath2.VirtualPathStringIfAvailable != null;
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0002E034 File Offset: 0x0002C234
		public static bool IsAppRelative(string virtualPath)
		{
			VirtualPath virtualPath2 = VirtualPath.Create(virtualPath);
			return virtualPath2.VirtualPathStringIfAvailable == null;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0002E054 File Offset: 0x0002C254
		public static string ToAppRelative(string virtualPath)
		{
			VirtualPath virtualPath2 = VirtualPath.CreateNonRelative(virtualPath);
			return virtualPath2.AppRelativeVirtualPathString;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0002E070 File Offset: 0x0002C270
		public static string ToAppRelative(string virtualPath, string applicationPath)
		{
			VirtualPath virtualPath2 = VirtualPath.CreateNonRelative(virtualPath);
			if (virtualPath2.AppRelativeVirtualPathStringIfAvailable != null)
			{
				return virtualPath2.AppRelativeVirtualPathStringIfAvailable;
			}
			VirtualPath virtualPath3 = VirtualPath.CreateAbsoluteTrailingSlash(applicationPath);
			return UrlPath.MakeVirtualPathAppRelative(virtualPath2.VirtualPathString, virtualPath3.VirtualPathString, true);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0002E0AC File Offset: 0x0002C2AC
		public static string ToAbsolute(string virtualPath)
		{
			VirtualPath virtualPath2 = VirtualPath.CreateNonRelative(virtualPath);
			return virtualPath2.VirtualPathString;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0002E0C8 File Offset: 0x0002C2C8
		public static string ToAbsolute(string virtualPath, string applicationPath)
		{
			VirtualPath virtualPath2 = VirtualPath.CreateNonRelative(virtualPath);
			if (virtualPath2.VirtualPathStringIfAvailable != null)
			{
				return virtualPath2.VirtualPathStringIfAvailable;
			}
			VirtualPath virtualPath3 = VirtualPath.CreateAbsoluteTrailingSlash(applicationPath);
			return UrlPath.MakeVirtualPathAppAbsolute(virtualPath2.AppRelativeVirtualPathString, virtualPath3.VirtualPathString);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0002E104 File Offset: 0x0002C304
		public static string GetFileName(string virtualPath)
		{
			VirtualPath virtualPath2 = VirtualPath.CreateNonRelative(virtualPath);
			return virtualPath2.FileName;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0002E120 File Offset: 0x0002C320
		public static string GetDirectory(string virtualPath)
		{
			VirtualPath virtualPath2 = VirtualPath.CreateNonRelative(virtualPath);
			virtualPath2 = virtualPath2.Parent;
			if (virtualPath2 == null)
			{
				return null;
			}
			return virtualPath2.VirtualPathStringWhicheverAvailable;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0002E14C File Offset: 0x0002C34C
		public static string GetExtension(string virtualPath)
		{
			VirtualPath virtualPath2 = VirtualPath.Create(virtualPath);
			return virtualPath2.Extension;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0002E166 File Offset: 0x0002C366
		public static string AppendTrailingSlash(string virtualPath)
		{
			return UrlPath.AppendSlashToPathIfNeeded(virtualPath);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0002E16E File Offset: 0x0002C36E
		public static string RemoveTrailingSlash(string virtualPath)
		{
			return UrlPath.RemoveSlashFromPathIfNeeded(virtualPath);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0002E178 File Offset: 0x0002C378
		public static string Combine(string basePath, string relativePath)
		{
			VirtualPath virtualPath = VirtualPath.Combine(VirtualPath.CreateNonRelative(basePath), VirtualPath.Create(relativePath));
			return virtualPath.VirtualPathStringWhicheverAvailable;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0002E19D File Offset: 0x0002C39D
		public static string MakeRelative(string fromPath, string toPath)
		{
			return UrlPath.MakeRelative(fromPath, toPath);
		}
	}
}
