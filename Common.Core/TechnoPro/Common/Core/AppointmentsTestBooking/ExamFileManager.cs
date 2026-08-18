using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x0200013D RID: 317
	public class ExamFileManager : IExamFileManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00069A24 File Offset: 0x00067C24
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x00069A2C File Offset: 0x00067C2C
		public OperationContext OpContext { get; set; }

		// Token: 0x06000E06 RID: 3590 RVA: 0x00069A35 File Offset: 0x00067C35
		public ExamFileManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ExamFileDAO(this.OpContext);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00069A58 File Offset: 0x00067C58
		private bool IsInstructorOrAltContactAllowedToViewExamFiles(int ExamId, int InstructorId, int AltContactId)
		{
			ILookupCourseManager lookupCourseManager = new LookupCourseManager(this.OpContext);
			LookupCourse lookupCourse = lookupCourseManager.LoadLookupCourseByExamId(ExamId);
			bool flag = lookupCourse == null;
			bool result;
			if (flag)
			{
				CWLogger.Logger.Warn("ExamFileManager:IsInstructorOrAltContactAllowedToViewExamFiles:FailedBecauseCourseCannotBeFoundForExam:ExamId={0}", ExamId);
				result = false;
			}
			else
			{
				List<LookupInstructor> list = (from g in lookupCourse.Instructors
				where g.InstructorId == InstructorId
				select g).ToList<LookupInstructor>();
				List<AlternateContact> list2 = (from g in lookupCourse.AlternateContacts
				where g.AlternateContactId == AltContactId
				select g).ToList<AlternateContact>();
				bool flag2 = list.Count < 1 && list2.Count < 1;
				if (flag2)
				{
					CWLogger.Logger.Warn("ExamFileManager:IsInstructorOrAltContactAllowedToViewExamFiles:FailedBecauseInstructorAndAltContactNotFoundOnCourse:ExamId={0}:Lucid={1}:iid={2}:altcontactid={3}", new object[]
					{
						ExamId,
						lookupCourse.LuCourseId,
						InstructorId,
						AltContactId
					});
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00069B60 File Offset: 0x00067D60
		public IList<ExamFile> LoadExamFilesByExam(int ExamId, bool IncludeDeletedFiles, bool LoadFileData)
		{
			return this.dao.LoadExamFilesByExam(ExamId, IncludeDeletedFiles, LoadFileData);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00069B80 File Offset: 0x00067D80
		public ExamFile LoadExamFileById(int ExamFileId)
		{
			return this.dao.LoadExamFileById(ExamFileId);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00069BA0 File Offset: 0x00067DA0
		public int CreateExamFile(ExamFile ExamFile)
		{
			return this.dao.CreateExamFile(ExamFile);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00069BBE File Offset: 0x00067DBE
		public void DeleteExamFile(int ExamFileId)
		{
			this.dao.DeleteExamFile(ExamFileId);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00069BD0 File Offset: 0x00067DD0
		public IList<ExamFile> LoadExamFilesByExamCheckProfAltContactPermissions(int InstructorId, int AltContactId, int ExamId, bool IncludeDeletedFiles, bool LoadFileData)
		{
			bool flag = !this.IsInstructorOrAltContactAllowedToViewExamFiles(ExamId, InstructorId, AltContactId);
			IList<ExamFile> result;
			if (flag)
			{
				CWLogger.Logger.Warn("ExamFileManager:LoadExamFilesByExamCheckProfAltContactPermissions:ProfAltContactNotAllowedToViewFiles:ExamId={0}:InstructorId={1}:AltContactId={2}", ExamId, InstructorId, AltContactId);
				result = new List<ExamFile>();
			}
			else
			{
				result = this.LoadExamFilesByExam(ExamId, IncludeDeletedFiles, LoadFileData);
			}
			return result;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00069C28 File Offset: 0x00067E28
		public ExamFile LoadExamFileByIdCheckProfAltContactPermissions(int ExamId, int InstructorId, int AltContactId, int ExamFileId)
		{
			bool flag = !this.IsInstructorOrAltContactAllowedToViewExamFiles(ExamId, InstructorId, AltContactId);
			ExamFile result;
			if (flag)
			{
				CWLogger.Logger.Warn("ExamFileManager:LoadExamFilesByExamCheckProfAltContactPermissions:ProfAltContactNotAllowedToViewFiles:ExamId={0}:InstructorId={1}:AltContactId={2}", ExamId, InstructorId, AltContactId);
				result = null;
			}
			else
			{
				result = this.dao.LoadExamFileById(ExamFileId);
			}
			return result;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00069C80 File Offset: 0x00067E80
		public IList<int> LoadExamFileIdsOlderThanDate(DateTime cutoffDate)
		{
			return this.dao.LoadExamFileIdsOlderThanDate(cutoffDate);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00069CA0 File Offset: 0x00067EA0
		public IList<int> LoadExamFileIdsWhereCourseEndDateIsInThePast(int courseEndDateOffsetInDays)
		{
			return this.dao.LoadExamFileIdsWhereCourseEndDateIsInThePast(courseEndDateOffsetInDays);
		}

		// Token: 0x04000298 RID: 664
		private IExamFileDAO dao;
	}
}
