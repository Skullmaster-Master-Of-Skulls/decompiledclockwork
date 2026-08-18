using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200004E RID: 78
	[ServiceContract(Namespace = "http://tpro.ca", Name = "FileStorageService")]
	[SoapHeaders]
	[XmlComments]
	public interface IFileStorage : IService
	{
		// Token: 0x0600025B RID: 603
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetFileResp GetFile(GetFileReq request);

		// Token: 0x0600025C RID: 604
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SaveFileResp SaveFile(SaveFileReq request);
	}
}
