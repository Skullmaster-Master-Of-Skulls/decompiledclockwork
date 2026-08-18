using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.ClientManager.ICore.DynamicForms
{
	// Token: 0x0200005D RID: 93
	public interface IDynamicDataClientManager : IWebService
	{
		// Token: 0x060002B8 RID: 696
		DynamicDataDTO LoadEmail(int PersonId);

		// Token: 0x060002B9 RID: 697
		int StoreFileInDocuments(int StudentPersonId, string Title, string Notes, BinaryFileDTO File);

		// Token: 0x060002BA RID: 698
		IList<DynamicDataDTO> LoadDataByFields(DynamicDataContextDTO Context, IList<int> ControlIds, eDynamicFormTypeDTO DataType);

		// Token: 0x060002BB RID: 699
		IList<DynamicDataDTO> LoadData(DynamicDataContextDTO Context, DynamicFormDTO Form);

		// Token: 0x060002BC RID: 700
		IList<DynamicDataDTO> LoadData(DynamicDataContextDTO Context, int screenNum, eDynamicFormTypeDTO formType);

		// Token: 0x060002BD RID: 701
		IList<DynamicDataSetDTO> LoadPerStudentDataForMultipleStudents(IList<int> PersonIds, IList<int> ControlIds);

		// Token: 0x060002BE RID: 702
		BinaryFileDTO LoadFileFromDocuments(int StudentPersonId, int FileId);

		// Token: 0x060002BF RID: 703
		int UploadDocumentToDatabase(BinaryFileDTO File);

		// Token: 0x060002C0 RID: 704
		void SaveData(DynamicDataContextDTO Context, IList<DynamicDataDTO> Data, eDynamicFormTypeDTO FormType);

		// Token: 0x060002C1 RID: 705
		PerDateEntryDTO GetExistingPerDateEntry(int StudentPersonId, int ScreenNum, SessionDTO Session);

		// Token: 0x060002C2 RID: 706
		int CreatePerDateEntry(PerDateEntryDTO PerDateEntry);

		// Token: 0x060002C3 RID: 707
		bool DoesAtLeastOneSavedDataItemExist(DynamicDataContextDTO Context, eDynamicFormTypeDTO FormType, int ScreenNum);

		// Token: 0x060002C4 RID: 708
		bool DoesAtLeastOneSavedDataItemExistByControlIds(DynamicDataContextDTO Context, eDynamicFormTypeDTO FormType, IList<int> controlIds);

		// Token: 0x060002C5 RID: 709
		void SaveDataBase(DynamicDataContextDTO Context, List<DynamicDataBaseDTO> Data, eDynamicFormTypeDTO FormType);

		// Token: 0x060002C6 RID: 710
		void AddRowToDynamicTableControl(DynamicDataContextDTO Context, eDynamicFormTypeDTO FormType, int ControlId, params string[] columnValues);

		// Token: 0x060002C7 RID: 711
		int StoreFileInDocuments(DynamicDataContextDTO Context, eDynamicFormTypeDTO FormType, int ControlId, string Title, string Notes, BinaryFileDTO File);

		// Token: 0x060002C8 RID: 712
		IList<int> UpdateIconForPerAppointmentDataChange(int ScreenNum, int IconId, int StudentPersonId, int ControlIdToActivate);

		// Token: 0x060002C9 RID: 713
		IList<PerDateEntryDTO> LoadPerDateEntries(int StudentPersonId, int ScreenNum);

		// Token: 0x060002CA RID: 714
		DataTable LoadPerStudentDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds);

		// Token: 0x060002CB RID: 715
		DataTable LoadAccommodationDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds);

		// Token: 0x060002CC RID: 716
		BinaryFileDTO LoadFileFromImageInfo(int DataId, int ControlId, string databaseTablePostFix = null);

		// Token: 0x060002CD RID: 717
		int GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(int cid, int pid);

		// Token: 0x060002CE RID: 718
		IList<BasicPersonDTO> LoadAssignedAdvisors(eDynamicFormType formType, int studentPersonId, int[] cids);
	}
}
