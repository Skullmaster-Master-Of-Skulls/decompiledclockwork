using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x02000140 RID: 320
	public class StudentClassTestInfoManager : IStudentClassTestInfoManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000E23 RID: 3619 RVA: 0x00069F3C File Offset: 0x0006813C
		public StudentClassTestInfoManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new StudentClassTestInfoDAO(opContext);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00069F5A File Offset: 0x0006815A
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x00069F62 File Offset: 0x00068162
		public OperationContext OpContext { get; set; }

		// Token: 0x06000E26 RID: 3622 RVA: 0x00069F6B File Offset: 0x0006816B
		public void DeleteStudentClassTestInfo(int AppointmentCourseId)
		{
			this.dao.DeleteStudentClassTestInfo(AppointmentCourseId);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00069F7B File Offset: 0x0006817B
		public void UpdateExamStatus(int AppointmentId, int NewExamStatusLookupId)
		{
			this.dao.UpdateExamStatus(AppointmentId, NewExamStatusLookupId);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00069F8C File Offset: 0x0006818C
		public StudentClassTest LoadClassTestByAppointmentId(int AppointmentId)
		{
			return this.dao.LoadClassTestByAppointmentId(AppointmentId);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00069FAC File Offset: 0x000681AC
		public IDictionary<int, StudentClassTest> LoadClassTestsByAppointmentIds(params int[] appointmentIds)
		{
			int[] array;
			if (appointmentIds == null)
			{
				array = null;
			}
			else
			{
				array = (from g in appointmentIds.Distinct<int>()
				where g > 0
				select g).ToArray<int>();
			}
			int[] array2 = array ?? new int[0];
			bool flag = array2.Length < 1;
			IDictionary<int, StudentClassTest> result;
			if (flag)
			{
				result = new Dictionary<int, StudentClassTest>();
			}
			else
			{
				result = this.dao.LoadClassTestsByAppointmentIds(array2);
			}
			return result;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0006A01C File Offset: 0x0006821C
		public ExamStatus LoadExamStatusByAppointmentId(int AppointmentId)
		{
			return this.dao.LoadExamStatusByAppointmentId(AppointmentId);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0006A03A File Offset: 0x0006823A
		public void UpdateBookingAndPrivateNote(int AppointmentId, string BookingNote, string PrivateNote)
		{
			this.UpdateBookingNote(AppointmentId, BookingNote);
			this.UpdatePrivateNote(AppointmentId, PrivateNote);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0006A04F File Offset: 0x0006824F
		public void UpdateBookingNote(int AppointmentId, string BookingNote)
		{
			this.dao.UpdateBookingNote(AppointmentId, BookingNote);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0006A060 File Offset: 0x00068260
		public void UpdatePrivateNote(int AppointmentId, string PrivateNote)
		{
			this.dao.UpdatePrivateNote(AppointmentId, PrivateNote);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0006A071 File Offset: 0x00068271
		public void UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(int AppointmentId)
		{
			this.dao.UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(AppointmentId);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0006A084 File Offset: 0x00068284
		public int CreateStudentClassTest(int AppointmentId, StudentClassTest StudentClassTest)
		{
			return this.dao.CreateStudentClassTest(AppointmentId, StudentClassTest);
		}

		// Token: 0x0400029E RID: 670
		private IStudentClassTestInfoDAO dao;
	}
}
