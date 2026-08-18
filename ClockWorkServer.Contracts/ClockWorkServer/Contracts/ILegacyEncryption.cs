using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Encryption;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200004C RID: 76
	[ServiceContract(Name = "LegacyEncryptionService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILegacyEncryption : IService
	{
		// Token: 0x06000252 RID: 594
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		EncryptResp Encrypt(EncryptReq Request);

		// Token: 0x06000253 RID: 595
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DecryptResp Decrypt(DecryptReq Request);

		// Token: 0x06000254 RID: 596
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		EncryptOrDecryptNameDataTableBatchResp EncryptOrDecryptNameDataTableBatch(EncryptOrDecryptNameDataTableBatchReq Request);

		// Token: 0x06000255 RID: 597
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		EncryptDataResp EncryptData(EncryptDataReq Request);

		// Token: 0x06000256 RID: 598
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DecryptDataResp DecryptData(DecryptDataReq Request);

		// Token: 0x06000257 RID: 599
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		EncodeUrlVariableResp EncodeUrlVariable(EncodeUrlVariableReq Request);

		// Token: 0x06000258 RID: 600
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DecryptLegacyDataItemsNeedingDecryptionResp DecryptLegacyDataItemsNeedingDecryption(DecryptLegacyDataItemsNeedingDecryptionReq Request);
	}
}
