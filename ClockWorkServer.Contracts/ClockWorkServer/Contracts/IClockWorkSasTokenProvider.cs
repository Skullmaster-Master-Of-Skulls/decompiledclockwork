using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200002C RID: 44
	[ServiceContract(Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IClockWorkSasTokenProvider : IService
	{
		// Token: 0x0600018A RID: 394
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidCredentialsFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		GetContainerSasUriResp GetContainerSasUri(GetContainerSasUriReq request);

		// Token: 0x0600018B RID: 395
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidCredentialsFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		GetBlobSasUriResp GetBlobSasUri(GetBlobSasUriReq request);

		// Token: 0x0600018C RID: 396
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidCredentialsFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		GetUpdatingSystemClientPrivateContainerSasUriResp GetUpdatingSystemClientPrivateContainerSasUri(GetUpdatingSystemClientPrivateContainerSasUriReq request);
	}
}
