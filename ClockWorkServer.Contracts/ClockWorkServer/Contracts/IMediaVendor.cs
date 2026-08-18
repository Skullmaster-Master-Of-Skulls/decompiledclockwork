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
	// Token: 0x0200000D RID: 13
	[ServiceContract(Name = "MediaVendorService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMediaVendor : IService
	{
		// Token: 0x06000068 RID: 104
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateMediaVendorResp CreateMediaVendor(CreateMediaVendorReq request);

		// Token: 0x06000069 RID: 105
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateMediaVendorResp UpdateMediaVendor(UpdateMediaVendorReq request);

		// Token: 0x0600006A RID: 106
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteMediaVendorResp DeleteMediaVendor(DeleteMediaVendorReq request);

		// Token: 0x0600006B RID: 107
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaVendorByIdResp LoadMediaVendorById(LoadMediaVendorByIdReq request);

		// Token: 0x0600006C RID: 108
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadMediaVendorByNameResp LoadMediaVendorByName(LoadMediaVendorByNameReq request);

		// Token: 0x0600006D RID: 109
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllMediaVendorsResp LoadAllMediaVendors(LoadAllMediaVendorsReq request);
	}
}
