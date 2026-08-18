using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000086 RID: 134
	public class DynamicDataReusableClientProxy : WCFTokenBasedReusableClientProxy<IDynamicData>, IDynamicData, IService
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x0000F4BA File Offset: 0x0000D6BA
		public DynamicDataReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000F4C5 File Offset: 0x0000D6C5
		public DynamicDataReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
		public LoadFileFromDocumentsResp LoadFileFromDocuments(LoadFileFromDocumentsReq Request)
		{
			return this.WrapServiceMethod<LoadFileFromDocumentsResp>(() => this.Proxy.LoadFileFromDocuments(Request));
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000F50C File Offset: 0x0000D70C
		public UploadDocumentToDatabaseResp UploadDocumentToDatabase(UploadDocumentToDatabaseReq Request)
		{
			return this.WrapServiceMethod<UploadDocumentToDatabaseResp>(() => this.Proxy.UploadDocumentToDatabase(Request));
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000F544 File Offset: 0x0000D744
		public SaveDataResp SaveData(SaveDataReq Request)
		{
			return this.WrapServiceMethod<SaveDataResp>(() => this.Proxy.SaveData(Request));
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000F57C File Offset: 0x0000D77C
		public LoadEmailResp LoadEmail(LoadEmailReq Request)
		{
			return this.WrapServiceMethod<LoadEmailResp>(() => this.Proxy.LoadEmail(Request));
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
		public GetExistingPerDateEntryResp GetExistingPerDateEntry(GetExistingPerDateEntryReq Request)
		{
			return this.WrapServiceMethod<GetExistingPerDateEntryResp>(() => this.Proxy.GetExistingPerDateEntry(Request));
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000F5EC File Offset: 0x0000D7EC
		public CreatePerDateEntryResp CreatePerDateEntry(CreatePerDateEntryReq Request)
		{
			return this.WrapServiceMethod<CreatePerDateEntryResp>(() => this.Proxy.CreatePerDateEntry(Request));
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000F624 File Offset: 0x0000D824
		public LoadDataResp LoadData(LoadDataReq Req)
		{
			return this.WrapServiceMethod<LoadDataResp>(() => this.Proxy.LoadData(Req));
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000F65C File Offset: 0x0000D85C
		public LoadDataByFormResp LoadDataByForm(LoadDataByFormReq Request)
		{
			return this.WrapServiceMethod<LoadDataByFormResp>(() => this.Proxy.LoadDataByForm(Request));
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000F694 File Offset: 0x0000D894
		public LoadPerStudentDataForMultipleStudentsResp LoadPerStudentDataForMultipleStudents(LoadPerStudentDataForMultipleStudentsReq Req)
		{
			return this.WrapServiceMethod<LoadPerStudentDataForMultipleStudentsResp>(() => this.Proxy.LoadPerStudentDataForMultipleStudents(Req));
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public StoreFileInDocumentsResp StoreFileInDocuments(StoreFileInDocumentsReq Request)
		{
			return this.WrapServiceMethod<StoreFileInDocumentsResp>(() => this.Proxy.StoreFileInDocuments(Request));
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000F704 File Offset: 0x0000D904
		public LoadDataByFieldsResp LoadDataByFields(LoadDataByFieldsReq Req)
		{
			return this.WrapServiceMethod<LoadDataByFieldsResp>(() => this.Proxy.LoadDataByFields(Req));
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000F73C File Offset: 0x0000D93C
		public DoesAtLeastOneSavedDataItemExistResp DoesAtLeastOneSavedDataItemExist(DoesAtLeastOneSavedDataItemExistReq Request)
		{
			return this.WrapServiceMethod<DoesAtLeastOneSavedDataItemExistResp>(() => this.Proxy.DoesAtLeastOneSavedDataItemExist(Request));
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000F774 File Offset: 0x0000D974
		public DoesAtLeastOneSavedDataItemExistByControlIdsResp DoesAtLeastOneSavedDataItemExistByControlIds(DoesAtLeastOneSavedDataItemExistByControlIdsReq Request)
		{
			return this.WrapServiceMethod<DoesAtLeastOneSavedDataItemExistByControlIdsResp>(() => this.Proxy.DoesAtLeastOneSavedDataItemExistByControlIds(Request));
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000F7AC File Offset: 0x0000D9AC
		public SaveDataBaseResp SaveDataBase(SaveDataBaseReq Request)
		{
			return this.WrapServiceMethod<SaveDataBaseResp>(() => this.Proxy.SaveDataBase(Request));
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000F7E4 File Offset: 0x0000D9E4
		public void AddRowToDynamicTableControl(AddRowToDynamicTableControlReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.AddRowToDynamicTableControl(Request);
			});
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000F81C File Offset: 0x0000DA1C
		public UpdateIconForPerAppointmentDataChangeResp UpdateIconForPerAppointmentDataChange(UpdateIconForPerAppointmentDataChangeReq Request)
		{
			return this.WrapServiceMethod<UpdateIconForPerAppointmentDataChangeResp>(() => this.Proxy.UpdateIconForPerAppointmentDataChange(Request));
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000F854 File Offset: 0x0000DA54
		public LoadPerDateEntriesResp LoadPerDateEntries(LoadPerDateEntriesReq Request)
		{
			return this.WrapServiceMethod<LoadPerDateEntriesResp>(() => this.Proxy.LoadPerDateEntries(Request));
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000F88C File Offset: 0x0000DA8C
		public LoadPerStudentDataForMultipleStudentsAsDataTableResp LoadPerStudentDataForMultipleStudentsAsDataTable(LoadPerStudentDataForMultipleStudentsAsDataTableReq Request)
		{
			return this.WrapServiceMethod<LoadPerStudentDataForMultipleStudentsAsDataTableResp>(() => this.Proxy.LoadPerStudentDataForMultipleStudentsAsDataTable(Request));
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
		public LoadAccommodationDataForMultipleStudentsAsDataTableResp LoadAccommodationDataForMultipleStudentsAsDataTable(LoadAccommodationDataForMultipleStudentsAsDataTableReq Request)
		{
			return this.WrapServiceMethod<LoadAccommodationDataForMultipleStudentsAsDataTableResp>(() => this.Proxy.LoadAccommodationDataForMultipleStudentsAsDataTable(Request));
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000F8FC File Offset: 0x0000DAFC
		public LoadFileFromImageInfoResp LoadFileFromImageInfo(LoadFileFromImageInfoReq Request)
		{
			return this.WrapServiceMethod<LoadFileFromImageInfoResp>(() => this.Proxy.LoadFileFromImageInfo(Request));
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000F934 File Offset: 0x0000DB34
		public ChangeAssignedAdvisorBatchResp ChangeAssignedAdvisorBatch(ChangeAssignedAdvisorBatchReq Request)
		{
			return this.WrapServiceMethod<ChangeAssignedAdvisorBatchResp>(() => this.Proxy.ChangeAssignedAdvisorBatch(Request));
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000F96C File Offset: 0x0000DB6C
		public GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlResp GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlReq Request)
		{
			return this.WrapServiceMethod<GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlResp>(() => this.Proxy.GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(Request));
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
		public LoadAssignedAdvisorsResp LoadAssignedAdvisors(LoadAssignedAdvisorsReq Request)
		{
			return this.WrapServiceMethod<LoadAssignedAdvisorsResp>(() => this.Proxy.LoadAssignedAdvisors(Request));
		}
	}
}
