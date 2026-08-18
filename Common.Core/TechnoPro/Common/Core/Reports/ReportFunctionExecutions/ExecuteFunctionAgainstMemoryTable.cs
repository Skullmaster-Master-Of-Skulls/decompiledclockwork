using System;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000078 RID: 120
	public class ExecuteFunctionAgainstMemoryTable : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0001AE28 File Offset: 0x00019028
		public ExecuteFunctionAgainstMemoryTable()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001AE43 File Offset: 0x00019043
		public ExecuteFunctionAgainstMemoryTable(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001AE61 File Offset: 0x00019061
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0001AE69 File Offset: 0x00019069
		public OperationContext OpContext { get; set; }

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001AE74 File Offset: 0x00019074
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			string text = function.GetDefaultFunctionParameter();
			int num = text.IndexOf(",");
			string text2 = text.Substring(0, num);
			text = text.Substring(num + 1);
			int num2 = int.Parse(text2);
			bool flag = Enum.IsDefined(typeof(eFunctionType), num2);
			if (flag)
			{
				eFunctionType functionCode = (eFunctionType)num2;
				ReportManager reportManager = new ReportManager(this.OpContext);
				ReportFunction function2 = new ReportFunction
				{
					FunctionCode = functionCode,
					Title = "Ad hoc Execute Report Function from function id=" + function.ReportFunctionId.ToString()
				};
				RunFunctionResultWithData runFunctionResultWithData = reportManager.RunFunction(CurrentWholeReportResult, function2);
				result.Data = runFunctionResultWithData.Data;
				result.Result = runFunctionResultWithData.Result;
				result.ReportParametersOut = runFunctionResultWithData.ReportParametersOut;
			}
			else
			{
				string errorMessage = string.Format("Common.Core.Reports.ReportFunctionExecutions.ExecuteFunctionAgainstMemoryTable:Unable to parse function code:fc={0}", text2 ?? "NULL");
				result.Result = new RunFunctionResult
				{
					Status = new RunStatus
					{
						ErrorMessage = errorMessage,
						LastStatusStep = eRunStatusStep.Failed
					},
					Function = function
				};
			}
		}

		// Token: 0x040000E1 RID: 225
		private ReportDAO dao;
	}
}
