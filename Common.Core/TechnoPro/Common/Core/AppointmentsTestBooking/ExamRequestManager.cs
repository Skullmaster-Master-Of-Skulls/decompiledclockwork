using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.CourseRegistrations;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.CourseRegistrations;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x0200013E RID: 318
	public class ExamRequestManager : IExamRequestManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000E10 RID: 3600 RVA: 0x00069CBE File Offset: 0x00067EBE
		public ExamRequestManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ExamRequestDAO(opContext);
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00069CDC File Offset: 0x00067EDC
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x00069CE4 File Offset: 0x00067EE4
		public OperationContext OpContext { get; set; }

		// Token: 0x06000E13 RID: 3603 RVA: 0x00069CF0 File Offset: 0x00067EF0
		public IList<ExamRequest> LoadRequestsByDateRange(DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadRequestsByDateRange(StartDate, EndDate);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00069D10 File Offset: 0x00067F10
		public int CreateExamRequest(int PersonId, int LuCourseId)
		{
			return this.dao.CreateExamRequest(PersonId, LuCourseId);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00069D2F File Offset: 0x00067F2F
		public void DeleteExamRequest(int ExamRequestId)
		{
			this.dao.DeleteExamRequest(ExamRequestId);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00069D40 File Offset: 0x00067F40
		public IList<ExamRequest> LoadRequestsByCourse(int LuCourseId)
		{
			return this.dao.LoadRequestsByCourse(LuCourseId);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00069D60 File Offset: 0x00067F60
		public IList<PersonBase> LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(int LuCourseId, out IList<int> PersonIdsWhoSubmittedExamRequest)
		{
			IList<ExamRequest> source = this.LoadRequestsByCourse(LuCourseId);
			PersonIdsWhoSubmittedExamRequest = (from g in source
			select (g.Student == null) ? 0 : g.Student.PersonId).Distinct<int>().ToList<int>();
			ICourseRegistrationManager courseRegistrationManager = new CourseRegistrationManager(this.OpContext);
			return (from g in courseRegistrationManager.LoadCourseRegistrationsByCourse(LuCourseId)
			where g.RegistrationStatus != eRegistrationStatus.Dropped
			select g into h
			select h.Student).ToList<PersonBase>();
		}

		// Token: 0x0400029A RID: 666
		private IExamRequestDAO dao;
	}
}
