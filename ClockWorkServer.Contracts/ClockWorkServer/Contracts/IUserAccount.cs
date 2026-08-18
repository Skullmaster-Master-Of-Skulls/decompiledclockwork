using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000099 RID: 153
	[ServiceContract(Name = "UserAccountService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IUserAccount : IService
	{
		// Token: 0x0600043D RID: 1085
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void RemovePassword(RemovePasswordReq Request);

		// Token: 0x0600043E RID: 1086
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreatePasswordResp CreatePassword(CreatePasswordReq Request);

		// Token: 0x0600043F RID: 1087
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdatePasswordRequireChange(UpdatePasswordRequireChangeReq Request);

		// Token: 0x06000440 RID: 1088
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdatePasswordResp UpdatePassword(UpdatePasswordReq Request);

		// Token: 0x06000441 RID: 1089
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ClearAllPasswords(ClearAllPasswordsReq Request);

		// Token: 0x06000442 RID: 1090
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPrimaryPasswordResp LoadPrimaryPassword(LoadPrimaryPasswordReq Request);

		// Token: 0x06000443 RID: 1091
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ClearPrimaryPassword(ClearPrimaryPasswordReq Request);

		// Token: 0x06000444 RID: 1092
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdatePrimaryPasswordRequireChange(UpdatePrimaryPasswordRequireChangeReq Request);

		// Token: 0x06000445 RID: 1093
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		[Obsolete("Use UpdatePrimaryPassword2 instead")]
		UpdatePrimaryPasswordResp UpdatePrimaryPassword(UpdatePrimaryPasswordReq Request);

		// Token: 0x06000446 RID: 1094
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdatePrimaryPassword2Resp UpdatePrimaryPassword2(UpdatePrimaryPassword2Req Request);

		// Token: 0x06000447 RID: 1095
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdatePrimaryPasswordExpiry(UpdatePrimaryPasswordExpiryReq Request);

		// Token: 0x06000448 RID: 1096
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ValidatePasswordAgainstPolicyResp ValidatePasswordAgainstPolicy(ValidatePasswordAgainstPolicyReq Request);

		// Token: 0x06000449 RID: 1097
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPasswordPolicyResp LoadPasswordPolicy(LoadPasswordPolicyReq Request);

		// Token: 0x0600044A RID: 1098
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdatePasswordPolicy(UpdatePasswordPolicyReq Request);
	}
}
