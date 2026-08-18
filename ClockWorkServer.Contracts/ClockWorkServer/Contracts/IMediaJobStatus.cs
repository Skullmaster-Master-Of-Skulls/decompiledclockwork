using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200000A RID: 10
	[ServiceContract(Name = "MediaJobStatusService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMediaJobStatus : IService
	{
		// Token: 0x0600004C RID: 76
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateMediaJobStatusResp CreateMediaJobStatus(CreateMediaJobStatusReq request);

		// Token: 0x0600004D RID: 77
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaJobStatusByNameResp GetMediaJobStatusByName(GetMediaJobStatusByNameReq request);

		// Token: 0x0600004E RID: 78
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetMediaJobStatusByGroupResp GetMediaJobStatusByGroup(GetMediaJobStatusByGroupReq request);

		// Token: 0x0600004F RID: 79
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAllMediaJobStatusResp GetAllMediaJobStatus(GetAllMediaJobStatusReq request);
	}
}
