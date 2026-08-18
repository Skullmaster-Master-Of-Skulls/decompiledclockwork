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
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200006C RID: 108
	public class DataSyncOldCourses : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000454 RID: 1108 RVA: 0x0000672B File Offset: 0x0000492B
		public DataSyncOldCourses()
		{
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00018A8C File Offset: 0x00016C8C
		public DataSyncOldCourses(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00018A9E File Offset: 0x00016C9E
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x00018AA6 File Offset: 0x00016CA6
		public OperationContext OpContext { get; set; }

		// Token: 0x06000458 RID: 1112 RVA: 0x00018AB0 File Offset: 0x00016CB0
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentReportResult, ReportFunction Function)
		{
			DataTable primaryDataTable = CurrentReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable == null || primaryDataTable.Rows.Count <= 0;
			if (!flag)
			{
				string text = null;
				bool flag2 = primaryDataTable.Rows.Count < 1;
				if (flag2)
				{
					ReportParameter reportParameter;
					if (CurrentReportResult == null)
					{
						reportParameter = null;
					}
					else
					{
						IList<ReportParameter> currentReportParameters = CurrentReportResult.CurrentReportParameters;
						if (currentReportParameters == null)
						{
							reportParameter = null;
						}
						else
						{
							reportParameter = currentReportParameters.FirstOrDefault((ReportParameter g) => g.Name.Equals("studentno", StringComparison.OrdinalIgnoreCase) || g.Name.Equals("student_no", StringComparison.OrdinalIgnoreCase));
						}
					}
					ReportParameter reportParameter2 = reportParameter;
					bool flag3 = ((reportParameter2 != null) ? reportParameter2.Value : null) != null;
					if (flag3)
					{
						text = reportParameter2.Value.ToString().Trim().ToUpper();
					}
				}
				bool flag4 = string.IsNullOrEmpty(text);
				if (flag4)
				{
					text = (primaryDataTable.Columns.Contains("student_no") ? primaryDataTable.Rows[0]["student_no"].ToString().Trim().ToUpper() : "");
				}
				bool flag5 = text.Length <= 0;
				if (!flag5)
				{
					IDataSyncCourseManager dataSyncCourseManager = new DataSyncCourseManager(this.OpContext);
					List<DataSyncExternalCourseRowPart> rowPartsFromDataTable = dataSyncCourseManager.GetRowPartsFromDataTable(primaryDataTable);
					List<DataSyncExternalCourse> externalCourses = dataSyncCourseManager.ParseExternalCourseRowParts(rowPartsFromDataTable);
					IList<DataSyncExternalCourseSyncResult> list = dataSyncCourseManager.ImportOldCourses(text, externalCourses);
					DataTable dataTable = new DataTable
					{
						TableName = "Results"
					};
					dataTable.Columns.Add("msg");
					foreach (DataSyncExternalCourseSyncResult result in list)
					{
						dataTable.Rows.Add(new object[]
						{
							result.GetResultString()
						});
					}
					Result.Data.Table = dataTable;
				}
			}
		}
	}
}
