using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Web.Mvc.Properties;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x020001F1 RID: 497
	[FileLevelControlBuilder(typeof(ViewMasterPageControlBuilder))]
	public class ViewMasterPage : MasterPage
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00027D9B File Offset: 0x00025F9B
		public AjaxHelper<object> Ajax
		{
			get
			{
				return this.ViewPage.Ajax;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00027DA8 File Offset: 0x00025FA8
		public HtmlHelper<object> Html
		{
			get
			{
				return this.ViewPage.Html;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00027DB5 File Offset: 0x00025FB5
		public object Model
		{
			get
			{
				return this.ViewData.Model;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00027DC2 File Offset: 0x00025FC2
		public TempDataDictionary TempData
		{
			get
			{
				return this.ViewPage.TempData;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00027DCF File Offset: 0x00025FCF
		public UrlHelper Url
		{
			get
			{
				return this.ViewPage.Url;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00027DDC File Offset: 0x00025FDC
		[Dynamic]
		public dynamic ViewBag
		{
			[return: Dynamic]
			get
			{
				return this.ViewPage.ViewBag;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00027DE9 File Offset: 0x00025FE9
		public ViewContext ViewContext
		{
			get
			{
				return this.ViewPage.ViewContext;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00027DF6 File Offset: 0x00025FF6
		public ViewDataDictionary ViewData
		{
			get
			{
				return this.ViewPage.ViewData;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00027E04 File Offset: 0x00026004
		internal ViewPage ViewPage
		{
			get
			{
				ViewPage viewPage = this.Page as ViewPage;
				if (viewPage == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ViewMasterPage_RequiresViewPage, new object[0]));
				}
				return viewPage;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00027E3C File Offset: 0x0002603C
		public HtmlTextWriter Writer
		{
			get
			{
				return this.ViewPage.Writer;
			}
		}
	}
}
