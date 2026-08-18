using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using Telerik.Web.UI.PivotGrid.Core.DataSouceView;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C9E RID: 3230
	internal static class GenericDataSourceView
	{
		// Token: 0x06007963 RID: 31075 RVA: 0x001BE77C File Offset: 0x001BC97C
		public static IDataSourceView GetSourceView(object dataSource)
		{
			IDataSourceView dataSourceView = GenericDataSourceView.InitializeConcreteWrapper(dataSource);
			if (dataSourceView == null)
			{
				throw new InvalidOperationException("Specified data source not supported");
			}
			return dataSourceView;
		}

		// Token: 0x06007964 RID: 31076 RVA: 0x001BE7A0 File Offset: 0x001BC9A0
		private static IDataSourceView InitializeConcreteWrapper(object dataSource)
		{
			IDataSourceView dataSourceView = GenericDataSourceView.TryGetDataSourceViewForDataSet(dataSource);
			if (dataSourceView == null)
			{
				dataSourceView = GenericDataSourceView.TryGetDataSourceViewForDataTable(dataSource);
			}
			if (dataSourceView == null)
			{
				dataSourceView = GenericDataSourceView.TryGetDataSourceViewForIListSource(dataSource);
			}
			if (dataSourceView == null)
			{
				dataSourceView = GenericDataSourceView.TryGetDataSourceViewForEnumerable(dataSource);
			}
			return dataSourceView;
		}

		// Token: 0x06007965 RID: 31077 RVA: 0x001BE7D8 File Offset: 0x001BC9D8
		private static IDataSourceView TryGetDataSourceViewForDataSet(object dataSource)
		{
			DataSet dataSet = dataSource as DataSet;
			if (dataSet != null)
			{
				DataTable dataTable = dataSet.Tables[0];
				return new DataTableDataSourceView(dataTable);
			}
			return null;
		}

		// Token: 0x06007966 RID: 31078 RVA: 0x001BE804 File Offset: 0x001BCA04
		private static IDataSourceView TryGetDataSourceViewForDataTable(object dataSource)
		{
			DataTable dataTable = dataSource as DataTable;
			if (dataTable != null)
			{
				return new DataTableDataSourceView(dataTable);
			}
			return null;
		}

		// Token: 0x06007967 RID: 31079 RVA: 0x001BE824 File Offset: 0x001BCA24
		private static IDataSourceView TryGetDataSourceViewForIListSource(object dataSource)
		{
			IListSource listSource = dataSource as IListSource;
			if (listSource != null)
			{
				return new EnumerableDataSourceView(listSource.GetList());
			}
			return null;
		}

		// Token: 0x06007968 RID: 31080 RVA: 0x001BE848 File Offset: 0x001BCA48
		private static IDataSourceView TryGetDataSourceViewForEnumerable(object dataSource)
		{
			IEnumerable enumerable = dataSource as IEnumerable;
			if (enumerable != null)
			{
				return new EnumerableDataSourceView(enumerable);
			}
			return null;
		}
	}
}
