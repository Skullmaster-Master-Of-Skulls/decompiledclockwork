using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.DynamicForms
{
	// Token: 0x02000080 RID: 128
	public interface IDynamicDataDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000320 RID: 800
		Task<List<DynamicData>> LoadDataAsync(DynamicDataContext Context, int FormNum, eDynamicFormType FormType);

		// Token: 0x06000321 RID: 801
		List<DynamicData> LoadData(DynamicDataContext Context, DynamicForm Form);

		// Token: 0x06000322 RID: 802
		Task<IList<DynamicDataSet>> LoadDataAsync(int PrimaryId, IList<int> SecondaryIds, IList<int> ScreenNums, eDynamicFormType ScreensType);

		// Token: 0x06000323 RID: 803
		int UploadDocumentToDatabase(BinaryFile File, int fileTypeCode = 1000);

		// Token: 0x06000324 RID: 804
		Task<int> UploadDocumentToDatabaseAsync(BinaryFile File, int fileTypeCode = 1000);

		// Token: 0x06000325 RID: 805
		BinaryFile LoadFileFromDocuments(int StudentPersonId, int FileId);

		// Token: 0x06000326 RID: 806
		Task<BinaryFile> LoadFileFromDocumentsAsync(int StudentPersonId, int FileId);

		// Token: 0x06000327 RID: 807
		void SaveData(DynamicDataContext context, List<DynamicData> data, eDynamicFormType DataType);

		// Token: 0x06000328 RID: 808
		List<DynamicData> LoadDataByFields(DynamicDataContext Context, List<int> ControlIds, eDynamicFormType DataType);

		// Token: 0x06000329 RID: 809
		Task<List<DynamicData>> LoadDataByFieldsAsync(DynamicDataContext Context, List<int> ControlIds, eDynamicFormType DataType);

		// Token: 0x0600032A RID: 810
		List<DynamicDataSet> LoadPerStudentDataForMultipleStudents(List<int> PersonIds, List<int> ControlIds);

		// Token: 0x0600032B RID: 811
		IList<PersonBase> LoadUniqueStudentsWithPerStudentDataEnteredByForm(int ScreenNum);

		// Token: 0x0600032C RID: 812
		IList<PersonBase> LoadStudentByDataItem(eDynamicFormType FormType, DynamicField Field, object Value);

		// Token: 0x0600032D RID: 813
		void CopyAllFormDataFromPerStudentToPerDate(int StudentPersonId, int ScreenNumPerStudent, int PerDateAppointmentId);

		// Token: 0x0600032E RID: 814
		void MergeAllData(int PersonIdNew, int PersonIdOld);

		// Token: 0x0600032F RID: 815
		bool DoesAtLeastOneSavedDataItemExist(DynamicDataContext context, int ScreenNum, eDynamicFormType FormType);

		// Token: 0x06000330 RID: 816
		bool DoesAtLeastOneSavedDataItemExist(DynamicDataContext context, IList<int> ControlIds, eDynamicFormType FormType);

		// Token: 0x06000331 RID: 817
		IList<DynamicDataSet> LoadData(int PrimaryId, IList<int> SecondaryIds, IList<int> ScreenNums, eDynamicFormType ScreensType);

		// Token: 0x06000332 RID: 818
		IList<int> UpdateIconForPerAppointmentDataChange(int ScreenNum, int IconId, int StudentPersonId, int ControlIdToActivate);

		// Token: 0x06000333 RID: 819
		IList<DynamicDataStorageItem> LoadDynamicDataStorageItemsByForm(DynamicDataContext Context, int FormNum, eDynamicFormType FormType);

		// Token: 0x06000334 RID: 820
		IList<DynamicDataStorageItem> LoadDynamicDataItemsByControlIds(DynamicDataContext Context, IList<int> ControlIds, eDynamicFormType FormType);

		// Token: 0x06000335 RID: 821
		void SaveDynamicDataStorageItems(DynamicDataContext Context, IList<DynamicDataStorageItem> StorageItems, eDynamicFormType FormType);

		// Token: 0x06000336 RID: 822
		void DeleteDataItem(DynamicDataContext context, int ControlId, eControlCode eControlCode, eDynamicFormType DataType, eDynamicDataStorageLocation location = eDynamicDataStorageLocation.Unknown);

		// Token: 0x06000337 RID: 823
		string GetDynamicDataSelectQuery(eDynamicFormType dataType);

		// Token: 0x06000338 RID: 824
		BinaryFile LoadFileFromImageInfo(string imageInfoTableName, int dataId);

		// Token: 0x06000339 RID: 825
		Task<BinaryFile> LoadFileFromImageInfoAsync(string imageInfoTableName, int dataId);

		// Token: 0x0600033A RID: 826
		IDictionary<int, int[]> LoadAllPersonIdsAndControlIdsWithDataForPerStudentData(params int[] ControlIds);

		// Token: 0x0600033B RID: 827
		IDictionary<int, int[]> LoadAllPersonIdsAndControlIdsWithDataForTemplateOnlyAccommodations(params int[] ControlIds);

		// Token: 0x0600033C RID: 828
		IList<Pair<PersonBase, PersonBase>> SwapAssignedAdvisors(int ControlId, int OldAdvisorPid, int NewAdvisorPid);

		// Token: 0x0600033D RID: 829
		List<DynamicDataSet> LoadPerCaseDataForMultipleStudents(List<int> PersonIds, List<int> ControlIds);

		// Token: 0x0600033E RID: 830
		int GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(int cid, int pid);

		// Token: 0x0600033F RID: 831
		IList<DynamicDataSet> LoadInstructorFormDataForMultipleExams(IList<int> examIds, IList<int> controlIds);

		// Token: 0x06000340 RID: 832
		IDictionary<int, DateTime?> LoadDateTimeDynamicPerStudentDataForStudents(int[] studentPersonIds, int cid);

		// Token: 0x06000341 RID: 833
		Task<IDictionary<int, DateTime?>> LoadDateTimeDynamicPerStudentDataForStudentsAsync(int[] studentPersonIds, int cid);

		// Token: 0x06000342 RID: 834
		List<DynamicData> LoadData(DynamicDataContext Context, int FormNum, eDynamicFormType FormType);

		// Token: 0x06000343 RID: 835
		IList<BasicPerson> LoadAssignedAdvisorsFromPerStudentForm(int studentPersonId, int[] cids);

		// Token: 0x06000344 RID: 836
		IList<int> FindPerAppointmentExistingDataForAnyAppointment(int pid, IList<int> controlIds);

		// Token: 0x06000345 RID: 837
		Task DeleteDataItemAsync(DynamicDataContext context, int ControlId, eControlCode eControlCode, eDynamicFormType DataType, eDynamicDataStorageLocation location = eDynamicDataStorageLocation.Unknown);

		// Token: 0x06000346 RID: 838
		Task SaveDataAsync(DynamicDataContext context, List<DynamicData> data, eDynamicFormType DataType);
	}
}
