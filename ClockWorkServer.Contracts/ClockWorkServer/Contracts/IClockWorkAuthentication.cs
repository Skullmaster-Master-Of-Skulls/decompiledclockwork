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
	// Token: 0x0200002A RID: 42
	[ServiceContract(Name = "ClockWorkAuthenticationService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IClockWorkAuthentication : IService
	{
		// Token: 0x0600017C RID: 380
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		FindStudentByUserNameResp FindStudentByUserName(FindStudentByUserNameReq Request);

		// Token: 0x0600017D RID: 381
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LookupAuthenticatedUserInClockWorkResp LookupAuthenticatedUserInClockWork(LookupAuthenticatedUserInClockWorkReq Request);

		// Token: 0x0600017E RID: 382
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		AuthenticateAndAuthorizeUserResp AuthenticateAndAuthorizeUser(AuthenticateAndAuthorizeUserReq Request);
	}
}
