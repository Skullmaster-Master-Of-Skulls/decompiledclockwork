using System;
using System.Collections;
using System.Data;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003F6 RID: 1014
	internal static class FilteredDataSetHelper
	{
		// Token: 0x060030E8 RID: 12520 RVA: 0x0009F1D0 File Offset: 0x0009D3D0
		public static DataView CreateFilteredDataView(DataTable table, string sortExpression, string filterExpression, IDictionary filterParameters)
		{
			DataView dataView = new DataView(table);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				dataView.Sort = sortExpression;
			}
			if (!string.IsNullOrEmpty(filterExpression))
			{
				bool flag = false;
				object[] array = new object[filterParameters.Count];
				int num = 0;
				foreach (object obj in filterParameters)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value == null)
					{
						flag = true;
						break;
					}
					array[num] = dictionaryEntry.Value;
					num++;
				}
				filterExpression = string.Format(CultureInfo.InvariantCulture, filterExpression, array);
				if (!flag)
				{
					dataView.RowFilter = filterExpression;
				}
			}
			return dataView;
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x0009F288 File Offset: 0x0009D488
		public static DataTable GetDataTable(Control owner, object dataObject)
		{
			DataSet dataSet = dataObject as DataSet;
			if (dataSet == null)
			{
				return dataObject as DataTable;
			}
			if (dataSet.Tables.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("FilteredDataSetHelper_DataSetHasNoTables", new object[]
				{
					owner.ID
				}));
			}
			return dataSet.Tables[0];
		}
	}
}
