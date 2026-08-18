using System;
using System.Runtime.CompilerServices;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x0200017E RID: 382
	public class AjaxHelper<TModel> : AjaxHelper
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x0001C1E2 File Offset: 0x0001A3E2
		public AjaxHelper(ViewContext viewContext, IViewDataContainer viewDataContainer) : this(viewContext, viewDataContainer, RouteTable.Routes)
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0001C1F1 File Offset: 0x0001A3F1
		public AjaxHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection) : base(viewContext, viewDataContainer, routeCollection)
		{
			this._viewData = new ViewDataDictionary<TModel>(viewDataContainer.ViewData);
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0001C218 File Offset: 0x0001A418
		[Dynamic]
		public new dynamic ViewBag
		{
			[return: Dynamic]
			get
			{
				if (this._dynamicViewDataDictionary == null)
				{
					this._dynamicViewDataDictionary = new DynamicViewDataDictionary(() => this.ViewData);
				}
				return this._dynamicViewDataDictionary;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0001C251 File Offset: 0x0001A451
		public new ViewDataDictionary<TModel> ViewData
		{
			get
			{
				return this._viewData;
			}
		}

		// Token: 0x040002C2 RID: 706
		private DynamicViewDataDictionary _dynamicViewDataDictionary;

		// Token: 0x040002C3 RID: 707
		private ViewDataDictionary<TModel> _viewData;
	}
}
