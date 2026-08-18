using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.DynamicForms
{
	// Token: 0x02000064 RID: 100
	public class DynamicDataClientManager : IDynamicDataClientManager, IWebService
	{
		// Token: 0x06000391 RID: 913 RVA: 0x0000FE98 File Offset: 0x0000E098
		public DynamicDataDTO LoadEmail(int PersonId)
		{
			LoadEmailReq loadEmailReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadEmailReq>();
			loadEmailReq.PersonId = PersonId;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().LoadEmail(loadEmailReq).EmailData;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000FED0 File Offset: 0x0000E0D0
		public int StoreFileInDocuments(int StudentPersonId, string Title, string Notes, BinaryFileDTO File)
		{
			StoreFileInDocumentsReq storeFileInDocumentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<StoreFileInDocumentsReq>();
			storeFileInDocumentsReq.StudentPersonId = StudentPersonId;
			storeFileInDocumentsReq.Title = (Title ?? "");
			storeFileInDocumentsReq.Notes = (Notes ?? "");
			storeFileInDocumentsReq.File = File;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().StoreFileInDocuments(storeFileInDocumentsReq).FileId;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000FF30 File Offset: 0x0000E130
		public IList<DynamicDataDTO> LoadDataByFields(DynamicDataContextDTO Context, IList<int> ControlIds, eDynamicFormTypeDTO DataType)
		{
			LoadDataByFieldsReq loadDataByFieldsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDataByFieldsReq>();
			loadDataByFieldsReq.Context = Context;
			loadDataByFieldsReq.ControlIds = ControlIds.ToList<int>();
			loadDataByFieldsReq.DataType = DataType;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().LoadDataByFields(loadDataByFieldsReq).Data;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000FF7C File Offset: 0x0000E17C
		public IList<DynamicDataDTO> LoadData(DynamicDataContextDTO Context, DynamicFormDTO Form)
		{
			LoadDataReq loadDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDataReq>();
			loadDataReq.Context = Context;
			loadDataReq.Form = Form;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().LoadData(loadDataReq).Data;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000FFBC File Offset: 0x0000E1BC
		public IList<DynamicDataDTO> LoadData(DynamicDataContextDTO Context, int screenNum, eDynamicFormTypeDTO formType)
		{
			LoadDataByFormReq loadDataByFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDataByFormReq>();
			loadDataByFormReq.Context = Context;
			loadDataByFormReq.ScreenNum = screenNum;
			loadDataByFormReq.FormType = formType;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().LoadDataByForm(loadDataByFormReq).Data;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00010004 File Offset: 0x0000E204
		public IList<DynamicDataSetDTO> LoadPerStudentDataForMultipleStudents(IList<int> PersonIds, IList<int> ControlIds)
		{
			LoadPerStudentDataForMultipleStudentsReq loadPerStudentDataForMultipleStudentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPerStudentDataForMultipleStudentsReq>();
			loadPerStudentDataForMultipleStudentsReq.PersonIds = PersonIds.ToList<int>();
			loadPerStudentDataForMultipleStudentsReq.ControlIds = ControlIds.ToList<int>();
			return ClientServiceFactory.GetClientInstance<IDynamicData>().LoadPerStudentDataForMultipleStudents(loadPerStudentDataForMultipleStudentsReq).Data;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001004C File Offset: 0x0000E24C
		public BinaryFileDTO LoadFileFromDocuments(int StudentPersonId, int FileId)
		{
			LoadFileFromDocumentsReq loadFileFromDocumentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFileFromDocumentsReq>();
			loadFileFromDocumentsReq.StudentPersonId = StudentPersonId;
			loadFileFromDocumentsReq.FileId = FileId;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().LoadFileFromDocuments(loadFileFromDocumentsReq).File;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001008C File Offset: 0x0000E28C
		public int UploadDocumentToDatabase(BinaryFileDTO File)
		{
			UploadDocumentToDatabaseReq uploadDocumentToDatabaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UploadDocumentToDatabaseReq>();
			uploadDocumentToDatabaseReq.File = File;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().UploadDocumentToDatabase(uploadDocumentToDatabaseReq).FileId;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000100C4 File Offset: 0x0000E2C4
		public void SaveData(DynamicDataContextDTO Context, IList<DynamicDataDTO> Data, eDynamicFormTypeDTO FormType)
		{
			SaveDataReq saveDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveDataReq>();
			saveDataReq.Context = Context;
			saveDataReq.Data = Data.ToList<DynamicDataDTO>();
			saveDataReq.FormType = FormType;
			ClientServiceFactory.GetClientInstance<IDynamicData>().SaveData(saveDataReq);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00010108 File Offset: 0x0000E308
		public PerDateEntryDTO GetExistingPerDateEntry(int StudentPersonId, int ScreenNum, SessionDTO Session)
		{
			GetExistingPerDateEntryReq getExistingPerDateEntryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetExistingPerDateEntryReq>();
			getExistingPerDateEntryReq.StudentPersonId = StudentPersonId;
			getExistingPerDateEntryReq.ScreenNum = ScreenNum;
			getExistingPerDateEntryReq.Session = Session;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().GetExistingPerDateEntry(getExistingPerDateEntryReq).PerDateEntry;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00010150 File Offset: 0x0000E350
		public int CreatePerDateEntry(PerDateEntryDTO PerDateEntry)
		{
			CreatePerDateEntryReq createPerDateEntryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreatePerDateEntryReq>();
			createPerDateEntryReq.PerDateEntry = PerDateEntry;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().CreatePerDateEntry(createPerDateEntryReq).AppointmentId;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00010188 File Offset: 0x0000E388
		public bool DoesAtLeastOneSavedDataItemExist(DynamicDataContextDTO Context, eDynamicFormTypeDTO FormType, int ScreenNum)
		{
			DoesAtLeastOneSavedDataItemExistReq doesAtLeastOneSavedDataItemExistReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DoesAtLeastOneSavedDataItemExistReq>();
			doesAtLeastOneSavedDataItemExistReq.Context = Context;
			doesAtLeastOneSavedDataItemExistReq.FormType = FormType;
			doesAtLeastOneSavedDataItemExistReq.ScreenNum = ScreenNum;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().DoesAtLeastOneSavedDataItemExist(doesAtLeastOneSavedDataItemExistReq).AtLeastOneDataItemExists;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000101D0 File Offset: 0x0000E3D0
		public bool DoesAtLeastOneSavedDataItemExistByControlIds(DynamicDataContextDTO Context, eDynamicFormTypeDTO FormType, IList<int> controlIds)
		{
			DoesAtLeastOneSavedDataItemExistByControlIdsReq doesAtLeastOneSavedDataItemExistByControlIdsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DoesAtLeastOneSavedDataItemExistByControlIdsReq>();
			doesAtLeastOneSavedDataItemExistByControlIdsReq.Context = Context;
			doesAtLeastOneSavedDataItemExistByControlIdsReq.FormType = FormType;
			doesAtLeastOneSavedDataItemExistByControlIdsReq.ControlIds = controlIds;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().DoesAtLeastOneSavedDataItemExistByControlIds(doesAtLeastOneSavedDataItemExistByControlIdsReq).AtLeastOneDataItemExists;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00010218 File Offset: 0x0000E418
		public void SaveDataBase(DynamicDataContextDTO Context, List<DynamicDataBaseDTO> Data, eDynamicFormTypeDTO FormType)
		{
			SaveDataBaseReq saveDataBaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveDataBaseReq>();
			saveDataBaseReq.Context = Context;
			saveDataBaseReq.Data = Data;
			saveDataBaseReq.FormType = FormType;
			ClientServiceFactory.GetClientInstance<IDynamicData>().SaveDataBase(saveDataBaseReq);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00010258 File Offset: 0x0000E458
		public void AddRowToDynamicTableControl(DynamicDataContextDTO Context, eDynamicFormTypeDTO FormType, int ControlId, params string[] columnValues)
		{
			AddRowToDynamicTableControlReq addRowToDynamicTableControlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddRowToDynamicTableControlReq>();
			addRowToDynamicTableControlReq.Context = Context;
			addRowToDynamicTableControlReq.FormType = FormType;
			addRowToDynamicTableControlReq.ControlId = ControlId;
			addRowToDynamicTableControlReq.ColumnValues = columnValues;
			ClientServiceFactory.GetClientInstance<IDynamicData>().AddRowToDynamicTableControl(addRowToDynamicTableControlReq);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000102A0 File Offset: 0x0000E4A0
		public int StoreFileInDocuments(DynamicDataContextDTO Context, eDynamicFormTypeDTO FormType, int ControlId, string Title, string Notes, BinaryFileDTO File)
		{
			StoreFileInDocumentsReq storeFileInDocumentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<StoreFileInDocumentsReq>();
			storeFileInDocumentsReq.DataContext = Context;
			storeFileInDocumentsReq.FormType = FormType;
			storeFileInDocumentsReq.ControlId = ControlId;
			storeFileInDocumentsReq.Title = Title;
			storeFileInDocumentsReq.Notes = Notes;
			storeFileInDocumentsReq.File = File;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().StoreFileInDocuments(storeFileInDocumentsReq).FileId;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00010300 File Offset: 0x0000E500
		public IList<int> UpdateIconForPerAppointmentDataChange(int ScreenNum, int IconId, int StudentPersonId, int ControlIdToActivate)
		{
			UpdateIconForPerAppointmentDataChangeReq updateIconForPerAppointmentDataChangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateIconForPerAppointmentDataChangeReq>();
			updateIconForPerAppointmentDataChangeReq.ScreenNum = ScreenNum;
			updateIconForPerAppointmentDataChangeReq.IconId = IconId;
			updateIconForPerAppointmentDataChangeReq.StudentPersonId = StudentPersonId;
			updateIconForPerAppointmentDataChangeReq.ControlIdToActivate = ControlIdToActivate;
			IList<int> appIds = ClientServiceFactory.GetClientInstance<IDynamicData>().UpdateIconForPerAppointmentDataChange(updateIconForPerAppointmentDataChangeReq).AppointmentIds;
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			Task.Run(delegate()
			{
				foreach (int appointmentId in appIds)
				{
					AppointmentDTO appointmentDTO = appointmentClientManager.LoadAppointment(appointmentId);
					bool flag = appointmentDTO != null;
					if (flag)
					{
						AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(appointmentDTO).GetAwaiter().GetResult();
					}
				}
			});
			return appIds;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00010380 File Offset: 0x0000E580
		public IList<PerDateEntryDTO> LoadPerDateEntries(int StudentPersonId, int ScreenNum)
		{
			LoadPerDateEntriesReq loadPerDateEntriesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPerDateEntriesReq>();
			loadPerDateEntriesReq.StudentPersonId = StudentPersonId;
			loadPerDateEntriesReq.ScreenNum = ScreenNum;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().LoadPerDateEntries(loadPerDateEntriesReq).PerDateEntries;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000103C0 File Offset: 0x0000E5C0
		public DataTable LoadPerStudentDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds)
		{
			LoadPerStudentDataForMultipleStudentsAsDataTableReq loadPerStudentDataForMultipleStudentsAsDataTableReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPerStudentDataForMultipleStudentsAsDataTableReq>();
			loadPerStudentDataForMultipleStudentsAsDataTableReq.PersonIds = PersonIds;
			loadPerStudentDataForMultipleStudentsAsDataTableReq.ControlIds = ControlIds;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().LoadPerStudentDataForMultipleStudentsAsDataTable(loadPerStudentDataForMultipleStudentsAsDataTableReq).Table;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00010400 File Offset: 0x0000E600
		public DataTable LoadAccommodationDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds)
		{
			LoadAccommodationDataForMultipleStudentsAsDataTableReq loadAccommodationDataForMultipleStudentsAsDataTableReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAccommodationDataForMultipleStudentsAsDataTableReq>();
			loadAccommodationDataForMultipleStudentsAsDataTableReq.PersonIds = PersonIds;
			loadAccommodationDataForMultipleStudentsAsDataTableReq.ControlIds = ControlIds;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().LoadAccommodationDataForMultipleStudentsAsDataTable(loadAccommodationDataForMultipleStudentsAsDataTableReq).Table;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00010440 File Offset: 0x0000E640
		public BinaryFileDTO LoadFileFromImageInfo(int DataId, int ControlId, string databaseTablePostFix = null)
		{
			LoadFileFromImageInfoReq loadFileFromImageInfoReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadFileFromImageInfoReq>();
			loadFileFromImageInfoReq.DataId = DataId;
			loadFileFromImageInfoReq.ControlId = ControlId;
			loadFileFromImageInfoReq.DatabaseTablePostFix = databaseTablePostFix;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().LoadFileFromImageInfo(loadFileFromImageInfoReq).File;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00010488 File Offset: 0x0000E688
		public int GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(int cid, int pid)
		{
			GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlReq getNumberOfStudentsStaffIsAssignedToInStaffDropListControlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlReq>();
			getNumberOfStudentsStaffIsAssignedToInStaffDropListControlReq.PersonId = pid;
			getNumberOfStudentsStaffIsAssignedToInStaffDropListControlReq.ControlId = cid;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(getNumberOfStudentsStaffIsAssignedToInStaffDropListControlReq).NumberOfStudents;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000104C8 File Offset: 0x0000E6C8
		public void UpdateStudentUsername(int PersonId, string Username)
		{
			bool flag = Username == null || Username.Trim().Length < 1;
			if (!flag)
			{
				DynamicFieldDTO dynamicFieldDTO = null;
				IDynamicFieldClientManager dynamicFieldClientManager = new DynamicFieldClientManager();
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				int settingValue = webSettingsClientManager.GetSettingValue<int>(Setting.LOGIN_StudentUsernameControlId);
				bool flag2 = settingValue > 0;
				if (flag2)
				{
					dynamicFieldDTO = dynamicFieldClientManager.LoadFieldByControlId(settingValue);
				}
				bool flag3 = dynamicFieldDTO != null;
				if (flag3)
				{
					string settingValue2 = webSettingsClientManager.GetSettingValue<string>(Setting.LOGIN_AuthorizationContext);
					AuthorizationContext authorizationContextFromXml = settingValue2.GetAuthorizationContextFromXml();
					bool flag4 = ((authorizationContextFromXml != null) ? authorizationContextFromXml.ContextItems : null) != null && authorizationContextFromXml.ContextItems.Count > 0;
					if (flag4)
					{
						AuthorizationContextItem authorizationContextItem = authorizationContextFromXml.ContextItems.FirstOrDefault((AuthorizationContextItem g) => !g.IsDisabled && g.ContextItemType == eAuthorizationContextItemType.Student && g.LookupMethod == eLookupMethod.ByUsername && g.LookupMethodCid > 0);
						bool flag5 = authorizationContextItem != null;
						if (flag5)
						{
							dynamicFieldDTO = dynamicFieldClientManager.LoadFieldByControlId(authorizationContextItem.LookupMethodCid);
						}
					}
				}
				bool flag6 = dynamicFieldDTO == null;
				if (flag6)
				{
					dynamicFieldDTO = dynamicFieldClientManager.LoadFieldByName("username");
				}
				bool flag7 = dynamicFieldDTO != null;
				if (flag7)
				{
					this.SaveData(new DynamicDataContextDTO
					{
						PrimaryId = PersonId
					}, new List<DynamicDataDTO>
					{
						new DynamicDataDTO
						{
							Field = dynamicFieldDTO,
							Value = Username
						}
					}, eDynamicFormTypeDTO.PerStudent);
				}
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00010610 File Offset: 0x0000E810
		public IList<BasicPersonDTO> LoadAssignedAdvisors(eDynamicFormType formType, int studentPersonId, int[] cids)
		{
			LoadAssignedAdvisorsReq loadAssignedAdvisorsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAssignedAdvisorsReq>();
			loadAssignedAdvisorsReq.FormType = formType;
			loadAssignedAdvisorsReq.StudentPersonId = studentPersonId;
			loadAssignedAdvisorsReq.AssignedAdvisorControlIds = cids;
			return ClientServiceFactory.GetClientInstance<IDynamicData>().LoadAssignedAdvisors(loadAssignedAdvisorsReq).AssignedAdvisors;
		}
	}
}
