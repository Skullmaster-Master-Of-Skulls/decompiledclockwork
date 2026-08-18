using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200112B RID: 4395
	public class GridInitializePagerItem : GridItemEventInfo
	{
		// Token: 0x0600B365 RID: 45925 RVA: 0x00271680 File Offset: 0x0026F880
		public GridInitializePagerItem(GridPagingManager paging)
		{
			this._pagingManager = paging;
		}

		// Token: 0x170039F6 RID: 14838
		// (get) Token: 0x0600B366 RID: 45926 RVA: 0x0027168F File Offset: 0x0026F88F
		public GridPagingManager PagingManager
		{
			get
			{
				return this._pagingManager;
			}
		}

		// Token: 0x04002F37 RID: 12087
		private GridPagingManager _pagingManager;
	}
}
