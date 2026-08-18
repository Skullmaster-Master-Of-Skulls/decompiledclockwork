using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.DAO.Reports.Impl.Legacy;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200005D RID: 93
	public class AddStudentsToMasterStudentTableInMemory : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x00014EB7 File Offset: 0x000130B7
		public AddStudentsToMasterStudentTableInMemory()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00014ED2 File Offset: 0x000130D2
		public AddStudentsToMasterStudentTableInMemory(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00014EF0 File Offset: 0x000130F0
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00014EF8 File Offset: 0x000130F8
		public OperationContext OpContext { get; set; }

		// Token: 0x060003F6 RID: 1014 RVA: 0x00014F04 File Offset: 0x00013104
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, TechnoPro.Common.Public.Entities.Reports.ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable != null;
			if (flag)
			{
				string[] array = TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(function.GetDefaultFunctionParameter(), true);
				string name = array[1];
				string columnName = array[0];
				MakeATableTheCurrentTable makeATableTheCurrentTable = new MakeATableTheCurrentTable(this.OpContext);
				RunFunctionData runFunctionData = makeATableTheCurrentTable.FindDataByName(name, CurrentWholeReportResult);
				bool flag2 = runFunctionData == null;
				if (flag2)
				{
					DataTable dataTable = new DataTable("students");
					dataTable.Columns.Add("student_no");
					runFunctionData = new RunFunctionData
					{
						Table = dataTable,
						TableSort = "",
						IsPrimary = false,
						Name = name,
						AddToAdditionalData = true
					};
					NameATable nameATable = new NameATable(this.OpContext);
					nameATable.AddTableToAdditionalData(name, dataTable, CurrentWholeReportResult, "");
				}
				DataTable table = runFunctionData.Table;
				foreach (object obj in primaryDataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string text = dataRow[columnName].ToString().Trim();
					DataRow[] array2 = table.Select("student_no='" + text.Replace("'", "''") + "'");
					bool flag3 = array2.Length < 1;
					if (flag3)
					{
						DataRow dataRow2 = table.NewRow();
						dataRow2[columnName] = text;
						table.Rows.Add(dataRow2);
					}
				}
			}
		}

		// Token: 0x040000B6 RID: 182
		private ReportDAO dao;
	}
}
