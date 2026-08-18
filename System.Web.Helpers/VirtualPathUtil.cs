using System;
using System.Globalization;
using System.IO;
using System.Web.Helpers.Resources;
using System.Web.WebPages;

namespace System.Web.Helpers
{
	// Token: 0x02000009 RID: 9
	internal static class VirtualPathUtil
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003020 File Offset: 0x00001220
		public static string MapPath(HttpContextBase httpContext, string path)
		{
			if (Path.IsPathRooted(path))
			{
				return path;
			}
			string result;
			try
			{
				result = httpContext.Request.MapPath(VirtualPathUtil.ResolvePath(TemplateStack.GetCurrentTemplate(httpContext), httpContext, path));
			}
			catch (HttpException)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, HelpersResources.PathUtils_IncorrectPath, new object[]
				{
					path
				}), "path");
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000308C File Offset: 0x0000128C
		public static string ResolvePath(string virtualPath)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				return virtualPath;
			}
			if (HttpContext.Current == null)
			{
				return virtualPath;
			}
			HttpContextWrapper httpContext = new HttpContextWrapper(HttpContext.Current);
			return VirtualPathUtil.ResolvePath(TemplateStack.GetCurrentTemplate(httpContext), httpContext, virtualPath);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000030C4 File Offset: 0x000012C4
		internal static string ResolvePath(ITemplateFile templateFile, HttpContextBase httpContext, string virtualPath)
		{
			string basePath;
			if (templateFile != null)
			{
				basePath = templateFile.TemplateInfo.VirtualPath;
			}
			else
			{
				basePath = httpContext.Request.AppRelativeCurrentExecutionFilePath;
			}
			return VirtualPathUtility.Combine(basePath, virtualPath);
		}
	}
}
