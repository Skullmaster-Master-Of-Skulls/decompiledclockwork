using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.DataFileIO.cs.Excel;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000080 RID: 128
	public class ImportExcel : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x0001B761 File Offset: 0x00019961
		public ImportExcel()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001B77C File Offset: 0x0001997C
		public ImportExcel(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0001B79A File Offset: 0x0001999A
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x0001B7A2 File Offset: 0x000199A2
		public OperationContext OpContext { get; set; }

		// Token: 0x060004CE RID: 1230 RVA: 0x0001B7AC File Offset: 0x000199AC
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			ImportExcelParameters importExcelParameters = function.GetDefaultFunctionParameter().ConvertXmlToImportExcelParameters();
			bool flag = importExcelParameters == null;
			if (flag)
			{
				importExcelParameters = new ImportExcelParameters();
			}
			bool flag2 = string.IsNullOrEmpty(importExcelParameters.ExcelFilenameWithPath);
			if (flag2)
			{
				throw new Exception("ImportExcelFunction: missing filename of excel file to import.");
			}
			bool flag3 = !string.IsNullOrEmpty(importExcelParameters.WorksheetName);
			bool flag4 = !flag3 && importExcelParameters.WorksheetIndex < 1;
			if (flag4)
			{
				importExcelParameters.WorksheetIndex = 0;
			}
			DataTable table = flag3 ? ExcelUtility.LoadExcelFromFile(importExcelParameters.ExcelFilenameWithPath, importExcelParameters.WorksheetName) : ExcelUtility.LoadExcelFromFile(importExcelParameters.ExcelFilenameWithPath, importExcelParameters.WorksheetIndex);
			result.Data.Table = table;
		}

		// Token: 0x040000EE RID: 238
		private ReportDAO dao;
	}
}
