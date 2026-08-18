using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000085 RID: 133
	[ServiceContract(Name = "WebSettingsService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IWebSettings : IService
	{
		// Token: 0x060003B3 RID: 947
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetInstanceNameResp GetInstanceNames(GetInstanceNameReq instanceNameReq);

		// Token: 0x060003B4 RID: 948
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetSettingsByGroupResp GetSettings(GetSettingsByGroupReq group);

		// Token: 0x060003B5 RID: 949
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetSettingResp GetSetting(GetSettingReq settingReq);

		// Token: 0x060003B6 RID: 950
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetSettingFromStringResp GetSettingFromString(GetSettingFromStringReq settingReq);

		// Token: 0x060003B7 RID: 951
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void SaveSetting(SaveSettingReq setting);

		// Token: 0x060003B8 RID: 952
		[OperationContract(Name = "ClearSettingsCacheByGroup")]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ClearSettingsCache(ClearSettingsCacheByGroupReq group);

		// Token: 0x060003B9 RID: 953
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void ClearSettingsCache(ClearSettingsCacheReq clearSettingsCacheReq);
	}
}
