using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001020 RID: 4128
	internal class DecoratingDataSource : IDataSource, IHierarchicalDataSource
	{
		// Token: 0x0600A305 RID: 41733 RVA: 0x00244A70 File Offset: 0x00242C70
		public DecoratingDataSource(IHierarchicalDataSource dataSource)
		{
			this._dataSource = dataSource;
		}

		// Token: 0x1400017F RID: 383
		// (add) Token: 0x0600A306 RID: 41734 RVA: 0x00244A7F File Offset: 0x00242C7F
		// (remove) Token: 0x0600A307 RID: 41735 RVA: 0x00244A81 File Offset: 0x00242C81
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x0600A308 RID: 41736 RVA: 0x00244A83 File Offset: 0x00242C83
		DataSourceView IDataSource.GetView(string viewName)
		{
			return new EmptyDataSourceView(this);
		}

		// Token: 0x0600A309 RID: 41737 RVA: 0x00244A8B File Offset: 0x00242C8B
		ICollection IDataSource.GetViewNames()
		{
			return null;
		}

		// Token: 0x14000180 RID: 384
		// (add) Token: 0x0600A30A RID: 41738 RVA: 0x00244A8E File Offset: 0x00242C8E
		// (remove) Token: 0x0600A30B RID: 41739 RVA: 0x00244A9C File Offset: 0x00242C9C
		event EventHandler IHierarchicalDataSource.DataSourceChanged
		{
			add
			{
				this._dataSource.DataSourceChanged += value;
			}
			remove
			{
				this._dataSource.DataSourceChanged -= value;
			}
		}

		// Token: 0x0600A30C RID: 41740 RVA: 0x00244AAA File Offset: 0x00242CAA
		HierarchicalDataSourceView IHierarchicalDataSource.GetHierarchicalView(string viewPath)
		{
			return this._dataSource.GetHierarchicalView(viewPath);
		}

		// Token: 0x04002D4F RID: 11599
		private IHierarchicalDataSource _dataSource;
	}
}
