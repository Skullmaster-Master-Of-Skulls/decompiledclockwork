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
	// Token: 0x0200007D RID: 125
	[ServiceContract(Name = "ServiceProviderService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IServiceProvider : IService
	{
		// Token: 0x0600037D RID: 893
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadProviderByIdResp LoadProviderById(LoadProviderByIdReq Request);

		// Token: 0x0600037E RID: 894
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadProviderByStudent_noResp LoadProviderByStudent_no(LoadProviderByStudent_noReq Request);

		// Token: 0x0600037F RID: 895
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadProviderByUserNameResp LoadProviderByUserName(LoadProviderByUserNameReq Request);

		// Token: 0x06000380 RID: 896
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadProviderByExternalIdResp LoadProviderByExternalId(LoadProviderByExternalIdReq Request);

		// Token: 0x06000381 RID: 897
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateProviderResp CreateProvider(CreateProviderReq Request);

		// Token: 0x06000382 RID: 898
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateProviderResp UpdateProvider(UpdateProviderReq Request);

		// Token: 0x06000383 RID: 899
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteProviderResp DeleteProvider(DeleteProviderReq Request);

		// Token: 0x06000384 RID: 900
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AddProviderCourseRegistrationResp AddProviderCourseRegistration(AddProviderCourseRegistrationReq Request);

		// Token: 0x06000385 RID: 901
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateProviderCourseRegistrationResp UpdateProviderCourseRegistration(UpdateProviderCourseRegistrationReq Request);

		// Token: 0x06000386 RID: 902
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteProviderCourseRegistrationResp DeleteProviderCourseRegistration(DeleteProviderCourseRegistrationReq Request);

		// Token: 0x06000387 RID: 903
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllProvidersWithAtLeastOneActiveApplicationResp LoadAllProvidersWithAtLeastOneActiveApplication(LoadAllProvidersWithAtLeastOneActiveApplicationReq Request);
	}
}
