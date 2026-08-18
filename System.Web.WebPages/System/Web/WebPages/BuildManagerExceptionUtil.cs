using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web.WebPages.Resources;
using Microsoft.Web.Infrastructure;

namespace System.Web.WebPages
{
	// Token: 0x02000098 RID: 152
	internal static class BuildManagerExceptionUtil
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0000F670 File Offset: 0x0000D870
		internal static bool IsUnsupportedExtensionError(HttpException e)
		{
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				MethodBase targetSite = ex.TargetSite;
				if (targetSite != null && targetSite.Name == "GetBuildProviderTypeFromExtension" && targetSite.DeclaringType != null && targetSite.DeclaringType.Name == "CompilationUtil")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000F6D8 File Offset: 0x0000D8D8
		internal static void ThrowIfUnsupportedExtension(string virtualPath, HttpException e)
		{
			if (BuildManagerExceptionUtil.IsUnsupportedExtensionError(e))
			{
				string extension = Path.GetExtension(virtualPath);
				throw new HttpException(string.Format(CultureInfo.CurrentCulture, WebPageResources.WebPage_FileNotSupported, new object[]
				{
					extension,
					virtualPath
				}));
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000F71C File Offset: 0x0000D91C
		internal static void ThrowIfCodeDomDefinedExtension(string virtualPath, HttpException e)
		{
			if (e is HttpCompileException)
			{
				string extension = Path.GetExtension(virtualPath);
				if (InfrastructureHelper.IsCodeDomDefinedExtension(extension))
				{
					throw new HttpException(string.Format(CultureInfo.CurrentCulture, WebPageResources.WebPage_FileNotSupported, new object[]
					{
						extension,
						virtualPath
					}));
				}
			}
		}
	}
}
