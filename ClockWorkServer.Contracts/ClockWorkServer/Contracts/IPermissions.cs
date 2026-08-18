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
	// Token: 0x020000A4 RID: 164
	[ServiceContract(Name = "PermissionsService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IPermissions : IService
	{
		// Token: 0x060004D4 RID: 1236
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadUserPermissionIsAllowedSetResp LoadUserPermissionIsAllowedSet(LoadUserPermissionIsAllowedSetReq Request);

		// Token: 0x060004D5 RID: 1237
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadJustUserPermissionsResp LoadJustUserPermissions(LoadJustUserPermissionsReq Request);

		// Token: 0x060004D6 RID: 1238
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadJustGroupPermissionsResp LoadJustGroupPermissions(LoadJustGroupPermissionsReq Request);

		// Token: 0x060004D7 RID: 1239
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateJustUserOrGroupPermissions(UpdateJustUserOrGroupPermissionsReq Request);
	}
}
