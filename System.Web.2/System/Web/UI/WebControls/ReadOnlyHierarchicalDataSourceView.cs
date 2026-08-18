using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004AE RID: 1198
	internal sealed class ReadOnlyHierarchicalDataSourceView : HierarchicalDataSourceView
	{
		// Token: 0x06003BE8 RID: 15336 RVA: 0x000C28D8 File Offset: 0x000C0AD8
		public ReadOnlyHierarchicalDataSourceView(IHierarchicalEnumerable dataSource)
		{
			this._dataSource = dataSource;
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x000C28E7 File Offset: 0x000C0AE7
		public override IHierarchicalEnumerable Select()
		{
			return this._dataSource;
		}

		// Token: 0x04002354 RID: 9044
		private IHierarchicalEnumerable _dataSource;
	}
}
