using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200009A RID: 154
	[ServiceContract(Name = "FileStorageService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IFileStorageAsync : IFileStorage, IService
	{
		// Token: 0x0600064B RID: 1611
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetFile(GetFileReq request, AsyncCallback callback, object asyncState);

		// Token: 0x0600064C RID: 1612
		GetFileResp EndGetFile(IAsyncResult result);
	}
}
