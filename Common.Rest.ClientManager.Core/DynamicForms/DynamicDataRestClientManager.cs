using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.DynamicForms
{
	// Token: 0x02000054 RID: 84
	public class DynamicDataRestClientManager : BearerTokenRestProxy<IDynamicDataClientManager>, IDynamicDataClientManager, IWebService
	{
		// Token: 0x06000328 RID: 808 RVA: 0x00009F70 File Offset: 0x00008170
		public DynamicDataRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00009F7A File Offset: 0x0000817A
		public DynamicDataRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00009F85 File Offset: 0x00008185
		public DynamicDataDTO LoadEmail(int PersonId)
		{
			return base.Get<DynamicDataDTO>(string.Format("dynamicdata/email/pid/{0}", PersonId), true);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00009FA0 File Offset: 0x000081A0
		public int StoreFileInDocuments(int StudentPersonId, string Title, string Notes, BinaryFileDTO File)
		{
			StoreFileInDocumentsReq storeFileInDocumentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<StoreFileInDocumentsReq>();
			storeFileInDocumentsReq.StudentPersonId = StudentPersonId;
			storeFileInDocumentsReq.Title = (Title ?? "");
			storeFileInDocumentsReq.Notes = (Notes ?? "");
			storeFileInDocumentsReq.File = File;
			return base.Post<StoreFileInDocumentsReq, int>(storeFileInDocumentsReq, "dynamicdata/storefileindocuments");
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00009FF4 File Offset: 0x000081F4
		public IList<DynamicDataDTO> LoadDataByFields(DynamicDataContextDTO Context, IList<int> ControlIds, eDynamicFormTypeDTO DataType)
		{
			return base.GetMany<DynamicDataDTO>(string.Format("dynamicdata/databyfields/controlids/{0}/datatype/{1}/primaryid/{2}/secondaryid/{3}", new object[]
			{
				ControlIds.CommaSeparatedValuesWithoutSpace<int>(),
				DataType,
				Context.PrimaryId,
				Context.SecondaryId
			}), true);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000A048 File Offset: 0x00008248
		public IList<DynamicDataDTO> LoadData(DynamicDataContextDTO Context, DynamicFormDTO Form)
		{
			LoadDataReq loadDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDataReq>();
			loadDataReq.Context = Context;
			loadDataReq.Form = Form;
			return base.Post<LoadDataReq, IList<DynamicDataDTO>>(loadDataReq, "dynamicdata/loaddata");
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000A07C File Offset: 0x0000827C
		public IList<DynamicDataDTO> LoadData(DynamicDataContextDTO Context, int screenNum, eDynamicFormTypeDTO formType)
		{
			LoadDataByFormReq loadDataByFormReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDataByFormReq>();
			loadDataByFormReq.Context = Context;
			loadDataByFormReq.ScreenNum = screenNum;
			loadDataByFormReq.FormType = formType;
			return base.Post<LoadDataByFormReq, IList<DynamicDataDTO>>(loadDataByFormReq, "dynamicdata/loaddatabyformtype");
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000A0B5 File Offset: 0x000082B5
		public IList<DynamicDataSetDTO> LoadPerStudentDataForMultipleStudents(IList<int> PersonIds, IList<int> ControlIds)
		{
			return base.GetMany<DynamicDataSetDTO>(string.Format("dynamicdata/perstudentdataformultiplestudents/pids/{0}/controlids/{1}", PersonIds.CommaSeparatedValuesWithoutSpace<int>(), ControlIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000A0D4 File Offset: 0x000082D4
		public BinaryFileDTO LoadFileFromDocuments(int StudentPersonId, int FileId)
		{
			return base.Get<BinaryFileDTO>(string.Format("dynamicdata/filefromdocuments/studentpid/{0}/fileid/{1}", StudentPersonId, FileId), true);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000A0F3 File Offset: 0x000082F3
		public int UploadDocumentToDatabase(BinaryFileDTO File)
		{
			return base.Post<BinaryFileDTO, int>(File, "dynamicdata/uploaddocumenttodatabase");
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000A104 File Offset: 0x00008304
		public void SaveData(DynamicDataContextDTO Context, IList<DynamicDataDTO> Data, eDynamicFormTypeDTO FormType)
		{
			SaveDataReq saveDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveDataReq>();
			saveDataReq.Context = Context;
			saveDataReq.Data = Data.ToList<DynamicDataDTO>();
			saveDataReq.FormType = FormType;
			base.Post<SaveDataReq>(saveDataReq, "dynamicdata/savedata");
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000A144 File Offset: 0x00008344
		public PerDateEntryDTO GetExistingPerDateEntry(int StudentPersonId, int ScreenNum, SessionDTO Session)
		{
			GetExistingPerDateEntryReq getExistingPerDateEntryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetExistingPerDateEntryReq>();
			getExistingPerDateEntryReq.StudentPersonId = StudentPersonId;
			getExistingPerDateEntryReq.ScreenNum = ScreenNum;
			getExistingPerDateEntryReq.Session = Session;
			return base.Post<GetExistingPerDateEntryReq, PerDateEntryDTO>(getExistingPerDateEntryReq, "dynamicdata/getexistingperdateentry");
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000A17D File Offset: 0x0000837D
		public int CreatePerDateEntry(PerDateEntryDTO PerDateEntry)
		{
			return base.Post<PerDateEntryDTO, int>(PerDateEntry, "dynamicdata/createperdateentry");
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000A18C File Offset: 0x0000838C
		public bool DoesAtLeastOneSavedDataItemExist(DynamicDataContextDTO Context, eDynamicFormTypeDTO FormType, int ScreenNum)
		{
			return base.Get<bool>(string.Format("dynamicdata/doesatleastonesaveddataitemexist/screennum/{0}/formtype/{1}/primaryid/{2}/secondaryid/{3}", new object[]
			{
				ScreenNum,
				FormType,
				Context.PrimaryId,
				Context.SecondaryId
			}), true);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000A1E0 File Offset: 0x000083E0
		public void SaveDataBase(DynamicDataContextDTO Context, List<DynamicDataBaseDTO> Data, eDynamicFormTypeDTO FormType)
		{
			SaveDataBaseReq saveDataBaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveDataBaseReq>();
			saveDataBaseReq.Context = Context;
			saveDataBaseReq.Data = Data;
			saveDataBaseReq.FormType = FormType;
			base.Post<SaveDataBaseReq>(saveDataBaseReq, "dynamicdata/savedatabase");
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000A21C File Offset: 0x0000841C
		public void AddRowToDynamicTableControl(DynamicDataContextDTO Context, eDynamicFormTypeDTO FormType, int ControlId, params string[] columnValues)
		{
			AddRowToDynamicTableControlReq addRowToDynamicTableControlReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddRowToDynamicTableControlReq>();
			addRowToDynamicTableControlReq.Context = Context;
			addRowToDynamicTableControlReq.FormType = FormType;
			addRowToDynamicTableControlReq.ControlId = ControlId;
			addRowToDynamicTableControlReq.ColumnValues = columnValues;
			base.Post<AddRowToDynamicTableControlReq>(addRowToDynamicTableControlReq, "dynamicdata/addrowtodynamictablecontrol");
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000A260 File Offset: 0x00008460
		public int StoreFileInDocuments(DynamicDataContextDTO Context, eDynamicFormTypeDTO FormType, int ControlId, string Title, string Notes, BinaryFileDTO File)
		{
			StoreFileInDocumentsReq storeFileInDocumentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<StoreFileInDocumentsReq>();
			storeFileInDocumentsReq.DataContext = Context;
			storeFileInDocumentsReq.FormType = FormType;
			storeFileInDocumentsReq.ControlId = ControlId;
			storeFileInDocumentsReq.Title = Title;
			storeFileInDocumentsReq.Notes = Notes;
			storeFileInDocumentsReq.File = File;
			return base.Post<StoreFileInDocumentsReq, int>(storeFileInDocumentsReq, "dynamicdata/storefileindocuments");
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public IList<int> UpdateIconForPerAppointmentDataChange(int ScreenNum, int IconId, int StudentPersonId, int ControlIdToActivate)
		{
			UpdateIconForPerAppointmentDataChangeReq updateIconForPerAppointmentDataChangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateIconForPerAppointmentDataChangeReq>();
			updateIconForPerAppointmentDataChangeReq.ScreenNum = ScreenNum;
			updateIconForPerAppointmentDataChangeReq.IconId = IconId;
			updateIconForPerAppointmentDataChangeReq.StudentPersonId = StudentPersonId;
			updateIconForPerAppointmentDataChangeReq.ControlIdToActivate = ControlIdToActivate;
			IList<int> list = base.Post<UpdateIconForPerAppointmentDataChangeReq, IList<int>>(updateIconForPerAppointmentDataChangeReq, "dynamicdata/iconforperappointmentdatachange");
			IAppointmentClientManager appointmentClientManager = ObjectFactory.Resolve<IAppointmentClientManager>();
			using (IEnumerator<int> enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int appId = enumerator.Current;
					Task.Run(() => AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(appointmentClientManager.LoadAppointment(appId)));
				}
			}
			return list;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000A360 File Offset: 0x00008560
		public IList<PerDateEntryDTO> LoadPerDateEntries(int StudentPersonId, int ScreenNum)
		{
			return base.GetMany<PerDateEntryDTO>(string.Format("dynamicdata/perdateentries/studentpid/{0}/screennum/{1}", StudentPersonId, ScreenNum), true);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000A37F File Offset: 0x0000857F
		public DataTable LoadPerStudentDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds)
		{
			return base.Get<DataTable>(string.Format("dynamicdata/perstudentdataformultiplestudents/studentpids/{0}/controlids/{1}", PersonIds.CommaSeparatedValuesWithoutSpace<int>(), ControlIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000A39E File Offset: 0x0000859E
		public DataTable LoadAccommodationDataForMultipleStudentsAsDataTable(IList<int> PersonIds, IList<int> ControlIds)
		{
			return base.Get<DataTable>(string.Format("dynamicdata/accommodationdataformultiplestudents/pids/{0}/controlids/{1}", PersonIds.CommaSeparatedValuesWithoutSpace<int>(), ControlIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000A3BD File Offset: 0x000085BD
		public BinaryFileDTO LoadFileFromImageInfo(int DataId, int ControlId)
		{
			return base.Get<BinaryFileDTO>(string.Format("dynamicdata/filefromimageinfo/dataid/{0}/controlid/{1}", DataId, ControlId), true);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000A3DC File Offset: 0x000085DC
		public int GetNumberOfStudentsStaffIsAssignedToInStaffDropListControl(int cid, int pid)
		{
			return base.Get<int>(string.Format("dynamicdata/numberofstudentsstaffisassignedtoinstaffdroplistcontrol/controlid/{0}/pid/{1}", cid, pid), true);
		}
	}
}
