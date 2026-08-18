using System;

namespace System.Web.Mvc
{
	// Token: 0x020001F3 RID: 499
	public class ViewPage<TModel> : ViewPage
	{
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00027EDA File Offset: 0x000260DA
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x00027EE2 File Offset: 0x000260E2
		public new AjaxHelper<TModel> Ajax { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00027EEB File Offset: 0x000260EB
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x00027EF3 File Offset: 0x000260F3
		public new HtmlHelper<TModel> Html { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00027EFC File Offset: 0x000260FC
		public new TModel Model
		{
			get
			{
				return this.ViewData.Model;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00027F09 File Offset: 0x00026109
		// (set) Token: 0x06000F3A RID: 3898 RVA: 0x00027F24 File Offset: 0x00026124
		public new ViewDataDictionary<TModel> ViewData
		{
			get
			{
				if (this._viewData == null)
				{
					this.SetViewData(new ViewDataDictionary<TModel>());
				}
				return this._viewData;
			}
			set
			{
				this.SetViewData(value);
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00027F2D File Offset: 0x0002612D
		public override void InitHelpers()
		{
			base.InitHelpers();
			this.Ajax = new AjaxHelper<TModel>(base.ViewContext, this);
			this.Html = new HtmlHelper<TModel>(base.ViewContext, this);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00027F59 File Offset: 0x00026159
		protected override void SetViewData(ViewDataDictionary viewData)
		{
			this._viewData = new ViewDataDictionary<TModel>(viewData);
			base.SetViewData(this._viewData);
		}

		// Token: 0x040003F6 RID: 1014
		private ViewDataDictionary<TModel> _viewData;
	}
}
