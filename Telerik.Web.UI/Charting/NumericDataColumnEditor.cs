using System;
using System.Data;

namespace Telerik.Charting
{
	// Token: 0x02001758 RID: 5976
	internal class NumericDataColumnEditor : DataColumnEditor
	{
		// Token: 0x0600E8F7 RID: 59639 RVA: 0x003453C4 File Offset: 0x003435C4
		public override void FillListBox(object data, string dataMember)
		{
			this.columnsListing.Items.Add("(None)");
			int num = 0;
			if (data != null)
			{
				DataTableDataHelper dataTableDataHelper = (DataTableDataHelper)DataHelper.CreateDataHelper(data, dataMember, true);
				if (dataTableDataHelper != null)
				{
					foreach (object obj in dataTableDataHelper.DataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						if (dataTableDataHelper.IsColumnNumeric(num++))
						{
							this.columnsListing.Items.Add(dataColumn.ColumnName);
						}
					}
				}
			}
			this.columnsListing.SelectedItem = this.oldValue;
			this.columnsListing.SelectedIndexChanged += base.columnsListing_SelectedIndexChanged;
		}
	}
}
