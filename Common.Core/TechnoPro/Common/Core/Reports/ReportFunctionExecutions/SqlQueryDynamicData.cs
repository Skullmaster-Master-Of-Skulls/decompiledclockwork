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
	// Token: 0x0200009D RID: 157
	public class SqlQueryDynamicData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x0002032C File Offset: 0x0001E52C
		public SqlQueryDynamicData()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00020347 File Offset: 0x0001E547
		public SqlQueryDynamicData(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00020365 File Offset: 0x0001E565
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0002036D File Offset: 0x0001E56D
		public OperationContext OpContext { get; set; }

		// Token: 0x06000579 RID: 1401 RVA: 0x00020378 File Offset: 0x0001E578
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, TechnoPro.Common.Public.Entities.Reports.ReportFunction function)
		{
			DataTable dataTable = this.dao.RunReportSql(SqlQuery.ExtractReportParameters(CurrentWholeReportResult), function.GetDefaultFunctionParameter());
			DataTable staffNamesTable = new DataTable("t");
			DataSet dataSet = new DataSet();
			eFunctionType functionCode = function.FunctionCode;
			eFunctionType eFunctionType = functionCode;
			if (eFunctionType <= eFunctionType.Sql_Query_Dynamic_Data_Keep_Rows_Without_Data_Info)
			{
				if (eFunctionType == eFunctionType.Sql_Query_Dynamic_Data || eFunctionType == eFunctionType.Sql_Query_Dynamic_Data_Keep_Rows_Without_Data_Info)
				{
					dataTable = TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.FormatStudentData(dataTable, ref dataSet, staffNamesTable, function.FunctionCode == eFunctionType.Sql_Query_Dynamic_Data_Keep_Rows_Without_Data_Info, this.OpContext);
				}
			}
			else if (eFunctionType != eFunctionType.Sql_Query_Dynamic_Data_2_Per_Student)
			{
				if (eFunctionType == eFunctionType.Sql_Query_Dynamic_Data_2_Per_Appointment)
				{
					DataView dataView = TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.FormatAndMapToColumnsStudentDataPerAppointment(dataTable.DefaultView, ref dataSet, staffNamesTable, this.OpContext);
					dataTable = dataView.Table;
				}
			}
			else
			{
				DataView dataView2 = TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.FormatAndMapToColumnsStudentDataPerStudent(new DataView(dataTable), ref dataSet, staffNamesTable, this.OpContext);
				dataTable = dataView2.Table;
			}
			result.Data.Table = dataTable;
		}

		// Token: 0x04000116 RID: 278
		private ReportDAO dao;
	}
}
