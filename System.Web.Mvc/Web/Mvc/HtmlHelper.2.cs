using System;
using System.Runtime.CompilerServices;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x02000180 RID: 384
	public class HtmlHelper<TModel> : HtmlHelper
	{
		// Token: 0x06000A82 RID: 2690 RVA: 0x0001CB1F File Offset: 0x0001AD1F
		public HtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer) : this(viewContext, viewDataContainer, RouteTable.Routes)
		{
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001CB2E File Offset: 0x0001AD2E
		public HtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection) : base(viewContext, viewDataContainer, routeCollection)
		{
			this._viewData = new ViewDataDictionary<TModel>(viewDataContainer.ViewData);
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0001CB54 File Offset: 0x0001AD54
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

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0001CB8D File Offset: 0x0001AD8D
		public new ViewDataDictionary<TModel> ViewData
		{
			get
			{
				return this._viewData;
			}
		}

		// Token: 0x040002D1 RID: 721
		private DynamicViewDataDictionary _dynamicViewDataDictionary;

		// Token: 0x040002D2 RID: 722
		private ViewDataDictionary<TModel> _viewData;
	}
}
