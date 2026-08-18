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
	// Token: 0x0200000C RID: 12
	[ServiceContract(Name = "MediaPublisherService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMediaPublisher : IService
	{
		// Token: 0x06000062 RID: 98
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreatePublisherResp CreatePublisher(CreatePublisherReq request);

		// Token: 0x06000063 RID: 99
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdatePublisherResp UpdatePublisher(UpdatePublisherReq request);

		// Token: 0x06000064 RID: 100
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeletePublisherResp DeletePublisher(DeletePublisherReq request);

		// Token: 0x06000065 RID: 101
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPublisherByIdResp LoadPublisherById(LoadPublisherByIdReq request);

		// Token: 0x06000066 RID: 102
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPublisherByNameResp LoadPublisherByName(LoadPublisherByNameReq request);

		// Token: 0x06000067 RID: 103
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllPublishersResp LoadAllPublishers(LoadAllPublishersReq request);
	}
}
