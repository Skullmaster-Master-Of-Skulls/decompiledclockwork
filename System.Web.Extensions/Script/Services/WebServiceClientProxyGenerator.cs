using System;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Web.Script.Services
{
	// Token: 0x020000F7 RID: 247
	internal class WebServiceClientProxyGenerator : ClientProxyGenerator
	{
		// Token: 0x06000D0B RID: 3339 RVA: 0x0002BE1C File Offset: 0x0002A01C
		internal static string GetInlineClientProxyScript(string path, HttpContext context, bool debug)
		{
			WebServiceData webServiceData = WebServiceData.GetWebServiceData(context, path, true, false, true);
			WebServiceClientProxyGenerator webServiceClientProxyGenerator = new WebServiceClientProxyGenerator(path, debug);
			return webServiceClientProxyGenerator.GetClientProxyScript(webServiceData);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002BE44 File Offset: 0x0002A044
		private static DateTime GetAssemblyModifiedTime(Assembly assembly)
		{
			AssemblyName name = assembly.GetName();
			DateTime lastWriteTime = File.GetLastWriteTime(new Uri(name.CodeBase).LocalPath);
			return new DateTime(lastWriteTime.Year, lastWriteTime.Month, lastWriteTime.Day, lastWriteTime.Hour, lastWriteTime.Minute, lastWriteTime.Second);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002BEA0 File Offset: 0x0002A0A0
		[SecuritySafeCritical]
		[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
		internal static string GetClientProxyScript(HttpContext context)
		{
			WebServiceData webServiceData = WebServiceData.GetWebServiceData(context, context.Request.FilePath);
			DateTime assemblyModifiedTime = WebServiceClientProxyGenerator.GetAssemblyModifiedTime(webServiceData.TypeData.Type.Assembly);
			string text = context.Request.Headers["If-Modified-Since"];
			DateTime t;
			if (text != null && DateTime.TryParse(text, out t) && t >= assemblyModifiedTime)
			{
				context.Response.StatusCode = 304;
				return null;
			}
			bool flag = RestHandlerFactory.IsClientProxyDebugRequest(context.Request.PathInfo);
			if (!flag && assemblyModifiedTime.ToUniversalTime() < DateTime.UtcNow)
			{
				HttpCachePolicy cache = context.Response.Cache;
				cache.SetCacheability(HttpCacheability.Public);
				cache.SetLastModified(assemblyModifiedTime);
				cache.SetExpires(assemblyModifiedTime.AddYears(-1));
			}
			WebServiceClientProxyGenerator webServiceClientProxyGenerator = new WebServiceClientProxyGenerator(context.Request.FilePath, flag);
			return webServiceClientProxyGenerator.GetClientProxyScript(webServiceData);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002BF82 File Offset: 0x0002A182
		internal WebServiceClientProxyGenerator(string path, bool debug)
		{
			this._path = path;
			this._debugMode = debug;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002BF98 File Offset: 0x0002A198
		protected override string GetProxyPath()
		{
			return this._path;
		}

		// Token: 0x0400039B RID: 923
		private string _path;
	}
}
