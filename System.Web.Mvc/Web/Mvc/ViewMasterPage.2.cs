using System;

namespace System.Web.Mvc
{
	// Token: 0x020001F2 RID: 498
	public class ViewMasterPage<TModel> : ViewMasterPage
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00027E51 File Offset: 0x00026051
		public new AjaxHelper<TModel> Ajax
		{
			get
			{
				if (this._ajaxHelper == null)
				{
					this._ajaxHelper = new AjaxHelper<TModel>(base.ViewContext, base.ViewPage);
				}
				return this._ajaxHelper;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00027E78 File Offset: 0x00026078
		public new HtmlHelper<TModel> Html
		{
			get
			{
				if (this._htmlHelper == null)
				{
					this._htmlHelper = new HtmlHelper<TModel>(base.ViewContext, base.ViewPage);
				}
				return this._htmlHelper;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00027E9F File Offset: 0x0002609F
		public new TModel Model
		{
			get
			{
				return this.ViewData.Model;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00027EAC File Offset: 0x000260AC
		public new ViewDataDictionary<TModel> ViewData
		{
			get
			{
				if (this._viewData == null)
				{
					this._viewData = new ViewDataDictionary<TModel>(base.ViewPage.ViewData);
				}
				return this._viewData;
			}
		}

		// Token: 0x040003F3 RID: 1011
		private AjaxHelper<TModel> _ajaxHelper;

		// Token: 0x040003F4 RID: 1012
		private HtmlHelper<TModel> _htmlHelper;

		// Token: 0x040003F5 RID: 1013
		private ViewDataDictionary<TModel> _viewData;
	}
}
