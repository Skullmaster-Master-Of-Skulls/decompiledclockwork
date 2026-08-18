using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200007E RID: 126
	[ServiceContract(Name = "ServiceProviderApplicationService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IServiceProviderApplication : IService
	{
		// Token: 0x06000388 RID: 904
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadApplicationByIdResp LoadApplicationById(LoadApplicationByIdReq Request);

		// Token: 0x06000389 RID: 905
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadApplicationByProviderAndTypeResp LoadApplicationByProviderAndType(LoadApplicationByProviderAndTypeReq Request);

		// Token: 0x0600038A RID: 906
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateApplicationResp CreateApplication(CreateApplicationReq Request);

		// Token: 0x0600038B RID: 907
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateApplicationResp UpdateApplication(UpdateApplicationReq Request);

		// Token: 0x0600038C RID: 908
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteApplicationResp DeleteApplication(DeleteApplicationReq Request);

		// Token: 0x0600038D RID: 909
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateApplicationAvailabilityTypeResp UpdateApplicationAvailabilityType(UpdateApplicationAvailabilityTypeReq Request);

		// Token: 0x0600038E RID: 910
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadApplicationsBySPProviderTypeResp LoadApplicationsBySPProviderType(LoadApplicationsBySPProviderTypeReq Request);

		// Token: 0x0600038F RID: 911
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadApplicationsBySPProviderResp LoadApplicationsBySPProvider(LoadApplicationsBySPProviderReq Request);
	}
}
