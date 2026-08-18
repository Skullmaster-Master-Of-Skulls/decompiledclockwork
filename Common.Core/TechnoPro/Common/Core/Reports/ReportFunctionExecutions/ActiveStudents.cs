using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000090 RID: 144
	public class ActiveStudents : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x0000672B File Offset: 0x0000492B
		public ActiveStudents()
		{
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001DA80 File Offset: 0x0001BC80
		public ActiveStudents(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001DA94 File Offset: 0x0001BC94
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentReportResult, ReportFunction Function)
		{
			DateTime? dateTime;
			DateTime? dateTime2;
			ActiveStudentsWithAccommodations.ExtractStartDateAndEndDateFromParameters(Function, out dateTime, out dateTime2);
			bool flag = dateTime == null || dateTime2 == null;
			if (flag)
			{
				throw new Exception("Invalid or missing startdate/enddate");
			}
			Result.ReportParametersOut.Add(new ReportParameter
			{
				Name = "startdate",
				Value = dateTime.Value
			});
			Result.ReportParametersOut.Add(new ReportParameter
			{
				Name = "enddate",
				Value = dateTime2.Value
			});
			OperationContext operationContext;
			if ((operationContext = this.OpContext) == null)
			{
				(operationContext = new OperationContext()).WhoAmI = 1;
			}
			OperationContext opContext = operationContext;
			IStudentManagementManager studentManagementManager = new StudentManagementManager(opContext);
			IList<PersonBase> list = studentManagementManager.LoadActiveStudents(dateTime.Value, dateTime2.Value);
			DataTable dataTable = new DataTable("t");
			dataTable.Columns.Add("personid", typeof(int));
			dataTable.Columns.Add("LastName");
			dataTable.Columns.Add("FirstName");
			dataTable.Columns.Add("MiddleName");
			dataTable.Columns.Add("StudentNumber");
			foreach (PersonBase personBase in list)
			{
				dataTable.Rows.Add(new object[]
				{
					personBase.PersonId,
					personBase.LastName ?? "",
					personBase.FirstName ?? "",
					personBase.MiddleName ?? "",
					personBase.Student_no ?? ""
				});
			}
			Result.Data.Table = dataTable;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0001DC9C File Offset: 0x0001BE9C
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0001DCA4 File Offset: 0x0001BEA4
		public OperationContext OpContext { get; set; }
	}
}
