using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000029 RID: 41
	[ServiceContract(Name = "CASAuthenticationService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface ICASAuthentication : IService
	{
		// Token: 0x0600017A RID: 378
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CASAuthenticationParameters.AuthenticateCASResp AuthenticateCAS(CASAuthenticationParameters.AuthenticateCASReq Request);

		// Token: 0x0600017B RID: 379
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsResp AuthenticateCASWithOverrideOptions(CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsReq Request);
	}
}
