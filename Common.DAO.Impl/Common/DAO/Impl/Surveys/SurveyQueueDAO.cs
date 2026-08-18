using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Surveys;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Surveys;

namespace TechnoPro.Common.DAO.Impl.Surveys
{
	// Token: 0x0200003F RID: 63
	public class SurveyQueueDAO : ISurveyQueueDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public SurveyQueueDAO()
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000ED24 File Offset: 0x0000CF24
		public SurveyQueueDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000ED36 File Offset: 0x0000CF36
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000ED3E File Offset: 0x0000CF3E
		public OperationContext OpContext { get; set; }

		// Token: 0x060001A4 RID: 420 RVA: 0x0000ED48 File Offset: 0x0000CF48
		[DebuggerStepThrough]
		public Task<bool> DeleteSurveyQueueItemAsync(int peopleSurveyId)
		{
			SurveyQueueDAO.<DeleteSurveyQueueItemAsync>d__6 <DeleteSurveyQueueItemAsync>d__ = new SurveyQueueDAO.<DeleteSurveyQueueItemAsync>d__6();
			<DeleteSurveyQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DeleteSurveyQueueItemAsync>d__.<>4__this = this;
			<DeleteSurveyQueueItemAsync>d__.peopleSurveyId = peopleSurveyId;
			<DeleteSurveyQueueItemAsync>d__.<>1__state = -1;
			<DeleteSurveyQueueItemAsync>d__.<>t__builder.Start<SurveyQueueDAO.<DeleteSurveyQueueItemAsync>d__6>(ref <DeleteSurveyQueueItemAsync>d__);
			return <DeleteSurveyQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000ED94 File Offset: 0x0000CF94
		[DebuggerStepThrough]
		public Task<IList<SurveyStatus>> LoadLookupSurveyStatusesAsync()
		{
			SurveyQueueDAO.<LoadLookupSurveyStatusesAsync>d__7 <LoadLookupSurveyStatusesAsync>d__ = new SurveyQueueDAO.<LoadLookupSurveyStatusesAsync>d__7();
			<LoadLookupSurveyStatusesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<SurveyStatus>>.Create();
			<LoadLookupSurveyStatusesAsync>d__.<>4__this = this;
			<LoadLookupSurveyStatusesAsync>d__.<>1__state = -1;
			<LoadLookupSurveyStatusesAsync>d__.<>t__builder.Start<SurveyQueueDAO.<LoadLookupSurveyStatusesAsync>d__7>(ref <LoadLookupSurveyStatusesAsync>d__);
			return <LoadLookupSurveyStatusesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		[DebuggerStepThrough]
		public Task<int?> LoadSurveyIdByPeopleSurveyId(int peopleSurveyId)
		{
			SurveyQueueDAO.<LoadSurveyIdByPeopleSurveyId>d__8 <LoadSurveyIdByPeopleSurveyId>d__ = new SurveyQueueDAO.<LoadSurveyIdByPeopleSurveyId>d__8();
			<LoadSurveyIdByPeopleSurveyId>d__.<>t__builder = AsyncTaskMethodBuilder<int?>.Create();
			<LoadSurveyIdByPeopleSurveyId>d__.<>4__this = this;
			<LoadSurveyIdByPeopleSurveyId>d__.peopleSurveyId = peopleSurveyId;
			<LoadSurveyIdByPeopleSurveyId>d__.<>1__state = -1;
			<LoadSurveyIdByPeopleSurveyId>d__.<>t__builder.Start<SurveyQueueDAO.<LoadSurveyIdByPeopleSurveyId>d__8>(ref <LoadSurveyIdByPeopleSurveyId>d__);
			return <LoadSurveyIdByPeopleSurveyId>d__.<>t__builder.Task;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000EE24 File Offset: 0x0000D024
		[DebuggerStepThrough]
		public Task<SurveyQueueItem> LoadSurveyQueueItemAsync(int peopleSurveyId)
		{
			SurveyQueueDAO.<LoadSurveyQueueItemAsync>d__9 <LoadSurveyQueueItemAsync>d__ = new SurveyQueueDAO.<LoadSurveyQueueItemAsync>d__9();
			<LoadSurveyQueueItemAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItem>.Create();
			<LoadSurveyQueueItemAsync>d__.<>4__this = this;
			<LoadSurveyQueueItemAsync>d__.peopleSurveyId = peopleSurveyId;
			<LoadSurveyQueueItemAsync>d__.<>1__state = -1;
			<LoadSurveyQueueItemAsync>d__.<>t__builder.Start<SurveyQueueDAO.<LoadSurveyQueueItemAsync>d__9>(ref <LoadSurveyQueueItemAsync>d__);
			return <LoadSurveyQueueItemAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000EE70 File Offset: 0x0000D070
		private SurveyQueueItem GetSurveyQueueItemFromRecord(IDataRecord record, IBatchDecryptor batchDecryptor)
		{
			int num = (record["peopleSurveyId"] is DBNull) ? 0 : Convert.ToInt32(record["peopleSurveyId"]);
			bool flag = num < 1;
			SurveyQueueItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num2 = (record["StatusTypeId"] is DBNull) ? 0 : ((int)record["StatusTypeId"]);
				int num3 = (record["assignedcounsellorpid"] is DBNull) ? 0 : ((int)record["assignedcounsellorpid"]);
				bool flag2 = record["emailisnotencrypted"] is DBNull || !Convert.ToBoolean(record["emailisnotencrypted"]);
				result = new SurveyQueueItem
				{
					PeopleSurveyId = num,
					StaffNote = ((record["StaffNote"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["StaffNote"])),
					Survey = new SurveyForDisplay
					{
						SurveyId = ((record["SurveyId"] is DBNull) ? 0 : Convert.ToInt32(record["SurveyId"])),
						Title = record["SurveyTitle"].ToString(),
						Description = record["SurveyDescription"].ToString(),
						ScreenNum = ((record["screennum"] is DBNull) ? 0 : ((int)record["screennum"])),
						ShortCode = record["SurveyShortCode"].ToString()
					},
					Student = new BasicPerson
					{
						PersonId = ((record["personid"] is DBNull) ? 0 : Convert.ToInt32(record["personid"])),
						FirstName = ((record["firstname"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["firstname"]).Trim()),
						MiddleName = ((record["middlename"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["middlename"]).Trim()),
						LastName = ((record["lastname"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["lastname"]).Trim()),
						StudentNumber = ((record["student_no"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["student_no"]).Trim())
					},
					Status = new SurveyStatus
					{
						PeopleSurveyStatusId = ((record["statusid"] is DBNull) ? 0 : ((int)record["statusid"])),
						Title = record["title"].ToString(),
						StatusType = (eSurveyStatusType)(Enum.IsDefined(typeof(eSurveyStatusType), num2) ? num2 : 0)
					},
					DateEntered = ((record["dateentered"] is DBNull) ? DateTime.MinValue : ((DateTime)record["dateentered"])),
					AssignedCounsellor = ((num3 < 1) ? null : new BasicPerson
					{
						PersonId = num3,
						FirstName = ((record["assignedcounsellorfirst"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["assignedcounsellorfirst"]).Trim()),
						LastName = ((record["assignedcounsellorlast"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["assignedcounsellorlast"]).Trim())
					}),
					StudentEmail = ((record["email"] is DBNull) ? null : (flag2 ? batchDecryptor.Decrypt((byte[])record["email"]) : Encoding.ASCII.GetString((byte[])record["email"])))
				};
			}
			return result;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000F2F0 File Offset: 0x0000D4F0
		[DebuggerStepThrough]
		public Task<IList<SurveyQueueItem>> LoadSurveyQueueItemsAsync(int surveyId, DateTime startDate, DateTime? endDate, int filterByAssignedCounsellorPid, params int[] statusIdsToExclude)
		{
			SurveyQueueDAO.<LoadSurveyQueueItemsAsync>d__11 <LoadSurveyQueueItemsAsync>d__ = new SurveyQueueDAO.<LoadSurveyQueueItemsAsync>d__11();
			<LoadSurveyQueueItemsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<SurveyQueueItem>>.Create();
			<LoadSurveyQueueItemsAsync>d__.<>4__this = this;
			<LoadSurveyQueueItemsAsync>d__.surveyId = surveyId;
			<LoadSurveyQueueItemsAsync>d__.startDate = startDate;
			<LoadSurveyQueueItemsAsync>d__.endDate = endDate;
			<LoadSurveyQueueItemsAsync>d__.filterByAssignedCounsellorPid = filterByAssignedCounsellorPid;
			<LoadSurveyQueueItemsAsync>d__.statusIdsToExclude = statusIdsToExclude;
			<LoadSurveyQueueItemsAsync>d__.<>1__state = -1;
			<LoadSurveyQueueItemsAsync>d__.<>t__builder.Start<SurveyQueueDAO.<LoadSurveyQueueItemsAsync>d__11>(ref <LoadSurveyQueueItemsAsync>d__);
			return <LoadSurveyQueueItemsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000F35C File Offset: 0x0000D55C
		[DebuggerStepThrough]
		public Task<SurveyQueueItem> UpdateSurveyQueueItemStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId)
		{
			SurveyQueueDAO.<UpdateSurveyQueueItemStatusAsync>d__12 <UpdateSurveyQueueItemStatusAsync>d__ = new SurveyQueueDAO.<UpdateSurveyQueueItemStatusAsync>d__12();
			<UpdateSurveyQueueItemStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItem>.Create();
			<UpdateSurveyQueueItemStatusAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStatusAsync>d__.peopleSurveyId = peopleSurveyId;
			<UpdateSurveyQueueItemStatusAsync>d__.newPeopleSurveyStatusId = newPeopleSurveyStatusId;
			<UpdateSurveyQueueItemStatusAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStatusAsync>d__.<>t__builder.Start<SurveyQueueDAO.<UpdateSurveyQueueItemStatusAsync>d__12>(ref <UpdateSurveyQueueItemStatusAsync>d__);
			return <UpdateSurveyQueueItemStatusAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000F3B0 File Offset: 0x0000D5B0
		[DebuggerStepThrough]
		public Task<SurveyQueueItem> UpdateSurveyQueueItemStaffNoteAsync(int peopleSurveyId, string newStaffNote)
		{
			SurveyQueueDAO.<UpdateSurveyQueueItemStaffNoteAsync>d__13 <UpdateSurveyQueueItemStaffNoteAsync>d__ = new SurveyQueueDAO.<UpdateSurveyQueueItemStaffNoteAsync>d__13();
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItem>.Create();
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.peopleSurveyId = peopleSurveyId;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.newStaffNote = newStaffNote;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder.Start<SurveyQueueDAO.<UpdateSurveyQueueItemStaffNoteAsync>d__13>(ref <UpdateSurveyQueueItemStaffNoteAsync>d__);
			return <UpdateSurveyQueueItemStaffNoteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000F404 File Offset: 0x0000D604
		[DebuggerStepThrough]
		public Task<SurveyQueueItem> UpdateSurveyQueueItemStaffNoteAndStatusAsync(int peopleSurveyId, int? newPeopleSurveyStatusId, string newStaffNote)
		{
			SurveyQueueDAO.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__14 <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__ = new SurveyQueueDAO.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__14();
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder = AsyncTaskMethodBuilder<SurveyQueueItem>.Create();
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>4__this = this;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.peopleSurveyId = peopleSurveyId;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.newPeopleSurveyStatusId = newPeopleSurveyStatusId;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.newStaffNote = newStaffNote;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>1__state = -1;
			<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Start<SurveyQueueDAO.<UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__14>(ref <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__);
			return <UpdateSurveyQueueItemStaffNoteAndStatusAsync>d__.<>t__builder.Task;
		}
	}
}
