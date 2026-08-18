using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200003D RID: 61
	public class DynamicDataServiceManager : IDynamicData, IService
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		public LoadDataByFormResp LoadDataByForm(LoadDataByFormReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			List<DynamicData> list = dynamicDataManager.LoadData(Request.Context.ToDomainObject(), Request.ScreenNum, (eDynamicFormType)Request.FormType);
			LoadDataByFormResp loadDataByFormResp = new LoadDataByFormResp();
			List<DynamicDataDTO> list2;
			if (list == null)
			{
				list2 = null;
			}
			else
			{
				list2 = list.ConvertAll<DynamicDataDTO>((DynamicData dd) => dd.ToDTO());
			}
			loadDataByFormResp.Data = (list2 ?? new List<DynamicDataDTO>());
			return loadDataByFormResp;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B940 File Offset: 0x00009B40
		public LoadPerStudentDataForMultipleStudentsResp LoadPerStudentDataForMultipleStudents(LoadPerStudentDataForMultipleStudentsReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			List<DynamicDataSet> list = dynamicDataManager.LoadPerStudentDataForMultipleStudents(Request.PersonIds, Request.ControlIds);
			LoadPerStudentDataForMultipleStudentsResp loadPerStudentDataForMultipleStudentsResp = new LoadPerStudentDataForMultipleStudentsResp();
			List<DynamicDataSetDTO> list2;
			if (list == null)
			{
				list2 = null;
			}
			else
			{
				list2 = list.ConvertAll<DynamicDataSetDTO>((DynamicDataSet f) => f.ToDTO());
			}
			loadPerStudentDataForMultipleStudentsResp.Data = (list2 ?? new List<DynamicDataSetDTO>());
			return loadPerStudentDataForMultipleStudentsResp;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		public LoadDataByFieldsResp LoadDataByFields(LoadDataByFieldsReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			List<DynamicData> list = dynamicDataManager.LoadDataByFields(Request.Context.ToDomainObject(), Request.ControlIds, (eDynamicFormType)Request.DataType);
			LoadDataByFieldsResp loadDataByFieldsResp = new LoadDataByFieldsResp();
			List<DynamicDataDTO> list2;
			if (list == null)
			{
				list2 = null;
			}
			else
			{
				list2 = list.ConvertAll<DynamicDataDTO>((DynamicData dd) => dd.ToDTO());
			}
			loadDataByFieldsResp.Data = (list2 ?? new List<DynamicDataDTO>());
			return loadDataByFieldsResp;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000BA30 File Offset: 0x00009C30
		public LoadDataResp LoadData(LoadDataReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			List<DynamicData> list = dynamicDataManager.LoadData(Request.Context.ToDomainObject(), Request.Form.ToDomainObject());
			LoadDataResp loadDataResp = new LoadDataResp();
			List<DynamicDataDTO> list2;
			if (list == null)
			{
				list2 = null;
			}
			else
			{
				list2 = list.ConvertAll<DynamicDataDTO>((DynamicData dd) => dd.ToDTO());
			}
			loadDataResp.Data = (list2 ?? new List<DynamicDataDTO>());
			return loadDataResp;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000BAAC File Offset: 0x00009CAC
		public StoreFileInDocumentsResp StoreFileInDocuments(StoreFileInDocumentsReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			int fileId = (Request.DataContext != null) ? dynamicDataManager.StoreFileInDocuments(Request.Title, Request.Notes, Request.File.ToDomainObject(), Request.DataContext.ToDomainObject(), (eDynamicFormType)Request.FormType, Request.ControlId, 1000) : dynamicDataManager.StoreFileInDocuments(Request.Title, Request.Notes, Request.File.ToDomainObject(), Request.StudentPersonId, 1000);
			return new StoreFileInDocumentsResp
			{
				FileId = fileId
			};
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000BB44 File Offset: 0x00009D44
		public LoadFileFromDocumentsResp LoadFileFromDocuments(LoadFileFromDocumentsReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			BinaryFile binaryFile = dynamicDataManager.LoadFileFromDocuments(Request.StudentPersonId, Request.FileId);
			return new LoadFileFromDocumentsResp
			{
				File = binaryFile.ToDTO()
			};
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000BB88 File Offset: 0x00009D88
		public UploadDocumentToDatabaseResp UploadDocumentToDatabase(UploadDocumentToDatabaseReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			int fileId = dynamicDataManager.UploadDocumentToDatabase(Request.File.ToDomainObject(), 1000);
			return new UploadDocumentToDatabaseResp
			{
				FileId = fileId
			};
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public SaveDataResp SaveData(SaveDataReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			dynamicDataManager.SaveData(Request.Context.ToDomainObject(), Request.Data.ConvertAll<DynamicData>((DynamicDataDTO f) => f.ToDomainObject()), (eDynamicFormType)Request.FormType);
			return new SaveDataResp();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000BC34 File Offset: 0x00009E34
		public LoadEmailResp LoadEmail(LoadEmailReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			DynamicData dynamicData = dynamicDataManager.LoadEmail(Request.PersonId);
			return new LoadEmailResp
			{
				EmailData = ((dynamicData != null) ? dynamicData.ToDTO() : null)
			};
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000BC78 File Offset: 0x00009E78
		public GetExistingPerDateEntryResp GetExistingPerDateEntry(GetExistingPerDateEntryReq Request)
		{
			IDynamicPerDateDataManager dynamicPerDateDataManager = new DynamicPerDateDataManager(Request.GetOperationContext());
			PerDateEntry existingPerDateEntry = dynamicPerDateDataManager.GetExistingPerDateEntry(Request.StudentPersonId, Request.ScreenNum, Request.Session.ToDomainObject());
			return new GetExistingPerDateEntryResp
			{
				PerDateEntry = ((existingPerDateEntry != null) ? existingPerDateEntry.ToDTO() : null)
			};
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000BCCC File Offset: 0x00009ECC
		public CreatePerDateEntryResp CreatePerDateEntry(CreatePerDateEntryReq Request)
		{
			IDynamicPerDateDataManager dynamicPerDateDataManager = new DynamicPerDateDataManager(Request.GetOperationContext());
			int appointmentId = dynamicPerDateDataManager.CreatePerDateEntry(Request.PerDateEntry.ToDomainObject());
			return new CreatePerDateEntryResp
			{
				AppointmentId = appointmentId
			};
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public SaveDataBaseResp SaveDataBase(SaveDataBaseReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			dynamicDataManager.SaveDataBase(Request.Context.ToDomainObject(), Request.Data.ConvertAll<DynamicDataBase>((DynamicDataBaseDTO f) => f.ToDomainObject()), (eDynamicFormType)Request.FormType);
			return new SaveDataBaseResp();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000BD74 File Offset: 0x00009F74
		public DoesAtLeastOneSavedDataItemExistResp DoesAtLeastOneSavedDataItemExist(DoesAtLeastOneSavedDataItemExistReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			bool atLeastOneDataItemExists = dynamicDataManager.DoesAtLeastOneSavedDataItemExist(Request.Context.ToDomainObject(), Request.ScreenNum, (eDynamicFormType)Request.FormType);
			return new DoesAtLeastOneSavedDataItemExistResp
			{
				AtLeastOneDataItemExists = atLeastOneDataItemExists
			};
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		public DoesAtLeastOneSavedDataItemExistByControlIdsResp DoesAtLeastOneSavedDataItemExistByControlIds(DoesAtLeastOneSavedDataItemExistByControlIdsReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			bool atLeastOneDataItemExists = dynamicDataManager.DoesAtLeastOneSavedDataItemExistByControlIds(Request.Context.ToDomainObject(), Request.ControlIds, (eDynamicFormType)Request.FormType);
			return new DoesAtLeastOneSavedDataItemExistByControlIdsResp
			{
				AtLeastOneDataItemExists = atLeastOneDataItemExists
			};
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000BE0C File Offset: 0x0000A00C
		public void AddRowToDynamicTableControl(AddRowToDynamicTableControlReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			dynamicDataManager.AddRowToDynamicTableControl(Request.Context.ToDomainObject(), (eDynamicFormType)Request.FormType, Request.ControlId, Request.ColumnValues.ToArray<string>());
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000BE50 File Offset: 0x0000A050
		public UpdateIconForPerAppointmentDataChangeResp UpdateIconForPerAppointmentDataChange(UpdateIconForPerAppointmentDataChangeReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			IList<int> appointmentIds = dynamicDataManager.UpdateIconForPerAppointmentDataChange(Request.ScreenNum, Request.IconId, Request.StudentPersonId, Request.ControlIdToActivate);
			return new UpdateIconForPerAppointmentDataChangeResp
			{
				AppointmentIds = appointmentIds
			};
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000BE9C File Offset: 0x0000A09C
		public LoadPerDateEntriesResp LoadPerDateEntries(LoadPerDateEntriesReq Request)
		{
			IDynamicPerDateDataManager dynamicPerDateDataManager = new DynamicPerDateDataManager(Request.GetOperationContext());
			IList<PerDateEntry> list = dynamicPerDateDataManager.LoadPerDateEntries(Request.StudentPersonId, Request.ScreenNum);
			LoadPerDateEntriesResp loadPerDateEntriesResp = new LoadPerDateEntriesResp();
			IList<PerDateEntryDTO> perDateEntries;
			if (list == null)
			{
				perDateEntries = null;
			}
			else
			{
				perDateEntries = list.ToList<PerDateEntry>().ConvertAll<PerDateEntryDTO>((PerDateEntry g) => g.ToDTO());
			}
			loadPerDateEntriesResp.PerDateEntries = perDateEntries;
			return loadPerDateEntriesResp;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000BF0C File Offset: 0x0000A10C
		public LoadPerStudentDataForMultipleStudentsAsDataTableResp LoadPerStudentDataForMultipleStudentsAsDataTable(LoadPerStudentDataForMultipleStudentsAsDataTableReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			DataTable dataTable = dynamicDataManager.LoadPerStudentDataForMultipleStudentsAsDataTable(Request.PersonIds, Request.ControlIds);
			bool flag = dataTable == null;
			LoadPerStudentDataForMultipleStudentsAsDataTableResp result;
			if (flag)
			{
				result = new LoadPerStudentDataForMultipleStudentsAsDataTableResp();
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(dataTable.TableName);
				if (flag2)
				{
					dataTable.TableName = "table";
				}
				result = new LoadPerStudentDataForMultipleStudentsAsDataTableResp
				{
					Table = dataTable
				};
			}
			return result;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000BF78 File Offset: 0x0000A178
		public LoadAccommodationDataForMultipleStudentsAsDataTableResp LoadAccommodationDataForMultipleStudentsAsDataTable(LoadAccommodationDataForMultipleStudentsAsDataTableReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			DataTable dataTable = dynamicDataManager.LoadAccommodationDataForMultipleStudentsAsDataTable(Request.PersonIds, Request.ControlIds);
			bool flag = dataTable == null;
			LoadAccommodationDataForMultipleStudentsAsDataTableResp result;
			if (flag)
			{
				result = new LoadAccommodationDataForMultipleStudentsAsDataTableResp();
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(dataTable.TableName);
				if (flag2)
				{
					dataTable.TableName = "table";
				}
				result = new LoadAccommodationDataForMultipleStudentsAsDataTableResp
				{
					Table = dataTable
				};
			}
			return result;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		public LoadFileFromImageInfoResp LoadFileFromImageInfo(LoadFileFromImageInfoReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			BinaryFile binaryFile = dynamicDataManager.LoadFileFromImageInfo(Request.DataId, Request.ControlId, Request.DatabaseTablePostFix);
			return new LoadFileFromImageInfoResp
			{
				File = ((binaryFile != null) ? binaryFile.ToDTO() : null)
			};
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000C034 File Offset: 0x0000A234
		public ChangeAssignedAdvisorBatchResp ChangeAssignedAdvisorBatch(ChangeAssignedAdvisorBatchReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			IList<Pair<PersonBase, PersonBase>> list = dynamicDataManager.ChangeAssignedAdvisorBatch(Request.ControlId, Request.OldAdvisorPersonId, Request.NewAdvisorPersonId);
			ChangeAssignedAdvisorBatchResp changeAssignedAdvisorBatchResp = new ChangeAssignedAdvisorBatchResp();
			IList<Pair<PersonBaseDTO, PersonBaseDTO>> updatedPersonIdsWithOldAdvisorPersonId;
			if (list == null)
			{
				updatedPersonIdsWithOldAdvisorPersonId = null;
			}
			else
			{
				updatedPersonIdsWithOldAdvisorPersonId = list.Select(delegate(Pair<PersonBase, PersonBase> g)
				{
					PersonBase item = g.Item1;
					PersonBaseDTO item2 = (item != null) ? item.ToDTO() : null;
					PersonBase item3 = g.Item2;
					return new Pair<PersonBaseDTO, PersonBaseDTO>(item2, (item3 != null) ? item3.ToDTO() : null);
				}).ToList<Pair<PersonBaseDTO, PersonBaseDTO>>();
			}
			changeAssignedAdvisorBatchResp.UpdatedPersonIdsWithOldAdvisorPersonId = updatedPersonIdsWithOldAdvisorPersonId;
			return changeAssignedAdvisorBatchResp;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		public GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlResp GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			int numberOfStudentsStaffIsAssignedToInStaffDropListControl = dynamicDataManager.GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(Request.ControlId, Request.PersonId);
			return new GetNumberOfStudentsStaffIsAssignedToInStaffDropListControlResp
			{
				NumberOfStudents = numberOfStudentsStaffIsAssignedToInStaffDropListControl
			};
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		public LoadAssignedAdvisorsResp LoadAssignedAdvisors(LoadAssignedAdvisorsReq Request)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(Request.GetOperationContext());
			LoadAssignedAdvisorsResp loadAssignedAdvisorsResp = new LoadAssignedAdvisorsResp();
			IList<BasicPerson> list = dynamicDataManager.LoadAssignedAdvisors(Request.FormType, Request.StudentPersonId, Request.AssignedAdvisorControlIds);
			IList<BasicPersonDTO> assignedAdvisors;
			if (list == null)
			{
				assignedAdvisors = null;
			}
			else
			{
				assignedAdvisors = (from g in list
				select g.ToDTO()).ToList<BasicPersonDTO>();
			}
			loadAssignedAdvisorsResp.AssignedAdvisors = assignedAdvisors;
			return loadAssignedAdvisorsResp;
		}
	}
}
