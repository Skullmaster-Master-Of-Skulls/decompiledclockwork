using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.DAO.Impl.StudentAccommodationRequests;
using TechnoPro.Common.DAO.StudentAccommodationRequests;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.Core.StudentAccommodationRequests
{
	// Token: 0x0200003F RID: 63
	public class StudentAccommodationRequestManager : IStudentAccommodationRequestManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000FAA7 File Offset: 0x0000DCA7
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000FAAF File Offset: 0x0000DCAF
		private IStudentAccommodationRequestDAO dao { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000FAB8 File Offset: 0x0000DCB8
		private IAccommodationsManager accommodationsManager
		{
			get
			{
				IAccommodationsManager result;
				if ((result = this._am) == null)
				{
					result = (this._am = new AccommodationsManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000FAE3 File Offset: 0x0000DCE3
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000FAEB File Offset: 0x0000DCEB
		public OperationContext OpContext { get; set; }

		// Token: 0x06000296 RID: 662 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
		public StudentAccommodationRequestManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new StudentAccommodationRequestDAO(opContext);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000FB14 File Offset: 0x0000DD14
		[DebuggerStepThrough]
		private Task AddArchiveEntryForUpdateAsync(StudentCourseAccommodationRequest updatedRequest)
		{
			StudentAccommodationRequestManager.<AddArchiveEntryForUpdateAsync>d__12 <AddArchiveEntryForUpdateAsync>d__ = new StudentAccommodationRequestManager.<AddArchiveEntryForUpdateAsync>d__12();
			<AddArchiveEntryForUpdateAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AddArchiveEntryForUpdateAsync>d__.<>4__this = this;
			<AddArchiveEntryForUpdateAsync>d__.updatedRequest = updatedRequest;
			<AddArchiveEntryForUpdateAsync>d__.<>1__state = -1;
			<AddArchiveEntryForUpdateAsync>d__.<>t__builder.Start<StudentAccommodationRequestManager.<AddArchiveEntryForUpdateAsync>d__12>(ref <AddArchiveEntryForUpdateAsync>d__);
			return <AddArchiveEntryForUpdateAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000FB60 File Offset: 0x0000DD60
		[DebuggerStepThrough]
		private Task AddArchiveEntryForNewAsync(StudentCourseAccommodationRequest newRequest)
		{
			StudentAccommodationRequestManager.<AddArchiveEntryForNewAsync>d__13 <AddArchiveEntryForNewAsync>d__ = new StudentAccommodationRequestManager.<AddArchiveEntryForNewAsync>d__13();
			<AddArchiveEntryForNewAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AddArchiveEntryForNewAsync>d__.<>4__this = this;
			<AddArchiveEntryForNewAsync>d__.newRequest = newRequest;
			<AddArchiveEntryForNewAsync>d__.<>1__state = -1;
			<AddArchiveEntryForNewAsync>d__.<>t__builder.Start<StudentAccommodationRequestManager.<AddArchiveEntryForNewAsync>d__13>(ref <AddArchiveEntryForNewAsync>d__);
			return <AddArchiveEntryForNewAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		[DebuggerStepThrough]
		private Task AddArchiveEntryForDeleteAsync(StudentCourseAccommodationRequest deletedRequest)
		{
			StudentAccommodationRequestManager.<AddArchiveEntryForDeleteAsync>d__14 <AddArchiveEntryForDeleteAsync>d__ = new StudentAccommodationRequestManager.<AddArchiveEntryForDeleteAsync>d__14();
			<AddArchiveEntryForDeleteAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AddArchiveEntryForDeleteAsync>d__.<>4__this = this;
			<AddArchiveEntryForDeleteAsync>d__.deletedRequest = deletedRequest;
			<AddArchiveEntryForDeleteAsync>d__.<>1__state = -1;
			<AddArchiveEntryForDeleteAsync>d__.<>t__builder.Start<StudentAccommodationRequestManager.<AddArchiveEntryForDeleteAsync>d__14>(ref <AddArchiveEntryForDeleteAsync>d__);
			return <AddArchiveEntryForDeleteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000FBF8 File Offset: 0x0000DDF8
		public IList<StudentCourseAccommodationRequest> LoadCourseRegistrationsWithRequestByStatus(eStudentCourseAccommodationRequestStatus statuses, Range<DateTime> RestrictToCourseDates = null)
		{
			return this.dao.LoadCourseRegistrationsWithRequestByStatus(RestrictToCourseDates, statuses);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000FC18 File Offset: 0x0000DE18
		public IList<StudentCourseAccommodationRequest> LoadCourseRegistrationsWithRequestByStatusWithCourseDatesInFuture(eStudentCourseAccommodationRequestStatus statuses)
		{
			return this.dao.LoadCourseRegistrationsWithRequestByStatusWithCourseDatesInFuture(DateTime.Today.AddDays(5.0), statuses);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000FC4C File Offset: 0x0000DE4C
		public IList<CourseRegistrationWithAccommodationRequest> LoadCourseRegistrationsWithRequestByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations)
		{
			IAccommodationsManager accommodationsManager = this.accommodationsManager;
			IList<CourseRegistrationWithAccommodations> source = accommodationsManager.LoadStudentsRegisteredCoursesWithAccommodations(StudentPersonId, StartDate, EndDate, LoadAccommodations, false);
			List<CourseRegistrationWithAccommodations> source2 = source.ToList<CourseRegistrationWithAccommodations>();
			IList<StudentCourseAccommodationRequest> requests = this.LoadRequestsByStudentAndDate(StudentPersonId, StartDate, EndDate);
			return (from course in source2
			let found = requests.FirstOrDefault((StudentCourseAccommodationRequest f) => f.LuCourseId == course.CourseReg.Course.LuCourseId)
			select new CourseRegistrationWithAccommodationRequest
			{
				CourseRegistrationWithAccommodations = course,
				AccommodationRequest = found
			}).ToList<CourseRegistrationWithAccommodationRequest>();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000FCCC File Offset: 0x0000DECC
		public StudentCourseAccommodationRequest LoadRequestById(int StudentCourseAccommodationRequestId)
		{
			return this.dao.LoadRequestById(StudentCourseAccommodationRequestId);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000FCEC File Offset: 0x0000DEEC
		public IList<StudentCourseAccommodationRequest> LoadRequestsByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadRequestsByStudentAndDate(StudentPersonId, StartDate, EndDate);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		public int AddRequest(int StudentPersonId, StudentCourseAccommodationRequest CourseAccommodationRequest)
		{
			StudentAccommodationRequestManager.<>c__DisplayClass20_0 CS$<>8__locals1 = new StudentAccommodationRequestManager.<>c__DisplayClass20_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.CourseAccommodationRequest = CourseAccommodationRequest;
			bool flag;
			int num = this.dao.AddRequest(StudentPersonId, CS$<>8__locals1.CourseAccommodationRequest, out flag);
			CS$<>8__locals1.CourseAccommodationRequest.StudentCourseAccommodationRequestId = num;
			bool flag2 = flag;
			if (flag2)
			{
				Task.Run(delegate()
				{
					StudentAccommodationRequestManager.<>c__DisplayClass20_0.<<AddRequest>b__0>d <<AddRequest>b__0>d = new StudentAccommodationRequestManager.<>c__DisplayClass20_0.<<AddRequest>b__0>d();
					<<AddRequest>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<AddRequest>b__0>d.<>4__this = CS$<>8__locals1;
					<<AddRequest>b__0>d.<>1__state = -1;
					<<AddRequest>b__0>d.<>t__builder.Start<StudentAccommodationRequestManager.<>c__DisplayClass20_0.<<AddRequest>b__0>d>(ref <<AddRequest>b__0>d);
					return <<AddRequest>b__0>d.<>t__builder.Task;
				});
			}
			else
			{
				Task.Run(delegate()
				{
					StudentAccommodationRequestManager.<>c__DisplayClass20_0.<<AddRequest>b__1>d <<AddRequest>b__1>d = new StudentAccommodationRequestManager.<>c__DisplayClass20_0.<<AddRequest>b__1>d();
					<<AddRequest>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<AddRequest>b__1>d.<>4__this = CS$<>8__locals1;
					<<AddRequest>b__1>d.<>1__state = -1;
					<<AddRequest>b__1>d.<>t__builder.Start<StudentAccommodationRequestManager.<>c__DisplayClass20_0.<<AddRequest>b__1>d>(ref <<AddRequest>b__1>d);
					return <<AddRequest>b__1>d.<>t__builder.Task;
				});
			}
			return num;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000FD84 File Offset: 0x0000DF84
		public StudentCourseAccommodationRequest LoadRequestByStudentAndCourse(int StudentPersonId, int LuCourseId)
		{
			return this.dao.LoadRequestByStudentAndCourse(StudentPersonId, LuCourseId);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		public void UpdateRequest(StudentCourseAccommodationRequest CourseAccommodationRequest)
		{
			StudentAccommodationRequestManager.<>c__DisplayClass22_0 CS$<>8__locals1 = new StudentAccommodationRequestManager.<>c__DisplayClass22_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.CourseAccommodationRequest = CourseAccommodationRequest;
			this.dao.UpdateRequest(CS$<>8__locals1.CourseAccommodationRequest);
			Task.Run(delegate()
			{
				StudentAccommodationRequestManager.<>c__DisplayClass22_0.<<UpdateRequest>b__0>d <<UpdateRequest>b__0>d = new StudentAccommodationRequestManager.<>c__DisplayClass22_0.<<UpdateRequest>b__0>d();
				<<UpdateRequest>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<UpdateRequest>b__0>d.<>4__this = CS$<>8__locals1;
				<<UpdateRequest>b__0>d.<>1__state = -1;
				<<UpdateRequest>b__0>d.<>t__builder.Start<StudentAccommodationRequestManager.<>c__DisplayClass22_0.<<UpdateRequest>b__0>d>(ref <<UpdateRequest>b__0>d);
				return <<UpdateRequest>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public void DeleteRequest(int StudentCourseAccommodationRequestId)
		{
			StudentAccommodationRequestManager.<>c__DisplayClass23_0 CS$<>8__locals1 = new StudentAccommodationRequestManager.<>c__DisplayClass23_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.existingRequest = this.LoadRequestById(StudentCourseAccommodationRequestId);
			this.dao.DeleteRequest(StudentCourseAccommodationRequestId);
			Task.Run(delegate()
			{
				StudentAccommodationRequestManager.<>c__DisplayClass23_0.<<DeleteRequest>b__0>d <<DeleteRequest>b__0>d = new StudentAccommodationRequestManager.<>c__DisplayClass23_0.<<DeleteRequest>b__0>d();
				<<DeleteRequest>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<DeleteRequest>b__0>d.<>4__this = CS$<>8__locals1;
				<<DeleteRequest>b__0>d.<>1__state = -1;
				<<DeleteRequest>b__0>d.<>t__builder.Start<StudentAccommodationRequestManager.<>c__DisplayClass23_0.<<DeleteRequest>b__0>d>(ref <<DeleteRequest>b__0>d);
				return <<DeleteRequest>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000FE34 File Offset: 0x0000E034
		public void UpdateRequestStatus(int StudentAccommodationRequestId, eStudentCourseAccommodationRequestStatus NewStatus)
		{
			StudentAccommodationRequestManager.<>c__DisplayClass24_0 CS$<>8__locals1 = new StudentAccommodationRequestManager.<>c__DisplayClass24_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.existingRequest = this.LoadRequestById(StudentAccommodationRequestId);
			bool flag = CS$<>8__locals1.existingRequest != null;
			if (flag)
			{
				CS$<>8__locals1.existingRequest.Status = NewStatus;
			}
			this.dao.UpdateRequestStatus(StudentAccommodationRequestId, NewStatus);
			Task.Run(delegate()
			{
				StudentAccommodationRequestManager.<>c__DisplayClass24_0.<<UpdateRequestStatus>b__0>d <<UpdateRequestStatus>b__0>d = new StudentAccommodationRequestManager.<>c__DisplayClass24_0.<<UpdateRequestStatus>b__0>d();
				<<UpdateRequestStatus>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<UpdateRequestStatus>b__0>d.<>4__this = CS$<>8__locals1;
				<<UpdateRequestStatus>b__0>d.<>1__state = -1;
				<<UpdateRequestStatus>b__0>d.<>t__builder.Start<StudentAccommodationRequestManager.<>c__DisplayClass24_0.<<UpdateRequestStatus>b__0>d>(ref <<UpdateRequestStatus>b__0>d);
				return <<UpdateRequestStatus>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000FE98 File Offset: 0x0000E098
		public StudentCourseAccommodationRequestHistory LoadStudentCourseAccommodationRequestHistory(int PersonId, int LuCourseId)
		{
			return this.dao.LoadStudentCourseAccommodationRequestHistory(PersonId, LuCourseId);
		}

		// Token: 0x0400007E RID: 126
		private IAccommodationsManager _am;
	}
}
