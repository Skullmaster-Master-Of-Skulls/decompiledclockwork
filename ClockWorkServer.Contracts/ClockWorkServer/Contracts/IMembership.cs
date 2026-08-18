using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Attributes;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000CD RID: 205
	[ServiceContract(Name = "MembershipService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IMembership : IService, IConnectivity
	{
		// Token: 0x0600059D RID: 1437
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidCredentialsFault))]
		[FaultContract(typeof(UnlicensedApplicationFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		[AllowAnonymous]
		LogonResult Logon(Credential credential);

		// Token: 0x0600059E RID: 1438
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidCredentialsFault))]
		[FaultContract(typeof(InvalidHashCredentialsFault))]
		[FaultContract(typeof(UnlicensedApplicationFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		[AllowAnonymous]
		LogonResult LogonSSO(LogonSSOReq request);

		// Token: 0x0600059F RID: 1439
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidCredentialsFault))]
		[FaultContract(typeof(UnlicensedApplicationFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		[AllowAnonymous]
		LogonResult LogonAsUser(Credential credential, string logonAsUsername);

		// Token: 0x060005A0 RID: 1440
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(UnlicensedApplicationFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		[AllowAnonymous]
		AuthTicketResult Validate(Token token);

		// Token: 0x060005A1 RID: 1441
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(UnlicensedApplicationFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		[AllowAnonymous]
		void Logout(Token token);

		// Token: 0x060005A2 RID: 1442
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(UnlicensedApplicationFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		ChangeUserPasswordResp ChangeUserPassword(ChangeUserPasswordReq Request);

		// Token: 0x060005A3 RID: 1443
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(UnlicensedApplicationFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		UserMustChangePasswordResp UserMustChangePassword(UserMustChangePasswordReq Request);

		// Token: 0x060005A4 RID: 1444
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[FaultContract(typeof(UnlicensedApplicationFault))]
		[SoapHeader("clientDetails", typeof(ClientParametersDTO), Direction = SoapHeaderDirection.In)]
		ChangeUserPasswordByAdminResp ChangeUserPasswordByAdmin(ChangeUserPasswordByAdminReq Request);
	}
}
