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
	// Token: 0x0200007F RID: 127
	[ServiceContract(Name = "ServiceProviderCourseRegistrationService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IServiceProviderCourseRegistration : IService
	{
		// Token: 0x06000390 RID: 912
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCourseRegistrationsByProviderResp LoadCourseRegistrationsByProvider(LoadCourseRegistrationsByProviderReq Request);

		// Token: 0x06000391 RID: 913
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadCourseRegistrationByIdResp LoadCourseRegistrationById(LoadCourseRegistrationByIdReq Request);

		// Token: 0x06000392 RID: 914
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateCourseRegistrationStatusResp UpdateCourseRegistrationStatus(UpdateCourseRegistrationStatusReq Request);

		// Token: 0x06000393 RID: 915
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateCourseRegistrationResp UpdateCourseRegistration(UpdateCourseRegistrationReq Request);

		// Token: 0x06000394 RID: 916
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DeleteCourseRegistrationResp DeleteCourseRegistration(DeleteCourseRegistrationReq Request);

		// Token: 0x06000395 RID: 917
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateCourseRegistrationResp CreateCourseRegistration(CreateCourseRegistrationReq Request);
	}
}
