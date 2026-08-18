using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.ClockWorkWeb.user.instructor
{
	// Token: 0x020000D2 RID: 210
	public class StudentInTestWrapper : WrapperBase<StudentWritingTestDTO>
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x0002FA45 File Offset: 0x0002DC45
		public StudentInTestWrapper()
		{
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0002FA4F File Offset: 0x0002DC4F
		public StudentInTestWrapper(StudentWritingTestDTO studentWritingTest) : base(studentWritingTest)
		{
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0002FA5A File Offset: 0x0002DC5A
		public int appointmentid
		{
			get
			{
				StudentWritingTestDTO item = base.Item;
				return (item != null) ? item.AppointmentId : 0;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0002FA70 File Offset: 0x0002DC70
		public bool? InstructorAcknowledged
		{
			get
			{
				StudentWritingTestDTO item = base.Item;
				return (item != null) ? item.InstructorAcknowledgedValue : null;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0002FA98 File Offset: 0x0002DC98
		public int InstructorAcknowledgedValue
		{
			get
			{
				return (this.InstructorAcknowledged != null) ? (this.InstructorAcknowledged.Value ? 1 : 2) : 0;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0002FACC File Offset: 0x0002DCCC
		public string firstname
		{
			get
			{
				StudentWritingTestDTO item = base.Item;
				string text;
				if (item == null)
				{
					text = null;
				}
				else
				{
					PersonBaseDTO student = item.Student;
					text = ((student != null) ? student.FirstName : null);
				}
				return text ?? "";
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0002FAF5 File Offset: 0x0002DCF5
		public string lastname
		{
			get
			{
				StudentWritingTestDTO item = base.Item;
				string text;
				if (item == null)
				{
					text = null;
				}
				else
				{
					PersonBaseDTO student = item.Student;
					text = ((student != null) ? student.LastName : null);
				}
				return text ?? "";
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0002FB1E File Offset: 0x0002DD1E
		public string student_no
		{
			get
			{
				StudentWritingTestDTO item = base.Item;
				string text;
				if (item == null)
				{
					text = null;
				}
				else
				{
					PersonBaseDTO student = item.Student;
					text = ((student != null) ? student.Student_no : null);
				}
				return text ?? "";
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0002FB47 File Offset: 0x0002DD47
		public DateTime startdate
		{
			get
			{
				StudentWritingTestDTO item = base.Item;
				return (item != null) ? item.StartDateTime : DateTime.Now;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0002FB5F File Offset: 0x0002DD5F
		public DateTime enddate
		{
			get
			{
				StudentWritingTestDTO item = base.Item;
				return (item != null) ? item.EndDateTime : DateTime.Now;
			}
		}
	}
}
