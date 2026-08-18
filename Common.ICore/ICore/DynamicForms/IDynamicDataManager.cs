using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.DynamicForms
{
	// Token: 0x02000098 RID: 152
	public interface IDynamicDataManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000441 RID: 1089
		Task<List<DynamicData>> LoadDataAsync(DynamicDataContext Context, int screenNum, eDynamicFormType formType);

		// Token: 0x06000442 RID: 1090
		Task<IList<DynamicDataSet>> LoadDataAsync(int PrimaryId, IList<int> SecondaryIds, IList<int> ScreenNums, eDynamicFormType ScreensType);

		// Token: 0x06000443 RID: 1091
		List<DynamicData> LoadData(DynamicDataContext Context, DynamicForm Form);

		// Token: 0x06000444 RID: 1092
		List<DynamicData> LoadData(DynamicDataContext Context, int screenNum, eDynamicFormType formType);

		// Token: 0x06000445 RID: 1093
		List<DynamicDataSet> LoadPerStudentDataForMultipleStudents(List<int> PersonIds, List<int> ControlIds);

		// Token: 0x06000446 RID: 1094
		List<DynamicData> LoadDataByFields(DynamicDataContext Context, List<int> ControlIds, eDynamicFormType DataType);

		// Token: 0x06000447 RID: 1095
		DynamicData LoadEmail(int PersonId);

		// Token: 0x06000448 RID: 1096
		IList<PersonBase> LoadUniqueStudentsWithPerStudentDataEnteredByForm(int ScreenNum);

		// Token: 0x06000449 RID: 1097
		int StoreFileInDocuments(string Title, string Notes, BinaryFile File, int StudentPersonId, int fileTypeCode = 1000);

		// Token: 0x0600044A RID: 1098
		int StoreFileInDocuments(string Title, string Notes, BinaryFile File, int StudentPersonId, int cid, int fileTypeCode = 1000);

		// Token: 0x0600044B RID: 1099
		BinaryFile LoadFileFromDocuments(int StudentPersonId, int FileId);

		// Token: 0x0600044C RID: 1100
		Task<BinaryFile> LoadFileFromDocumentsAsync(int StudentPersonId, int FileId);

		// Token: 0x0600044D RID: 1101
		int UploadDocumentToDatabase(BinaryFile File, int fileTypeCode = 1000);

		// Token: 0x0600044E RID: 1102
		Task<int> UploadDocumentToDatabaseAsync(BinaryFile File, int fileTypeCode = 1000);

		// Token: 0x0600044F RID: 1103
		void SaveData(DynamicDataContext context, List<DynamicData> data, eDynamicFormType DataType);

		// Token: 0x06000450 RID: 1104
		Task SaveDataAsync(DynamicDataContext context, List<DynamicData> data, eDynamicFormType DataType);

		// Token: 0x06000451 RID: 1105
		IList<PersonBase> LoadStudentByDataItem(eDynamicFormType FormType, DynamicField Field, object Value);

		// Token: 0x06000452 RID: 1106
		int CopyDataFromPerStudentToPerDateForm(int ScreenNumPerStudentData, int ScreenNumPerDateData);

		// Token: 0x06000453 RID: 1107
		void MergeAllData(int PersonIdNew, int PersonIdOld);

		// Token: 0x06000454 RID: 1108
		void SaveDataBase(DynamicDataContext context, List<DynamicDataBase> data, eDynamicFormType DataType);

		// Token: 0x06000455 RID: 1109
		bool DoesAtLeastOneSavedDataItemExist(DynamicDataContext context, int ScreenNum, eDynamicFormType FormType);

		// Token: 0x06000456 RID: 1110
		IList<DynamicDataSet> LoadData(int PrimaryId, IList<int> SecondaryIds, IList<int> ScreenNums, eDynamicFormType ScreensType);

		// Token: 0x06000457 RID: 1111
		int StoreFileInDocuments(string Title, string Notes, BinaryFile File, DynamicDataContext Context, eDynamicFormType DataType, int cid, int fileTypeCode = 1000);

		// Token: 0x06000458 RID: 1112
		Task<int> StoreFileInDocumentsAsync(string Title, string Notes, BinaryFile File, DynamicDataContext Context, eDynamicFormType DataType, int cid, int fileTypeCode = 1000);

		// Token: 0x06000459 RID: 1113
		void AddRowToDynamicTableControl(DynamicDataContext Context, eDynamicFormType DataType, int cid, params string[] columnValues);

		// Token: 0x0600045A RID: 1114
		IList<int> UpdateIconForPerAppointmentDataChange(int ScreenNum, int IconId, int StudentPersonId, int ControlIdToActivate);

		// Token: 0x0600045B RID: 1115
		DataTable LoadPerStudentDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds);

		// Token: 0x0600045C RID: 1116
		DataTable LoadAccommodationDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds);

		// Token: 0x0600045D RID: 1117
		IList<IDynamicDataSerializableItem> LoadDynamicDataItemsByForm(DynamicDataContext Context, int FormNum, eDynamicFormType FormType);

		// Token: 0x0600045E RID: 1118
		IList<IDynamicDataSerializableItem> LoadDynamicDataItemsByControlIds(DynamicDataContext Context, IList<int> ControlIds, eDynamicFormType FormType);

		// Token: 0x0600045F RID: 1119
		void SaveDynamicDataItems(DynamicDataContext Context, IList<IDynamicDataSerializableItem> Items, eDynamicFormType FormType);

		// Token: 0x06000460 RID: 1120
		void DeleteDataItem(DynamicDataContext context, int ControlId, eControlCode eControlCode, eDynamicFormType DataType, eDynamicDataStorageLocation location = eDynamicDataStorageLocation.Unknown);

		// Token: 0x06000461 RID: 1121
		BinaryFile LoadFileFromImageInfo(int DataId, int ControlId, string databaseTablePostFix = null);

		// Token: 0x06000462 RID: 1122
		Task<BinaryFile> LoadFileFromImageInfoAsync(int DataId, int ControlId, string databaseTablePostFix = null);

		// Token: 0x06000463 RID: 1123
		IList<Pair<PersonBase, PersonBase>> ChangeAssignedAdvisorBatch(int ControlId, int OldAssignedAdvisorPersonId, int NewAssignedAdvisorPersonId);

		// Token: 0x06000464 RID: 1124
		int GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(int cid, int pid);

		// Token: 0x06000465 RID: 1125
		IList<DynamicDataSet> LoadInstructorFormDataForMultipleExams(IList<int> examIds, IList<int> controlIds);

		// Token: 0x06000466 RID: 1126
		bool DoesAtLeastOneSavedDataItemExistByControlIds(DynamicDataContext context, IList<int> cids, eDynamicFormType FormType);

		// Token: 0x06000467 RID: 1127
		Task<IDictionary<int, DateTime?>> LoadDateTimeDynamicPerStudentDataForStudentsAsync(int[] studentPersonIds, int cid);

		// Token: 0x06000468 RID: 1128
		IDictionary<int, DateTime?> LoadDateTimeDynamicPerStudentDataForStudents(int[] studentPersonIds, int cid);

		// Token: 0x06000469 RID: 1129
		IList<BasicPerson> LoadAssignedAdvisors(eDynamicFormType formType, int studentPersonId, int[] cids);

		// Token: 0x0600046A RID: 1130
		IList<int> FindPerAppointmentExistingDataForAnyAppointment(int pid, IList<int> controlIds);

		// Token: 0x0600046B RID: 1131
		Task<List<DynamicData>> LoadDataByFieldsAsync(DynamicDataContext Context, List<int> ControlIds, eDynamicFormType DataType);
	}
}
