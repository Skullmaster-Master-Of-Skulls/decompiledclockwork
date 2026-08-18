using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000060 RID: 96
	[ServiceContract(Name = "LegacyDynamicDataService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILegacyDynamicData : IService
	{
		// Token: 0x060002D4 RID: 724
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetDynamicDataDecryptedPreviewItemsResp GetDynamicDataDecryptedPreviewItems(GetDynamicDataDecryptedPreviewItemsReq Request);

		// Token: 0x060002D5 RID: 725
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ReverseEncryptionOnDataResp ReverseEncryptionOnData(ReverseEncryptionOnDataReq Request);

		// Token: 0x060002D6 RID: 726
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LookupStaffSignatureBase64Resp LookupStaffSignatureBase64(LookupStaffSignatureBase64Req Request);
	}
}
