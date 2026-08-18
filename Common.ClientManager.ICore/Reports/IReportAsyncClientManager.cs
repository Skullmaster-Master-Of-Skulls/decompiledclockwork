using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.ClientManager.ICore.Reports
{
	// Token: 0x02000024 RID: 36
	public interface IReportAsyncClientManager : IDisposable
	{
		// Token: 0x060000D1 RID: 209
		IAsyncResult BeginExecuteReport(AsyncCallback callback, object asyncState, int ReportId, IList<eFunctionType> FunctionTypesToSkip, IList<ReportParameterDTO> ReportParameters, IList<int> OnlyRunFunctionIds, eReportExecutedFromLocation ExecutedFromLocation);

		// Token: 0x060000D2 RID: 210
		RunReportResultDTO EndExecuteReport(IAsyncResult Result);
	}
}
