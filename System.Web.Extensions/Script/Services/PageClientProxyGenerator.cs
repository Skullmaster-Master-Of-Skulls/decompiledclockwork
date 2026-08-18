using System;
using System.Web.UI;

namespace System.Web.Script.Services
{
	// Token: 0x020000EC RID: 236
	internal class PageClientProxyGenerator : ClientProxyGenerator
	{
		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002B234 File Offset: 0x00029434
		internal PageClientProxyGenerator(IPage page, bool debug) : this(VirtualPathUtility.MakeRelative(page.Request.Path, page.Request.FilePath), debug)
		{
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002B258 File Offset: 0x00029458
		internal PageClientProxyGenerator(string path, bool debug)
		{
			this._path = path;
			this._debugMode = debug;
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002B270 File Offset: 0x00029470
		internal static string GetClientProxyScript(HttpContext context, IPage page, bool debug)
		{
			if (context == null || page == null)
			{
				return null;
			}
			WebServiceData webServiceData = WebServiceData.GetWebServiceData(context, page.AppRelativeVirtualPath, false, true);
			if (webServiceData == null)
			{
				return null;
			}
			PageClientProxyGenerator pageClientProxyGenerator = new PageClientProxyGenerator(page, debug);
			return pageClientProxyGenerator.GetClientProxyScript(webServiceData);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002B2A8 File Offset: 0x000294A8
		protected override void GenerateTypeDeclaration(WebServiceData webServiceData, bool genClass)
		{
			if (genClass)
			{
				this._builder.Append("PageMethods.prototype = ");
				return;
			}
			this._builder.Append("var PageMethods = ");
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002B2D0 File Offset: 0x000294D0
		protected override string GetProxyTypeName(WebServiceData data)
		{
			return "PageMethods";
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002B2D7 File Offset: 0x000294D7
		protected override string GetProxyPath()
		{
			return this._path;
		}

		// Token: 0x0400038D RID: 909
		private string _path;
	}
}
