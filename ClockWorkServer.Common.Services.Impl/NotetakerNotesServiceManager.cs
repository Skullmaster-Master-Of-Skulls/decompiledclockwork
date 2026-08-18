using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking;
using TechnoPro.Common.Core.Mappers.Notetaking;
using TechnoPro.Common.Core.Notetaking;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.Notetaking;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Notetaking;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200006E RID: 110
	public class NotetakerNotesServiceManager : INotetakerNotes, IService
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x00013328 File Offset: 0x00011528
		private bool IsAllowedToUseNotetaking(OperationContext opContext)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(opContext);
			return oldUserSettingManager.GetSettingValue_Bool(opContext.WhoAmI, eSettingCode.SETTING_ShowServiceProviders);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00013354 File Offset: 0x00011554
		private void CheckIsAllowedToUseNotetaking(OperationContext opContext)
		{
			bool flag = this.IsAllowedToUseNotetaking(opContext);
			bool flag2 = flag;
			if (flag2)
			{
				return;
			}
			CWLogger.Logger.Warn("NotetakerNotesServiceManager:NotAllowedToUseNotetaking:whoami={0}", ((opContext != null) ? opContext.WhoAmI : -1).ToString());
			throw new PermissionDeniedException("Not allowed to use notetaking");
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000133A0 File Offset: 0x000115A0
		public LoadLectureNoteDescriptionsResp LoadLectureNoteDescriptions(LoadLectureNoteDescriptionsReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			this.CheckIsAllowedToUseNotetaking(operationContext);
			INotetakerNotesManager notetakerNotesManager = new NotetakerNotesManager(operationContext);
			IList<LectureNoteDescription> list = notetakerNotesManager.LoadLectureNoteDescriptions(Request.CourseStartDate, Request.CourseEndDate, Request.OnlyReturnNotesMarkedForDeletion);
			LoadLectureNoteDescriptionsResp loadLectureNoteDescriptionsResp = new LoadLectureNoteDescriptionsResp();
			IList<LectureNoteDescriptionDTO> lectureNoteDescriptions;
			if (list == null)
			{
				lectureNoteDescriptions = null;
			}
			else
			{
				lectureNoteDescriptions = (from g in list
				select g.ToDTO()).ToList<LectureNoteDescriptionDTO>();
			}
			loadLectureNoteDescriptionsResp.LectureNoteDescriptions = lectureNoteDescriptions;
			return loadLectureNoteDescriptionsResp;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00013420 File Offset: 0x00011620
		public DeleteAllNotesMarkedForDeletionTodayOrEarlierResp DeleteAllNotesMarkedForDeletionTodayOrEarlier(DeleteAllNotesMarkedForDeletionTodayOrEarlierReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			this.CheckIsAllowedToUseNotetaking(operationContext);
			INotetakerNotesManager notetakerNotesManager = new NotetakerNotesManager(operationContext);
			int numNotesDeleted = notetakerNotesManager.DeleteAllNotesMarkedForDeletionTodayOrEarlier();
			return new DeleteAllNotesMarkedForDeletionTodayOrEarlierResp
			{
				NumNotesDeleted = numNotesDeleted
			};
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001345C File Offset: 0x0001165C
		public DeleteAllNotesMarkedForDeletionResp DeleteAllNotesMarkedForDeletion(DeleteAllNotesMarkedForDeletionReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			this.CheckIsAllowedToUseNotetaking(operationContext);
			INotetakerNotesManager notetakerNotesManager = new NotetakerNotesManager(operationContext);
			int numNotesDeleted = notetakerNotesManager.DeleteAllNotesMarkedForDeletion();
			return new DeleteAllNotesMarkedForDeletionResp
			{
				NumNotesDeleted = numNotesDeleted
			};
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00013498 File Offset: 0x00011698
		public RemoveAllNotesDeletionMarksResp RemoveAllNotesDeletionMarks(RemoveAllNotesDeletionMarksReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			this.CheckIsAllowedToUseNotetaking(operationContext);
			INotetakerNotesManager notetakerNotesManager = new NotetakerNotesManager(operationContext);
			notetakerNotesManager.RemoveAllNotesDeletionMarks();
			return new RemoveAllNotesDeletionMarksResp();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000134CC File Offset: 0x000116CC
		public RemoveNotesDeletionMarksResp RemoveNotesDeletionMarks(RemoveNotesDeletionMarksReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			this.CheckIsAllowedToUseNotetaking(operationContext);
			INotetakerNotesManager notetakerNotesManager = new NotetakerNotesManager(operationContext);
			notetakerNotesManager.RemoveNotesDeletionMarks(Request.NotetakerDocumentIds);
			return new RemoveNotesDeletionMarksResp();
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00013508 File Offset: 0x00011708
		public AddNotesDeletionMarksResp AddNotesDeletionMarks(AddNotesDeletionMarksReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			this.CheckIsAllowedToUseNotetaking(operationContext);
			INotetakerNotesManager notetakerNotesManager = new NotetakerNotesManager(operationContext);
			notetakerNotesManager.AddNotesDeletionMarks(Request.DateOfDeletion, Request.NotetakerDocumentIds);
			return new AddNotesDeletionMarksResp();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00013548 File Offset: 0x00011748
		public DownloadLectureNoteResp DownloadLectureNote(DownloadLectureNoteReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			this.CheckIsAllowedToUseNotetaking(operationContext);
			INotetakerNotesManager notetakerNotesManager = new NotetakerNotesManager(operationContext);
			LectureNote lectureNote = notetakerNotesManager.DownloadLectureNote(Request.NotetakerDocumentId);
			return new DownloadLectureNoteResp
			{
				LectureNote = ((lectureNote != null) ? lectureNote.ToDTO() : null)
			};
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00013598 File Offset: 0x00011798
		public GetTotalFileSizeByMonthResp GetTotalFileSizeByMonth(GetTotalFileSizeByMonthReq Request)
		{
			OperationContext operationContext = Request.GetOperationContext();
			this.CheckIsAllowedToUseNotetaking(operationContext);
			INotetakerNotesManager notetakerNotesManager = new NotetakerNotesManager(operationContext);
			IDictionary<DateTime, long> totalFileSizeByMonth = notetakerNotesManager.GetTotalFileSizeByMonth();
			return new GetTotalFileSizeByMonthResp
			{
				TotalFileSizesByMonths = totalFileSizeByMonth
			};
		}
	}
}
