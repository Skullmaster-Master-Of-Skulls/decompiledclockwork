using System;

namespace System.Web.Mvc
{
	// Token: 0x0200018D RID: 397
	public class ViewUserControl<TModel> : ViewUserControl
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0001E247 File Offset: 0x0001C447
		public new AjaxHelper<TModel> Ajax
		{
			get
			{
				if (this._ajaxHelper == null)
				{
					this._ajaxHelper = new AjaxHelper<TModel>(base.ViewContext, this);
				}
				return this._ajaxHelper;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0001E269 File Offset: 0x0001C469
		public new HtmlHelper<TModel> Html
		{
			get
			{
				if (this._htmlHelper == null)
				{
					this._htmlHelper = new HtmlHelper<TModel>(base.ViewContext, this);
				}
				return this._htmlHelper;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0001E28B File Offset: 0x0001C48B
		public new TModel Model
		{
			get
			{
				return this.ViewData.Model;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0001E298 File Offset: 0x0001C498
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0001E2A6 File Offset: 0x0001C4A6
		public new ViewDataDictionary<TModel> ViewData
		{
			get
			{
				base.EnsureViewData();
				return this._viewData;
			}
			set
			{
				this.SetViewData(value);
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001E2AF File Offset: 0x0001C4AF
		protected override void SetViewData(ViewDataDictionary viewData)
		{
			this._viewData = new ViewDataDictionary<TModel>(viewData);
			base.SetViewData(this._viewData);
		}

		// Token: 0x04000304 RID: 772
		private AjaxHelper<TModel> _ajaxHelper;

		// Token: 0x04000305 RID: 773
		private HtmlHelper<TModel> _htmlHelper;

		// Token: 0x04000306 RID: 774
		private ViewDataDictionary<TModel> _viewData;
	}
}
