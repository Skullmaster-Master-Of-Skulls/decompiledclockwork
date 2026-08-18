using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000069 RID: 105
	public class DataSyncCourses2 : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x0000672B File Offset: 0x0000492B
		public DataSyncCourses2()
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000186D9 File Offset: 0x000168D9
		public DataSyncCourses2(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x000186EB File Offset: 0x000168EB
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x000186F3 File Offset: 0x000168F3
		public OperationContext OpContext { get; set; }

		// Token: 0x06000449 RID: 1097 RVA: 0x000186FC File Offset: 0x000168FC
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable == null;
			if (!flag)
			{
				string text = null;
				bool flag2 = primaryDataTable.Rows.Count < 1;
				if (flag2)
				{
					bool flag3 = CurrentWholeReportResult != null && CurrentWholeReportResult.CurrentReportParameters != null;
					if (flag3)
					{
						ReportParameter reportParameter = CurrentWholeReportResult.CurrentReportParameters.FirstOrDefault((ReportParameter g) => g.Name.Equals("studentno", StringComparison.OrdinalIgnoreCase) || g.Name.Equals("student_no", StringComparison.OrdinalIgnoreCase));
						bool flag4 = ((reportParameter != null) ? reportParameter.Value : null) != null;
						if (flag4)
						{
							text = reportParameter.Value.ToString().Trim().ToUpper();
						}
					}
				}
				bool flag5 = string.IsNullOrEmpty(text);
				if (flag5)
				{
					text = (primaryDataTable.Columns.Contains("student_no") ? primaryDataTable.Rows[0]["student_no"].ToString().Trim().ToUpper() : "");
				}
				bool flag6 = text.Length <= 0;
				if (!flag6)
				{
					DataSyncOperationContext dataSyncOperationContext = this.OpContext.ConvertTo<DataSyncOperationContext>();
					dataSyncOperationContext.BatchDataSyncLogId = CurrentWholeReportResult.ExtractParameterValueInt(0, new string[]
					{
						"BatchDataSyncLogId"
					});
					IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(dataSyncOperationContext);
					List<DataSyncExternalCourseRowPart> rowPartsFromDataTable = dataSyncCourseManager.GetRowPartsFromDataTable(primaryDataTable);
					List<DataSyncExternalCourse> allExternalCourses = dataSyncCourseManager.ParseExternalCourseRowParts(rowPartsFromDataTable);
					List<DataSyncExternalCourseSyncResult> list = dataSyncCourseManager.DataSyncCourses(text, allExternalCourses);
					DataTable dataTable = new DataTable
					{
						TableName = "Results"
					};
					dataTable.Columns.Add("msg");
					foreach (DataSyncExternalCourseSyncResult result2 in list)
					{
						dataTable.Rows.Add(new object[]
						{
							result2.GetResultString()
						});
					}
					result.Data.Table = dataTable;
				}
			}
		}
	}
}
