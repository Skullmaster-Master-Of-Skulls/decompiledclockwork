using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000071 RID: 113
	public class DataSync_LoadDataFromClockWork : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x0001A24C File Offset: 0x0001844C
		public DataSync_LoadDataFromClockWork()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001A267 File Offset: 0x00018467
		public DataSync_LoadDataFromClockWork(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001A285 File Offset: 0x00018485
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0001A28D File Offset: 0x0001848D
		public OperationContext OpContext { get; set; }

		// Token: 0x06000480 RID: 1152 RVA: 0x0001A298 File Offset: 0x00018498
		private static string GetValueFromTableOrParameters(DataTable table, string[] lookupFieldColumns, IList<ReportParameter> CurrentReportParameters)
		{
			DataTable t = table ?? new DataTable("table");
			bool flag = t.Rows.Count > 0;
			string text;
			if (flag)
			{
				DataRow dr0 = t.Rows[0];
				List<string> source = (from g in lookupFieldColumns
				where t.Columns.Contains(g)
				select g).ToList<string>();
				List<string> list = (from g in source
				select dr0[g].ToString().Trim() into h
				where h.Length > 0
				select h).ToList<string>();
				text = ((list.Count > 0) ? list[0] : string.Empty);
			}
			else
			{
				text = string.Empty;
			}
			bool flag2 = text.Length > 0 || CurrentReportParameters == null;
			string result;
			if (flag2)
			{
				result = text;
			}
			else
			{
				IEnumerable<KeyValuePair<string, ReportParameter>> source2 = from m in lookupFieldColumns.ToDictionary((string g) => g, (string g) => CurrentReportParameters.FirstOrDefault((ReportParameter h) => h.Name.Equals(g, StringComparison.OrdinalIgnoreCase)))
				where m.Value != null
				select m;
				List<string> list2 = (from g in source2
				select (g.Value.Value == null) ? "" : g.Value.Value.ToString() into h
				where h.Length > 0
				select h).ToList<string>();
				result = ((list2.Count > 0) ? list2[0] : string.Empty);
			}
			return result;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001A464 File Offset: 0x00018664
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable dataTable = CurrentWholeReportResult.GetPrimaryDataTable() ?? new DataTable("t2");
			bool flag = string.IsNullOrEmpty(dataTable.TableName);
			if (flag)
			{
				dataTable.TableName = "t";
			}
			DataTable table = dataTable;
			try
			{
				string defaultFunctionParameter = function.GetDefaultFunctionParameter();
				DataSyncLoadDataFromClockWorkParameters dataSyncLoadDataFromClockWorkParameters = defaultFunctionParameter.ConvertXmlToDataSyncLoadDataFromClockWorkParameters();
				string[] array;
				if (dataSyncLoadDataFromClockWorkParameters.LookupFieldParameterNames != null && dataSyncLoadDataFromClockWorkParameters.LookupFieldParameterNames.Length >= 1)
				{
					array = dataSyncLoadDataFromClockWorkParameters.LookupFieldParameterNames;
				}
				else
				{
					string[] array2 = new string[2];
					array2[0] = "studentno";
					array = array2;
					array2[1] = "student_no";
				}
				string[] lookupFieldColumns = array;
				string valueFromTableOrParameters = DataSync_LoadDataFromClockWork.GetValueFromTableOrParameters(table, lookupFieldColumns, (CurrentWholeReportResult == null) ? null : CurrentWholeReportResult.CurrentReportParameters);
				IDataSyncManager dataSyncManager = new DataSyncManager(this.OpContext);
				string sql = (dataSyncLoadDataFromClockWorkParameters.OverrideSql ?? "").Trim();
				DataTable table2;
				switch (dataSyncLoadDataFromClockWorkParameters.LoadDataType)
				{
				case eDataSyncLoadDataFromClockWorkParametersType.SingleTable:
					table2 = dataSyncManager.LoadCustomData(dataSyncLoadDataFromClockWorkParameters.CustomTableNameWithoutCustomPrefix, valueFromTableOrParameters, dataSyncLoadDataFromClockWorkParameters.LookupExternalColumnName);
					break;
				case eDataSyncLoadDataFromClockWorkParametersType.SingleTableEncryptedLookup:
					table2 = dataSyncManager.LoadCustomDataByEncryptedLookupField(dataSyncLoadDataFromClockWorkParameters.CustomTableNameWithoutCustomPrefix, valueFromTableOrParameters, dataSyncLoadDataFromClockWorkParameters.LookupExternalColumnName, Array.Empty<string>());
					break;
				case eDataSyncLoadDataFromClockWorkParametersType.MultipleTables:
					table2 = dataSyncManager.LoadCustomDataWithCustomSql(sql, valueFromTableOrParameters);
					break;
				default:
					table2 = null;
					break;
				}
				result.Data.Table = table2;
			}
			catch (Exception ex)
			{
				string text = string.Format("Common.Core.Reports.ReportFunctionExecutions.DataSync_LoadDataFromClockWork:err={0}", ex.ToString());
				result.Result = new RunFunctionResult
				{
					Status = new RunStatus
					{
						ErrorMessage = text,
						LastStatusStep = eRunStatusStep.Failed
					},
					Function = function
				};
				CWLogger.Logger.Error(text);
			}
		}

		// Token: 0x040000D4 RID: 212
		private ReportDAO dao;
	}
}
