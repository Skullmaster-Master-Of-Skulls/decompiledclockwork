using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000063 RID: 99
	public class CreateNewBooleanColumns : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001798E File Offset: 0x00015B8E
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00017996 File Offset: 0x00015B96
		public OperationContext OpContext { get; set; }

		// Token: 0x06000427 RID: 1063 RVA: 0x0000672B File Offset: 0x0000492B
		public CreateNewBooleanColumns()
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001799F File Offset: 0x00015B9F
		public CreateNewBooleanColumns(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000179B4 File Offset: 0x00015BB4
		private string GetUniqueColumnName(string proposedColName, DataTable t)
		{
			string text = proposedColName;
			int num = 0;
			while (t.Columns.Contains(text) && num < 100000)
			{
				text = proposedColName + "_" + num++.ToString();
			}
			return text;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00017A08 File Offset: 0x00015C08
		private DataTable CreateNewBooleanColumnsFromUniqueValuesInAColumn(DataTable _t, string colName)
		{
			DataTable dataTable = _t.Copy();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = (dataRow[colName] is DBNull) ? null : dataRow[colName].ToString().Trim();
				bool flag = string.IsNullOrEmpty(text);
				if (!flag)
				{
					string key = text.ToLower();
					bool flag2 = dictionary.ContainsKey(key);
					if (!flag2)
					{
						string uniqueColumnName = this.GetUniqueColumnName(text, dataTable);
						dictionary.Add(key, uniqueColumnName);
						dataTable.Columns.Add(uniqueColumnName, typeof(bool));
					}
				}
			}
			foreach (object obj2 in dataTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj2;
				string text2 = (dataRow2[colName] is DBNull) ? null : dataRow2[colName].ToString().Trim();
				bool flag3 = string.IsNullOrEmpty(text2);
				if (!flag3)
				{
					string key2 = text2.ToLower();
					bool flag4 = !dictionary.ContainsKey(key2);
					if (!flag4)
					{
						string columnName = dictionary[key2];
						dataRow2[columnName] = true;
					}
				}
			}
			return dataTable;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00017BB4 File Offset: 0x00015DB4
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable == null;
			if (!flag)
			{
				string defaultFunctionParameter = function.GetDefaultFunctionParameter();
				result.Data.Table = this.CreateNewBooleanColumnsFromUniqueValuesInAColumn(primaryDataTable, defaultFunctionParameter);
			}
		}
	}
}
