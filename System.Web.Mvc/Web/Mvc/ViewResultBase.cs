using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web.Mvc
{
	// Token: 0x020001CB RID: 459
	public abstract class ViewResultBase : ActionResult
	{
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00023B31 File Offset: 0x00021D31
		public object Model
		{
			get
			{
				return this.ViewData.Model;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00023B3E File Offset: 0x00021D3E
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x00023B59 File Offset: 0x00021D59
		public TempDataDictionary TempData
		{
			get
			{
				if (this._tempData == null)
				{
					this._tempData = new TempDataDictionary();
				}
				return this._tempData;
			}
			set
			{
				this._tempData = value;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00023B62 File Offset: 0x00021D62
		// (set) Token: 0x06000D8E RID: 3470 RVA: 0x00023B6A File Offset: 0x00021D6A
		public IView View { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00023B7C File Offset: 0x00021D7C
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

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00023BB5 File Offset: 0x00021DB5
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x00023BD0 File Offset: 0x00021DD0
		public ViewDataDictionary ViewData
		{
			get
			{
				if (this._viewData == null)
				{
					this._viewData = new ViewDataDictionary();
				}
				return this._viewData;
			}
			set
			{
				this._viewData = value;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00023BD9 File Offset: 0x00021DD9
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x00023BEA File Offset: 0x00021DEA
		public ViewEngineCollection ViewEngineCollection
		{
			get
			{
				return this._viewEngineCollection ?? ViewEngines.Engines;
			}
			set
			{
				this._viewEngineCollection = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00023BF3 File Offset: 0x00021DF3
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x00023C04 File Offset: 0x00021E04
		public string ViewName
		{
			get
			{
				return this._viewName ?? string.Empty;
			}
			set
			{
				this._viewName = value;
			}
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00023C10 File Offset: 0x00021E10
		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (string.IsNullOrEmpty(this.ViewName))
			{
				this.ViewName = context.RouteData.GetRequiredString("action");
			}
			ViewEngineResult viewEngineResult = null;
			if (this.View == null)
			{
				viewEngineResult = this.FindView(context);
				this.View = viewEngineResult.View;
			}
			TextWriter output = context.HttpContext.Response.Output;
			ViewContext viewContext = new ViewContext(context, this.View, this.ViewData, this.TempData, output);
			this.View.Render(viewContext, output);
			if (viewEngineResult != null)
			{
				viewEngineResult.ViewEngine.ReleaseView(context, this.View);
			}
		}

		// Token: 0x06000D97 RID: 3479
		protected abstract ViewEngineResult FindView(ControllerContext context);

		// Token: 0x04000380 RID: 896
		private DynamicViewDataDictionary _dynamicViewData;

		// Token: 0x04000381 RID: 897
		private TempDataDictionary _tempData;

		// Token: 0x04000382 RID: 898
		private ViewDataDictionary _viewData;

		// Token: 0x04000383 RID: 899
		private ViewEngineCollection _viewEngineCollection;

		// Token: 0x04000384 RID: 900
		private string _viewName;
	}
}
