using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DataSync;
using TechnoPro.Common.Public;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000079 RID: 121
	[ServiceContract(Name = "DataSyncService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IDataSyncAsync : IDataSync, IService
	{
		// Token: 0x0600050E RID: 1294
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginRunFullDataSyncForExistingStudent(RunFullDataSyncForExistingStudentReq req, AsyncCallback callback, object asyncState);

		// Token: 0x0600050F RID: 1295
		RunFullDataSyncForExistingStudentResp EndRunFullDataSyncForExistingStudent(IAsyncResult result);

		// Token: 0x06000510 RID: 1296
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginPreviewDataSyncData(PreviewDataSyncDataReq req, AsyncCallback callback, object asyncState);

		// Token: 0x06000511 RID: 1297
		PreviewDataSyncDataResp EndPreviewDataSyncData(IAsyncResult result);

		// Token: 0x06000512 RID: 1298
		void Close();
	}
}
