using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000626 RID: 1574
	internal sealed class ReadOnlyHierarchicalDataSource : IHierarchicalDataSource
	{
		// Token: 0x06004E10 RID: 19984 RVA: 0x0013C6F3 File Offset: 0x0013B6F3
		public ReadOnlyHierarchicalDataSource(object dataSource)
		{
			this._dataSource = dataSource;
		}

		// Token: 0x140000FA RID: 250
		// (add) Token: 0x06004E11 RID: 19985 RVA: 0x0013C702 File Offset: 0x0013B702
		// (remove) Token: 0x06004E12 RID: 19986 RVA: 0x0013C704 File Offset: 0x0013B704
		event EventHandler IHierarchicalDataSource.DataSourceChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x0013C708 File Offset: 0x0013B708
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

		// Token: 0x04002C7A RID: 11386
		private object _dataSource;
	}
}
