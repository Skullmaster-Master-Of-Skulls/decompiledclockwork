using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000118 RID: 280
	[ServiceContract(Name = "ReportService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IReportAsync : IReport, IService
	{
		// Token: 0x06000AE4 RID: 2788
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginExecuteReport(ExecuteReportReq req, AsyncCallback callback, object asyncState);

		// Token: 0x06000AE5 RID: 2789
		ExecuteReportResp EndExecuteReport(IAsyncResult result);

		// Token: 0x06000AE6 RID: 2790
		void Close();
	}
}
