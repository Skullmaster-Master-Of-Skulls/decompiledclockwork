using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004AD RID: 1197
	internal sealed class ReadOnlyHierarchicalDataSource : IHierarchicalDataSource
	{
		// Token: 0x06003BE4 RID: 15332 RVA: 0x000C2873 File Offset: 0x000C0A73
		public ReadOnlyHierarchicalDataSource(object dataSource)
		{
			this._dataSource = dataSource;
		}

		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x06003BE5 RID: 15333 RVA: 0x00006164 File Offset: 0x00004364
		// (remove) Token: 0x06003BE6 RID: 15334 RVA: 0x00006164 File Offset: 0x00004364
		event EventHandler IHierarchicalDataSource.DataSourceChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x000C2884 File Offset: 0x000C0A84
		HierarchicalDataSourceView IHierarchicalDataSource.GetHierarchicalView(string viewPath)
		{
			IHierarchicalDataSource hierarchicalDataSource = this._dataSource as IHierarchicalDataSource;
			if (hierarchicalDataSource != null)
			{
				return hierarchicalDataSource.GetHierarchicalView(viewPath);
			}
			IHierarchicalEnumerable hierarchicalEnumerable = this._dataSource as IHierarchicalEnumerable;
			if (hierarchicalEnumerable != null && viewPath != null && viewPath.Length != 0)
			{
				throw new InvalidOperationException(SR.GetString("ReadOnlyHierarchicalDataSourceView_CantAccessPathInEnumerable"));
			}
			return new ReadOnlyHierarchicalDataSourceView(hierarchicalEnumerable);
		}

		// Token: 0x04002353 RID: 9043
		private object _dataSource;
	}
}
