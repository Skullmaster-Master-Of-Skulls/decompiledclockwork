using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020003A7 RID: 935
	public class GridLinqDataSource : LinqDataSource
	{
		// Token: 0x060022FB RID: 8955 RVA: 0x000751E7 File Offset: 0x000733E7
		public GridLinqDataSource(string contextTypeName, string tableName, string select, int startRowIndex, int maximumRows, string sortExpression, string filterExpression)
		{
			this._startRowIndex = startRowIndex;
			this._maximumRows = maximumRows;
			this._sortExpression = sortExpression;
			this._filterExpression = filterExpression;
			this.ContextTypeName = contextTypeName;
			base.TableName = tableName;
			base.Select = select;
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x00075224 File Offset: 0x00073424
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public Pair GetData()
		{
			Pair pair = new Pair();
			LinqDataSourceView linqDataSourceView = (LinqDataSourceView)this.GetView("DefaultView");
			DataSourceSelectArguments dataSourceSelectArguments = new DataSourceSelectArguments();
			dataSourceSelectArguments.MaximumRows = this._maximumRows;
			dataSourceSelectArguments.StartRowIndex = this._startRowIndex;
			dataSourceSelectArguments.SortExpression = this._sortExpression;
			dataSourceSelectArguments.RetrieveTotalRowCount = true;
			if (!string.IsNullOrEmpty(this._filterExpression))
			{
				linqDataSourceView.Where = this._filterExpression;
			}
			pair.First = linqDataSourceView.Select(dataSourceSelectArguments);
			pair.Second = dataSourceSelectArguments.TotalRowCount;
			return pair;
		}

		// Token: 0x0400090D RID: 2317
		private int _startRowIndex;

		// Token: 0x0400090E RID: 2318
		private int _maximumRows;

		// Token: 0x0400090F RID: 2319
		private string _sortExpression;

		// Token: 0x04000910 RID: 2320
		private string _filterExpression;
	}
}
