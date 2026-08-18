using System;
using System.ServiceModel;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.Faults;
using TechnoPro.Common.Public;
using TechnoPro.Common.WCF.Faults;
using WCFExtrasPlus.Soap;
using WCFExtrasPlus.Wsdl.Documentation;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x02000043 RID: 67
	[ServiceContract(Name = "DynamicDataService", Namespace = "http://tpro.ca")]
	[SoapHeaders]
	[XmlComments]
	public interface IDynamicData : IService
	{
		// Token: 0x06000206 RID: 518
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadEmailResp LoadEmail(LoadEmailReq Request);

		// Token: 0x06000207 RID: 519
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		StoreFileInDocumentsResp StoreFileInDocuments(StoreFileInDocumentsReq Request);

		// Token: 0x06000208 RID: 520
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDataByFieldsResp LoadDataByFields(LoadDataByFieldsReq Request);

		// Token: 0x06000209 RID: 521
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDataResp LoadData(LoadDataReq Request);

		// Token: 0x0600020A RID: 522
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadDataByFormResp LoadDataByForm(LoadDataByFormReq Request);

		// Token: 0x0600020B RID: 523
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPerStudentDataForMultipleStudentsResp LoadPerStudentDataForMultipleStudents(LoadPerStudentDataForMultipleStudentsReq Request);

		// Token: 0x0600020C RID: 524
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFileFromDocumentsResp LoadFileFromDocuments(LoadFileFromDocumentsReq Request);

		// Token: 0x0600020D RID: 525
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UploadDocumentToDatabaseResp UploadDocumentToDatabase(UploadDocumentToDatabaseReq Request);

		// Token: 0x0600020E RID: 526
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SaveDataResp SaveData(SaveDataReq Request);

		// Token: 0x0600020F RID: 527
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetExistingPerDateEntryResp GetExistingPerDateEntry(GetExistingPerDateEntryReq Request);

		// Token: 0x06000210 RID: 528
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		CreatePerDateEntryResp CreatePerDateEntry(CreatePerDateEntryReq Request);

		// Token: 0x06000211 RID: 529
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		SaveDataBaseResp SaveDataBase(SaveDataBaseReq Request);

		// Token: 0x06000212 RID: 530
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DoesAtLeastOneSavedDataItemExistResp DoesAtLeastOneSavedDataItemExist(DoesAtLeastOneSavedDataItemExistReq Request);

		// Token: 0x06000213 RID: 531
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		DoesAtLeastOneSavedDataItemExistByControlIdsResp DoesAtLeastOneSavedDataItemExistByControlIds(DoesAtLeastOneSavedDataItemExistByControlIdsReq Request);

		// Token: 0x06000214 RID: 532
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		void AddRowToDynamicTableControl(AddRowToDynamicTableControlReq Request);

		// Token: 0x06000215 RID: 533
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		UpdateIconForPerAppointmentDataChangeResp UpdateIconForPerAppointmentDataChange(UpdateIconForPerAppointmentDataChangeReq Request);

		// Token: 0x06000216 RID: 534
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPerDateEntriesResp LoadPerDateEntries(LoadPerDateEntriesReq Request);

		// Token: 0x06000217 RID: 535
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadPerStudentDataForMultipleStudentsAsDataTableResp LoadPerStudentDataForMultipleStudentsAsDataTable(LoadPerStudentDataForMultipleStudentsAsDataTableReq Request);

		// Token: 0x06000218 RID: 536
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAccommodationDataForMultipleStudentsAsDataTableResp LoadAccommodationDataForMultipleStudentsAsDataTable(LoadAccommodationDataForMultipleStudentsAsDataTableReq Request);

		// Token: 0x06000219 RID: 537
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadFileFromImageInfoResp LoadFileFromImageInfo(LoadFileFromImageInfoReq Request);

		// Token: 0x0600021A RID: 538
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		ChangeAssignedAdvisorBatchResp ChangeAssignedAdvisorBatch(ChangeAssignedAdvisorBatchReq Request);

		// Token: 0x0600021B RID: 539
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlResp GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlReq Request);

		// Token: 0x0600021C RID: 540
		[OperationContract]
		[FaultContract(typeof(HeaderNullFault))]
		[FaultContract(typeof(InvalidSessionIdentifierFault))]
		[SoapHeader("operationDetails", typeof(OperationData), Direction = SoapHeaderDirection.In)]
		LoadAssignedAdvisorsResp LoadAssignedAdvisors(LoadAssignedAdvisorsReq Request);
	}
}
