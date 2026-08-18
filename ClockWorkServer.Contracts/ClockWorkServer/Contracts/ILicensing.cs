using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Licensing;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000CB RID: 203
	[ServiceContract(Name = "LicensingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ILicensing : IService
	{
		// Token: 0x06000593 RID: 1427
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LicensingImportKeyResp ImportKey(LicensingImportKeyReq licImportKeyReq);

		// Token: 0x06000594 RID: 1428
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LicensingSupportPlanKeyResp GetSupportPlanKey(LicensingSupportPlanKeyReq licSupportPlanKeyReq);

		// Token: 0x06000595 RID: 1429
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LicensingKeysResp GetKeys(LicensingKeysReq licKeysReq);

		// Token: 0x06000596 RID: 1430
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LicensingProductStatusResp GetProductStatus(LicensingProductStatusReq licensingProductStatusReq);

		// Token: 0x06000597 RID: 1431
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LicensingValidationParametersResp SaveValidationParameters(LicensingValidationParametersReq licValidationParametersReq);

		// Token: 0x06000598 RID: 1432
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetLicenseStateResp GetLicenseState(GetLicenseStateReq request);
	}
}
