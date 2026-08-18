using System;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.Academic;
using TechnoPro.Common.DAO.Impl.General;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.Impl.Workflows;
using TechnoPro.Common.DAO.Vets;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Academic;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.DAO.Impl.Vets
{
	// Token: 0x0200001F RID: 31
	public class VetsBenefitApplicationDAO : IVetsBenefitApplicationDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00005BC7 File Offset: 0x00003DC7
		public VetsBenefitApplicationDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00005BD9 File Offset: 0x00003DD9
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00005BE1 File Offset: 0x00003DE1
		public OperationContext OpContext { get; set; }

		// Token: 0x060000C4 RID: 196 RVA: 0x00005BEC File Offset: 0x00003DEC
		private static VetsBenefitApplication GetBenefitApplicationFromRecord(IDataReader record, OperationContext opContext, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = record == null;
			VetsBenefitApplication result;
			if (flag)
			{
				result = null;
			}
			else
			{
				PersonBase personFromReader = PeopleDAO.GetPersonFromReader("", record, opContext, batchDecryptor);
				Semester semesterFromRecord = SemesterDAO.GetSemesterFromRecord(record);
				VetsChapter chapterFromRecord = VetsChapterDAO.GetChapterFromRecord(record);
				int num = (record["PreferredStep"] is DBNull) ? 0 : ((int)record["PreferredStep"]);
				int num2 = (record["FinalStatus"] is DBNull) ? 0 : ((int)record["FinalStatus"]);
				result = new VetsBenefitApplication
				{
					BenefitApplicationId = (Guid)record["BenefitApplicationId"],
					Chapter = chapterFromRecord,
					Student = personFromReader,
					Semester = semesterFromRecord,
					PerSemesterId = ((record["PerSemesterId"] is DBNull) ? 0 : ((int)record["PerSemesterId"])),
					StudentAgreed = (!(record["StudentAgreeCompleted"] is DBNull) && (bool)record["StudentAgreeCompleted"]),
					BenAppCompleted = (!(record["BenAppCompleted"] is DBNull) && (bool)record["BenAppCompleted"]),
					RegistrationCompleted = (!(record["RegistrationCompleted"] is DBNull) && (bool)record["RegistrationCompleted"]),
					PreferredStep = (Enum.IsDefined(typeof(eVetsBenefitApplicationStep), num) ? new eVetsBenefitApplicationStep?((eVetsBenefitApplicationStep)num) : null),
					FinalStatus = (eVetsRequestStatus)(Enum.IsDefined(typeof(eVetsRequestStatus), num2) ? num2 : 0),
					ScreenerPersonId = ((record["ScreenerPersonId"] is DBNull) ? 0 : ((int)record["ScreenerPersonId"])),
					CertifierPersonId = ((record["CertifierPersonId"] is DBNull) ? 0 : ((int)record["CertifierPersonId"])),
					ModificationHistoryItem = ModificationHistoryItemDAO.GetModificationHistoryItemBaseFromRecord(record, ""),
					CurrentProgressStepId = ((record["ProgressStepId"] is DBNull) ? Guid.Empty : ((Guid)record["ProgressStepId"]))
				};
			}
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005E60 File Offset: 0x00004060
		[DebuggerStepThrough]
		public Task<VetsBenefitApplication> LoadBenefitApplicationByIdAsync(Guid BenefitApplicationId)
		{
			VetsBenefitApplicationDAO.<LoadBenefitApplicationByIdAsync>d__6 <LoadBenefitApplicationByIdAsync>d__ = new VetsBenefitApplicationDAO.<LoadBenefitApplicationByIdAsync>d__6();
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<VetsBenefitApplication>.Create();
			<LoadBenefitApplicationByIdAsync>d__.<>4__this = this;
			<LoadBenefitApplicationByIdAsync>d__.BenefitApplicationId = BenefitApplicationId;
			<LoadBenefitApplicationByIdAsync>d__.<>1__state = -1;
			<LoadBenefitApplicationByIdAsync>d__.<>t__builder.Start<VetsBenefitApplicationDAO.<LoadBenefitApplicationByIdAsync>d__6>(ref <LoadBenefitApplicationByIdAsync>d__);
			return <LoadBenefitApplicationByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005EAC File Offset: 0x000040AC
		private static void UpdateBenefitApplicationStatusFromRecord(ref VetsBenefitApplicationStatus baseApplication, IDataReader record, IBatchDecryptor batchDecryptor, OperationContext opContext)
		{
			bool flag = record == null;
			if (!flag)
			{
				baseApplication.Screener = PeopleDAO.GetPersonFromReader("", record, opContext, batchDecryptor);
				baseApplication.Certifier = PeopleDAO.GetPersonFromReader("certifier", record, opContext, batchDecryptor);
				int num = (record["FinalStatus"] is DBNull) ? 0 : ((int)record["FinalStatus"]);
				baseApplication.FinalStatus = (eVetsRequestStatus)(Enum.IsDefined(typeof(eVetsRequestStatus), num) ? num : 0);
				baseApplication.CurrentProgressStep = WorkflowProgressStepDAO.GetProgressStepFromRecord(record);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005F48 File Offset: 0x00004148
		private static VetsRequestStatusNote GetRequestStatusNoteFromRecord(IDataReader record, IBatchDecryptor batchDecryptor, OperationContext opContext)
		{
			bool flag = record == null;
			VetsRequestStatusNote result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["BenefitApplicationStatusDetailNotesId"] is DBNull) ? 0 : ((int)record["BenefitApplicationStatusDetailNotesId"]);
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = new VetsRequestStatusNote
					{
						BenefitApplicationStatusDetailNotesId = num,
						DateEntered = (DateTime)record["DateEntered"],
						ForStudent = (!(record["ForStudent"] is DBNull) && (bool)record["ForStudent"]),
						Note = ((record["Note"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["Note"]))
					};
				}
			}
			return result;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000602C File Offset: 0x0000422C
		[DebuggerStepThrough]
		public Task<VetsBenefitApplicationStatus> LoadBenefitApplicationStatusByIdAsync(VetsBenefitApplicationStatus baseApplication)
		{
			VetsBenefitApplicationDAO.<LoadBenefitApplicationStatusByIdAsync>d__9 <LoadBenefitApplicationStatusByIdAsync>d__ = new VetsBenefitApplicationDAO.<LoadBenefitApplicationStatusByIdAsync>d__9();
			<LoadBenefitApplicationStatusByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<VetsBenefitApplicationStatus>.Create();
			<LoadBenefitApplicationStatusByIdAsync>d__.<>4__this = this;
			<LoadBenefitApplicationStatusByIdAsync>d__.baseApplication = baseApplication;
			<LoadBenefitApplicationStatusByIdAsync>d__.<>1__state = -1;
			<LoadBenefitApplicationStatusByIdAsync>d__.<>t__builder.Start<VetsBenefitApplicationDAO.<LoadBenefitApplicationStatusByIdAsync>d__9>(ref <LoadBenefitApplicationStatusByIdAsync>d__);
			return <LoadBenefitApplicationStatusByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006078 File Offset: 0x00004278
		[DebuggerStepThrough]
		public Task UpdateBenefitApplicationStudentInfoAsync(Guid BenefitApplicationId, bool? RegistrationCompleted, Guid? ChapterId, bool? BenAppCompleted, bool? StudentAgreeCompleted, eVetsBenefitApplicationStep? PreferredStep)
		{
			VetsBenefitApplicationDAO.<UpdateBenefitApplicationStudentInfoAsync>d__10 <UpdateBenefitApplicationStudentInfoAsync>d__ = new VetsBenefitApplicationDAO.<UpdateBenefitApplicationStudentInfoAsync>d__10();
			<UpdateBenefitApplicationStudentInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateBenefitApplicationStudentInfoAsync>d__.<>4__this = this;
			<UpdateBenefitApplicationStudentInfoAsync>d__.BenefitApplicationId = BenefitApplicationId;
			<UpdateBenefitApplicationStudentInfoAsync>d__.RegistrationCompleted = RegistrationCompleted;
			<UpdateBenefitApplicationStudentInfoAsync>d__.ChapterId = ChapterId;
			<UpdateBenefitApplicationStudentInfoAsync>d__.BenAppCompleted = BenAppCompleted;
			<UpdateBenefitApplicationStudentInfoAsync>d__.StudentAgreeCompleted = StudentAgreeCompleted;
			<UpdateBenefitApplicationStudentInfoAsync>d__.PreferredStep = PreferredStep;
			<UpdateBenefitApplicationStudentInfoAsync>d__.<>1__state = -1;
			<UpdateBenefitApplicationStudentInfoAsync>d__.<>t__builder.Start<VetsBenefitApplicationDAO.<UpdateBenefitApplicationStudentInfoAsync>d__10>(ref <UpdateBenefitApplicationStudentInfoAsync>d__);
			return <UpdateBenefitApplicationStudentInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000060EC File Offset: 0x000042EC
		[DebuggerStepThrough]
		public Task<Guid?> CreateVetsBenefitApplicationAsync(int PersonId, int SemesterId)
		{
			VetsBenefitApplicationDAO.<CreateVetsBenefitApplicationAsync>d__11 <CreateVetsBenefitApplicationAsync>d__ = new VetsBenefitApplicationDAO.<CreateVetsBenefitApplicationAsync>d__11();
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Guid?>.Create();
			<CreateVetsBenefitApplicationAsync>d__.<>4__this = this;
			<CreateVetsBenefitApplicationAsync>d__.PersonId = PersonId;
			<CreateVetsBenefitApplicationAsync>d__.SemesterId = SemesterId;
			<CreateVetsBenefitApplicationAsync>d__.<>1__state = -1;
			<CreateVetsBenefitApplicationAsync>d__.<>t__builder.Start<VetsBenefitApplicationDAO.<CreateVetsBenefitApplicationAsync>d__11>(ref <CreateVetsBenefitApplicationAsync>d__);
			return <CreateVetsBenefitApplicationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006140 File Offset: 0x00004340
		[DebuggerStepThrough]
		public Task UpdateVetsBenefitApplicationModificationEntryAsync(Guid BenefitApplicationId, params eVetsBenefitApplicationModificationType[] ModificationTypes)
		{
			VetsBenefitApplicationDAO.<UpdateVetsBenefitApplicationModificationEntryAsync>d__12 <UpdateVetsBenefitApplicationModificationEntryAsync>d__ = new VetsBenefitApplicationDAO.<UpdateVetsBenefitApplicationModificationEntryAsync>d__12();
			<UpdateVetsBenefitApplicationModificationEntryAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateVetsBenefitApplicationModificationEntryAsync>d__.<>4__this = this;
			<UpdateVetsBenefitApplicationModificationEntryAsync>d__.BenefitApplicationId = BenefitApplicationId;
			<UpdateVetsBenefitApplicationModificationEntryAsync>d__.ModificationTypes = ModificationTypes;
			<UpdateVetsBenefitApplicationModificationEntryAsync>d__.<>1__state = -1;
			<UpdateVetsBenefitApplicationModificationEntryAsync>d__.<>t__builder.Start<VetsBenefitApplicationDAO.<UpdateVetsBenefitApplicationModificationEntryAsync>d__12>(ref <UpdateVetsBenefitApplicationModificationEntryAsync>d__);
			return <UpdateVetsBenefitApplicationModificationEntryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006194 File Offset: 0x00004394
		private static VetsStudentCardInfoItem GetVetsStudentCardInfoItemFromRecord(IDataReader record)
		{
			bool flag = record == null || record["BenefitApplicationId"] is DBNull;
			VetsStudentCardInfoItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["PreferredStep"] is DBNull) ? 0 : ((int)record["PreferredStep"]);
				int num2 = (record["FinalStatus"] is DBNull) ? 0 : ((int)record["FinalStatus"]);
				result = new VetsStudentCardInfoItem
				{
					VetsBenefitApplicationId = (Guid)record["BenefitApplicationId"],
					Semester = SemesterDAO.GetSemesterFromRecord(record),
					ChapterId = ((record["ChapterId"] is DBNull) ? null : new Guid?((Guid)record["ChapterId"])),
					ChapterTitle = ((record["ChapterTitle"] is DBNull) ? string.Empty : ((string)record["ChapterTitle"])),
					StudentAgreeCompleted = (!(record["StudentAgreeCompleted"] is DBNull) && (bool)record["StudentAgreeCompleted"]),
					BenAppCompleted = (!(record["StudentAgreeCompleted"] is DBNull) && (bool)record["StudentAgreeCompleted"]),
					RegistrationCompleted = (!(record["StudentAgreeCompleted"] is DBNull) && (bool)record["StudentAgreeCompleted"]),
					PreferredStep = (Enum.IsDefined(typeof(eVetsBenefitApplicationStep), num) ? new eVetsBenefitApplicationStep?((eVetsBenefitApplicationStep)num) : null),
					FinalStatus = (eVetsRequestStatus)(Enum.IsDefined(typeof(eVetsRequestStatus), num2) ? num2 : 0),
					CurrentProgressStepId = ((record["currentprogressid"] is DBNull) ? Guid.Empty : ((Guid)record["currentprogressid"])),
					DateCreated = ((record["DateCreated"] is DBNull) ? DateTime.MinValue : ((DateTime)record["DateCreated"])),
					DateLastModified = ((record["DateLastModified"] is DBNull) ? null : new DateTime?((DateTime)record["DateLastModified"]))
				};
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000642C File Offset: 0x0000462C
		[DebuggerStepThrough]
		public Task<VetsStudentCardInfo> LoadStudentVeteranCardInfoAsync(int PersonId)
		{
			VetsBenefitApplicationDAO.<LoadStudentVeteranCardInfoAsync>d__14 <LoadStudentVeteranCardInfoAsync>d__ = new VetsBenefitApplicationDAO.<LoadStudentVeteranCardInfoAsync>d__14();
			<LoadStudentVeteranCardInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<VetsStudentCardInfo>.Create();
			<LoadStudentVeteranCardInfoAsync>d__.<>4__this = this;
			<LoadStudentVeteranCardInfoAsync>d__.PersonId = PersonId;
			<LoadStudentVeteranCardInfoAsync>d__.<>1__state = -1;
			<LoadStudentVeteranCardInfoAsync>d__.<>t__builder.Start<VetsBenefitApplicationDAO.<LoadStudentVeteranCardInfoAsync>d__14>(ref <LoadStudentVeteranCardInfoAsync>d__);
			return <LoadStudentVeteranCardInfoAsync>d__.<>t__builder.Task;
		}
	}
}
