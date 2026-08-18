using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc.Properties;
using System.Web.Mvc.Razor;
using System.Web.WebPages;

namespace System.Web.Mvc
{
	// Token: 0x020000BF RID: 191
	public class RazorView : BuildManagerCompiledView
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x0000E014 File Offset: 0x0000C214
		public RazorView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions) : this(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, null)
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000E024 File Offset: 0x0000C224
		public RazorView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions, IViewPageActivator viewPageActivator) : base(controllerContext, viewPath, viewPageActivator)
		{
			this.LayoutPath = (layoutPath ?? string.Empty);
			this.RunViewStartPages = runViewStartPages;
			this.StartPageLookup = new StartPageLookupDelegate(StartPage.GetStartPage);
			this.ViewStartFileExtensions = (viewStartFileExtensions ?? Enumerable.Empty<string>());
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000E076 File Offset: 0x0000C276
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x0000E07E File Offset: 0x0000C27E
		public string LayoutPath { get; private set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000E087 File Offset: 0x0000C287
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0000E08F File Offset: 0x0000C28F
		public bool RunViewStartPages { get; private set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000E098 File Offset: 0x0000C298
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		internal StartPageLookupDelegate StartPageLookup { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000E0A9 File Offset: 0x0000C2A9
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x0000E0B1 File Offset: 0x0000C2B1
		internal IVirtualPathFactory VirtualPathFactory { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000E0BA File Offset: 0x0000C2BA
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0000E0C2 File Offset: 0x0000C2C2
		internal DisplayModeProvider DisplayModeProvider { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000E0CB File Offset: 0x0000C2CB
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0000E0D3 File Offset: 0x0000C2D3
		public IEnumerable<string> ViewStartFileExtensions { get; private set; }

		// Token: 0x0600050E RID: 1294 RVA: 0x0000E0DC File Offset: 0x0000C2DC
		protected override void RenderView(ViewContext viewContext, TextWriter writer, object instance)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			WebViewPage webViewPage = instance as WebViewPage;
			if (webViewPage == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.CshtmlView_WrongViewBase, new object[]
				{
					base.ViewPath
				}));
			}
			webViewPage.OverridenLayoutPath = this.LayoutPath;
			webViewPage.VirtualPath = base.ViewPath;
			webViewPage.ViewContext = viewContext;
			webViewPage.ViewData = viewContext.ViewData;
			webViewPage.InitHelpers();
			if (this.VirtualPathFactory != null)
			{
				webViewPage.VirtualPathFactory = this.VirtualPathFactory;
			}
			if (this.DisplayModeProvider != null)
			{
				webViewPage.DisplayModeProvider = this.DisplayModeProvider;
			}
			WebPageRenderingBase startPage = null;
			if (this.RunViewStartPages)
			{
				startPage = this.StartPageLookup(webViewPage, RazorViewEngine.ViewStartFileName, this.ViewStartFileExtensions);
			}
			webViewPage.ExecutePageHierarchy(new WebPageContext(viewContext.HttpContext, null, null), writer, startPage);
		}
	}
}
