using System;

namespace System.Web.Mvc
{
	// Token: 0x020000DA RID: 218
	public abstract class WebViewPage<TModel> : WebViewPage
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		public new AjaxHelper<TModel> Ajax { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000FA01 File Offset: 0x0000DC01
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0000FA09 File Offset: 0x0000DC09
		public new HtmlHelper<TModel> Html { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000FA12 File Offset: 0x0000DC12
		public new TModel Model
		{
			get
			{
				return this.ViewData.Model;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000FA1F File Offset: 0x0000DC1F
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0000FA3A File Offset: 0x0000DC3A
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

		// Token: 0x060005AC RID: 1452 RVA: 0x0000FA43 File Offset: 0x0000DC43
		public override void InitHelpers()
		{
			base.InitHelpers();
			this.Ajax = new AjaxHelper<TModel>(base.ViewContext, this);
			this.Html = new HtmlHelper<TModel>(base.ViewContext, this);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000FA6F File Offset: 0x0000DC6F
		protected override void SetViewData(ViewDataDictionary viewData)
		{
			this._viewData = new ViewDataDictionary<TModel>(viewData);
			base.SetViewData(this._viewData);
		}

		// Token: 0x04000195 RID: 405
		private ViewDataDictionary<TModel> _viewData;
	}
}
