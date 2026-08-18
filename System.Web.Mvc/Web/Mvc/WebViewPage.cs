using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.Mvc.Properties;
using System.Web.WebPages;

namespace System.Web.Mvc
{
	// Token: 0x020000D9 RID: 217
	public abstract class WebViewPage : WebPageBase, IViewDataContainer, IViewStartPageChild
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000F7CC File Offset: 0x0000D9CC
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0000F7E3 File Offset: 0x0000D9E3
		public override HttpContextBase Context
		{
			get
			{
				return this._context ?? this.ViewContext.HttpContext;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000F7EC File Offset: 0x0000D9EC
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0000F816 File Offset: 0x0000DA16
		public HtmlHelper<object> Html
		{
			get
			{
				if (this._html == null && this.ViewContext != null)
				{
					this._html = new HtmlHelper<object>(this.ViewContext, this);
				}
				return this._html;
			}
			set
			{
				this._html = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000F81F File Offset: 0x0000DA1F
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0000F849 File Offset: 0x0000DA49
		public AjaxHelper<object> Ajax
		{
			get
			{
				if (this._ajax == null && this.ViewContext != null)
				{
					this._ajax = new AjaxHelper<object>(this.ViewContext, this);
				}
				return this._ajax;
			}
			set
			{
				this._ajax = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000F852 File Offset: 0x0000DA52
		public object Model
		{
			get
			{
				return this.ViewData.Model;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000F85F File Offset: 0x0000DA5F
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0000F867 File Offset: 0x0000DA67
		internal string OverridenLayoutPath { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000F870 File Offset: 0x0000DA70
		public TempDataDictionary TempData
		{
			get
			{
				return this.ViewContext.TempData;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000F87D File Offset: 0x0000DA7D
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x0000F885 File Offset: 0x0000DA85
		public UrlHelper Url { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000F898 File Offset: 0x0000DA98
		[Dynamic]
		public dynamic ViewBag
		{
			[return: Dynamic]
			get
			{
				if (this._dynamicViewData == null)
				{
					this._dynamicViewData = new DynamicViewDataDictionary(() => this.ViewData);
				}
				return this._dynamicViewData;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000F8D1 File Offset: 0x0000DAD1
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0000F8D9 File Offset: 0x0000DAD9
		public ViewContext ViewContext { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000F8E2 File Offset: 0x0000DAE2
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0000F8FD File Offset: 0x0000DAFD
		public ViewDataDictionary ViewData
		{
			get
			{
				if (this._viewData == null)
				{
					this.SetViewData(new ViewDataDictionary());
				}
				return this._viewData;
			}
			set
			{
				this.SetViewData(value);
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000F908 File Offset: 0x0000DB08
		protected override void ConfigurePage(WebPageBase parentPage)
		{
			WebViewPage webViewPage = parentPage as WebViewPage;
			if (webViewPage == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.CshtmlView_WrongViewBase, new object[]
				{
					parentPage.VirtualPath
				}));
			}
			this.ViewContext = webViewPage.ViewContext;
			this.ViewData = webViewPage.ViewData;
			this.InitHelpers();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000F964 File Offset: 0x0000DB64
		public override void ExecutePageHierarchy()
		{
			TextWriter writer = this.ViewContext.Writer;
			this.ViewContext.Writer = base.Output;
			base.ExecutePageHierarchy();
			if (!string.IsNullOrEmpty(this.OverridenLayoutPath))
			{
				this.Layout = this.OverridenLayoutPath;
			}
			this.ViewContext.Writer = writer;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000F9B9 File Offset: 0x0000DBB9
		public virtual void InitHelpers()
		{
			this.Html = null;
			this.Ajax = null;
			this.Url = new UrlHelper(this.ViewContext.RequestContext);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000F9DF File Offset: 0x0000DBDF
		protected virtual void SetViewData(ViewDataDictionary viewData)
		{
			this._viewData = viewData;
		}

		// Token: 0x0400018D RID: 397
		private ViewDataDictionary _viewData;

		// Token: 0x0400018E RID: 398
		private DynamicViewDataDictionary _dynamicViewData;

		// Token: 0x0400018F RID: 399
		private HttpContextBase _context;

		// Token: 0x04000190 RID: 400
		private HtmlHelper<object> _html;

		// Token: 0x04000191 RID: 401
		private AjaxHelper<object> _ajax;
	}
}
