using System;
using System.Web.Mvc.Properties;
using System.Web.WebPages;

namespace System.Web.Mvc
{
	// Token: 0x020000D7 RID: 215
	public abstract class ViewStartPage : StartPage, IViewStartPageChild
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000F760 File Offset: 0x0000D960
		public HtmlHelper<object> Html
		{
			get
			{
				return this.ViewStartPageChild.Html;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000F76D File Offset: 0x0000D96D
		public UrlHelper Url
		{
			get
			{
				return this.ViewStartPageChild.Url;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000F77A File Offset: 0x0000D97A
		public ViewContext ViewContext
		{
			get
			{
				return this.ViewStartPageChild.ViewContext;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000F788 File Offset: 0x0000D988
		internal IViewStartPageChild ViewStartPageChild
		{
			get
			{
				if (this._viewStartPageChild == null)
				{
					IViewStartPageChild viewStartPageChild = base.ChildPage as IViewStartPageChild;
					if (viewStartPageChild == null)
					{
						throw new InvalidOperationException(MvcResources.ViewStartPage_RequiresMvcRazorView);
					}
					this._viewStartPageChild = viewStartPageChild;
				}
				return this._viewStartPageChild;
			}
		}

		// Token: 0x0400018C RID: 396
		private IViewStartPageChild _viewStartPageChild;
	}
}
