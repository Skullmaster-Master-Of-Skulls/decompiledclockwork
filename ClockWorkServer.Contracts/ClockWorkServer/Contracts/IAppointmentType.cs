using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000027 RID: 39
	[ServiceContract(Name = "AppointmentTypeService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IAppointmentType : IService
	{
		// Token: 0x06000154 RID: 340
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAppointmentTypeAssociatedPerAppScreenNumsResp GetAppointmentTypeAssociatedPerAppScreenNums(GetAppointmentTypeAssociatedPerAppScreenNumsReq Request);

		// Token: 0x06000155 RID: 341
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppTypeByIdResp LoadAppTypeById(LoadAppTypeByIdReq Request);

		// Token: 0x06000156 RID: 342
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllAppTypesResp LoadAllAppTypes(LoadAllAppTypesReq Request);

		// Token: 0x06000157 RID: 343
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateAppType(UpdateAppTypeReq Request);

		// Token: 0x06000158 RID: 344
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateAppTypeResp CreateAppType(CreateAppTypeReq Request);

		// Token: 0x06000159 RID: 345
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteAppType(DeleteAppTypeReq Request);

		// Token: 0x0600015A RID: 346
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppTypeGroupByIdResp LoadAppTypeGroupById(LoadAppTypeGroupByIdReq Request);

		// Token: 0x0600015B RID: 347
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllAppTypeGroupsResp LoadAllAppTypeGroups(LoadAllAppTypeGroupsReq Request);

		// Token: 0x0600015C RID: 348
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppTypeGroupWithAppTypesByIdResp LoadAppTypeGroupWithAppTypesById(LoadAppTypeGroupWithAppTypesByIdReq Request);

		// Token: 0x0600015D RID: 349
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void DeleteAppTypeGroup(DeleteAppTypeGroupReq Request);

		// Token: 0x0600015E RID: 350
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateAppTypeGroupResp CreateAppTypeGroup(CreateAppTypeGroupReq Request);

		// Token: 0x0600015F RID: 351
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void UpdateAppTypeGroup(UpdateAppTypeGroupReq Request);

		// Token: 0x06000160 RID: 352
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetAllowedAppTypeIdsResp GetAllowedAppTypeIds(GetAllowedAppTypeIdsReq Request);

		// Token: 0x06000161 RID: 353
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllowedAppTypesResp LoadAllowedAppTypes(LoadAllowedAppTypesReq Request);

		// Token: 0x06000162 RID: 354
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllowedTestExamRelatedAppTypesResp LoadAllowedTestExamRelatedAppTypes(LoadAllowedTestExamRelatedAppTypesReq Request);

		// Token: 0x06000163 RID: 355
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllowedWorkshopRelatedAppTypesResp LoadAllowedWorkshopRelatedAppTypes(LoadAllowedWorkshopRelatedAppTypesReq Request);

		// Token: 0x06000164 RID: 356
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAppTypeWithExtendedInfoIdByIdResp LoadAppTypeWithExtendedInfoIdById(LoadAppTypeWithExtendedInfoIdByIdReq Request);

		// Token: 0x06000165 RID: 357
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateAppTypeWithExtendedInfoResp UpdateAppTypeWithExtendedInfo(UpdateAppTypeWithExtendedInfoReq Request);

		// Token: 0x06000166 RID: 358
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreateAppTypeWithExtendedInfoResp CreateAppTypeWithExtendedInfo(CreateAppTypeWithExtendedInfoReq Request);

		// Token: 0x06000167 RID: 359
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAllAppTypesWithGroupsResp LoadAllAppTypesWithGroups(LoadAllAppTypesWithGroupsReq Request);
	}
}
