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
	// Token: 0x02000081 RID: 129
	[ServiceContract(Name = "ServiceProviderTypeService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IServiceProviderType : IService
	{
		// Token: 0x06000396 RID: 918
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadProviderTypeByIdResp LoadProviderTypeById(LoadProviderTypeByIdReq Request);

		// Token: 0x06000397 RID: 919
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadProviderTypeByBehaviourCodeResp LoadProviderTypeByBehaviourCode(LoadProviderTypeByBehaviourCodeReq Request);

		// Token: 0x06000398 RID: 920
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllProviderTypesResp LoadAllProviderTypes(LoadAllProviderTypesReq Request);

		// Token: 0x06000399 RID: 921
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateProviderTypeResp CreateProviderType(CreateProviderTypeReq Request);

		// Token: 0x0600039A RID: 922
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateProviderTypeResp UpdateProviderType(UpdateProviderTypeReq Request);

		// Token: 0x0600039B RID: 923
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteProviderTypeResp DeleteProviderType(DeleteProviderTypeReq Request);
	}
}
