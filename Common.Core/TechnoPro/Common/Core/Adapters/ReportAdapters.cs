using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x02000177 RID: 375
	public static class ReportAdapters
	{
		// Token: 0x06001049 RID: 4169 RVA: 0x00078174 File Offset: 0x00076374
		public static DataTable GetPrimaryDataTable(this RunReportResult RunReportResult)
		{
			return RunReportResult.GetPrimaryDataView();
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00078190 File Offset: 0x00076390
		public static DataTable GetPrimaryDataView(this RunReportResult RunReportResult)
		{
			RunFunctionData primaryData = RunReportResult.PrimaryData;
			return (primaryData != null) ? primaryData.Table : null;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x000781B8 File Offset: 0x000763B8
		public static string GetDefaultFunctionParameter(this ReportFunction Function)
		{
			ReportParameter reportParameter = Function.FunctionParameters.FirstOrDefault((ReportParameter f) => f.Name.Equals("default", StringComparison.OrdinalIgnoreCase));
			string result;
			if (reportParameter != null)
			{
				object value = reportParameter.Value;
				result = (((value != null) ? value.ToString() : null) ?? "");
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0007821C File Offset: 0x0007641C
		public static DataTable ExtractPrimaryTable(this RunReportResult currentReportResult)
		{
			DataTable result;
			if (currentReportResult == null)
			{
				result = null;
			}
			else
			{
				RunFunctionData primaryData = currentReportResult.PrimaryData;
				result = ((primaryData != null) ? primaryData.Table : null);
			}
			return result;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00078248 File Offset: 0x00076448
		public static int ExtractParameterValueInt(this RunReportResult currentReportResult, int defaultReturnValue, params string[] possibleParameterNames)
		{
			object obj = currentReportResult.ExtractParameterValue(possibleParameterNames);
			bool flag = obj == null;
			int result;
			if (flag)
			{
				result = defaultReturnValue;
			}
			else
			{
				bool flag2 = obj is int;
				if (flag2)
				{
					result = (int)obj;
				}
				else
				{
					string s = obj.ToString().Trim();
					int num;
					bool flag3 = int.TryParse(s, out num);
					if (flag3)
					{
						result = num;
					}
					else
					{
						result = defaultReturnValue;
					}
				}
			}
			return result;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x000782AC File Offset: 0x000764AC
		public static object ExtractParameterValue(this RunReportResult currentReportResult, params string[] possibleParameterNames)
		{
			bool flag = currentReportResult == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IList<ReportParameter> currentReportParameters = currentReportResult.CurrentReportParameters;
				ReportParameter reportParameter = (currentReportParameters != null) ? currentReportParameters.FirstOrDefault((ReportParameter g) => possibleParameterNames.Any((string m) => m.Equals(g.Name, StringComparison.OrdinalIgnoreCase))) : null;
				bool flag2 = reportParameter != null;
				if (flag2)
				{
					result = reportParameter.Value;
				}
				else
				{
					DataTable dataTable = currentReportResult.ExtractPrimaryTable();
					bool flag3 = dataTable == null || dataTable.Rows.Count < 1;
					if (flag3)
					{
						result = null;
					}
					else
					{
						string text = (from DataColumn dc in dataTable.Columns
						select dc.ColumnName).FirstOrDefault((string g) => possibleParameterNames.Any((string m) => m.Equals(g, StringComparison.OrdinalIgnoreCase)));
						bool flag4 = string.IsNullOrEmpty(text);
						if (flag4)
						{
							result = null;
						}
						else
						{
							result = ((dataTable.Rows[0][text] is DBNull) ? null : dataTable.Rows[0][text]);
						}
					}
				}
			}
			return result;
		}
	}
}
