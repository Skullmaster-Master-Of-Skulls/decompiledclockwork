using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000087 RID: 135
	internal class DynamicDataClientBaseProxy : ClientBase<IDynamicData>, IDynamicData, IService
	{
		// Token: 0x060005A0 RID: 1440 RVA: 0x0000F9DC File Offset: 0x0000DBDC
		public DynamicDataClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000F9E7 File Offset: 0x0000DBE7
		public DynamicDataClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		public LoadDataByFormResp LoadDataByForm(LoadDataByFormReq Request)
		{
			return base.Channel.LoadDataByForm(Request);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000FA14 File Offset: 0x0000DC14
		public LoadPerStudentDataForMultipleStudentsResp LoadPerStudentDataForMultipleStudents(LoadPerStudentDataForMultipleStudentsReq Req)
		{
			return base.Channel.LoadPerStudentDataForMultipleStudents(Req);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000FA34 File Offset: 0x0000DC34
		public StoreFileInDocumentsResp StoreFileInDocuments(StoreFileInDocumentsReq Request)
		{
			return base.Channel.StoreFileInDocuments(Request);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000FA54 File Offset: 0x0000DC54
		public LoadDataResp LoadData(LoadDataReq Req)
		{
			return base.Channel.LoadData(Req);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000FA74 File Offset: 0x0000DC74
		public LoadDataByFieldsResp LoadDataByFields(LoadDataByFieldsReq Req)
		{
			return base.Channel.LoadDataByFields(Req);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000FA94 File Offset: 0x0000DC94
		public LoadFileFromDocumentsResp LoadFileFromDocuments(LoadFileFromDocumentsReq Request)
		{
			return base.Channel.LoadFileFromDocuments(Request);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000FAB4 File Offset: 0x0000DCB4
		public UploadDocumentToDatabaseResp UploadDocumentToDatabase(UploadDocumentToDatabaseReq Request)
		{
			return base.Channel.UploadDocumentToDatabase(Request);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		public SaveDataResp SaveData(SaveDataReq Request)
		{
			return base.Channel.SaveData(Request);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
		public LoadEmailResp LoadEmail(LoadEmailReq Request)
		{
			return base.Channel.LoadEmail(Request);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000FB14 File Offset: 0x0000DD14
		public GetExistingPerDateEntryResp GetExistingPerDateEntry(GetExistingPerDateEntryReq Request)
		{
			return base.Channel.GetExistingPerDateEntry(Request);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000FB34 File Offset: 0x0000DD34
		public CreatePerDateEntryResp CreatePerDateEntry(CreatePerDateEntryReq Request)
		{
			return base.Channel.CreatePerDateEntry(Request);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000FB54 File Offset: 0x0000DD54
		public DoesAtLeastOneSavedDataItemExistResp DoesAtLeastOneSavedDataItemExist(DoesAtLeastOneSavedDataItemExistReq Request)
		{
			return base.Channel.DoesAtLeastOneSavedDataItemExist(Request);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000FB74 File Offset: 0x0000DD74
		public DoesAtLeastOneSavedDataItemExistByControlIdsResp DoesAtLeastOneSavedDataItemExistByControlIds(DoesAtLeastOneSavedDataItemExistByControlIdsReq Request)
		{
			return base.Channel.DoesAtLeastOneSavedDataItemExistByControlIds(Request);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000FB94 File Offset: 0x0000DD94
		public SaveDataBaseResp SaveDataBase(SaveDataBaseReq Request)
		{
			return base.Channel.SaveDataBase(Request);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000FBB2 File Offset: 0x0000DDB2
		public void AddRowToDynamicTableControl(AddRowToDynamicTableControlReq Request)
		{
			base.Channel.AddRowToDynamicTableControl(Request);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		public UpdateIconForPerAppointmentDataChangeResp UpdateIconForPerAppointmentDataChange(UpdateIconForPerAppointmentDataChangeReq Request)
		{
			return base.Channel.UpdateIconForPerAppointmentDataChange(Request);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
		public LoadPerDateEntriesResp LoadPerDateEntries(LoadPerDateEntriesReq Request)
		{
			return base.Channel.LoadPerDateEntries(Request);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000FC04 File Offset: 0x0000DE04
		public LoadPerStudentDataForMultipleStudentsAsDataTableResp LoadPerStudentDataForMultipleStudentsAsDataTable(LoadPerStudentDataForMultipleStudentsAsDataTableReq Request)
		{
			return base.Channel.LoadPerStudentDataForMultipleStudentsAsDataTable(Request);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000FC24 File Offset: 0x0000DE24
		public LoadAccommodationDataForMultipleStudentsAsDataTableResp LoadAccommodationDataForMultipleStudentsAsDataTable(LoadAccommodationDataForMultipleStudentsAsDataTableReq Request)
		{
			return base.Channel.LoadAccommodationDataForMultipleStudentsAsDataTable(Request);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000FC44 File Offset: 0x0000DE44
		public LoadFileFromImageInfoResp LoadFileFromImageInfo(LoadFileFromImageInfoReq Request)
		{
			return base.Channel.LoadFileFromImageInfo(Request);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000FC64 File Offset: 0x0000DE64
		public ChangeAssignedAdvisorBatchResp ChangeAssignedAdvisorBatch(ChangeAssignedAdvisorBatchReq Request)
		{
			return base.Channel.ChangeAssignedAdvisorBatch(Request);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000FC84 File Offset: 0x0000DE84
		public GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlResp GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlReq Request)
		{
			return base.Channel.GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(Request);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000FCA4 File Offset: 0x0000DEA4
		public LoadAssignedAdvisorsResp LoadAssignedAdvisors(LoadAssignedAdvisorsReq Request)
		{
			return base.Channel.LoadAssignedAdvisors(Request);
		}
	}
}
