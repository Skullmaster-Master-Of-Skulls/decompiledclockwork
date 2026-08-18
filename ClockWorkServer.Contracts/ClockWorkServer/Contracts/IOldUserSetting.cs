using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x0200009A RID: 154
	[ServiceContract(Name = "OldUserSettingService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IOldUserSetting : IService
	{
		// Token: 0x0600044B RID: 1099
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateUserSettings(UpdateUserSettingsReq Request);

		// Token: 0x0600044C RID: 1100
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateGroupSettings(UpdateGroupSettingsReq Request);

		// Token: 0x0600044D RID: 1101
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllUserSettingsResp LoadAllUserSettings(LoadAllUserSettingsReq Request);

		// Token: 0x0600044E RID: 1102
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SaveSettings(SaveSettingsReq Request);

		// Token: 0x0600044F RID: 1103
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPersonSettingsResp LoadPersonSettings(LoadPersonSettingsReq Request);

		// Token: 0x06000450 RID: 1104
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadGroupSettingsResp LoadGroupSettings(LoadGroupSettingsReq Request);

		// Token: 0x06000451 RID: 1105
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadEveryoneSettingsResp LoadEveryoneSettings(LoadEveryoneSettingsReq Request);

		// Token: 0x06000452 RID: 1106
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ClearCacheForUser(ClearCacheForUserReq Request);

		// Token: 0x06000453 RID: 1107
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetUserPersonalSettingValueResp GetUserPersonalSettingValue(GetUserPersonalSettingValueReq Request);

		// Token: 0x06000454 RID: 1108
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SetUserPersonalSettingValue(SetUserPersonalSettingValueReq Request);

		// Token: 0x06000455 RID: 1109
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadUserSettingReportForUserSetResp LoadUserSettingReportForUserSet(LoadUserSettingReportForUserSetReq Request);

		// Token: 0x06000456 RID: 1110
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetSettingValueStringResp GetSettingValueString(GetSettingValueStringReq Request);
	}
}
