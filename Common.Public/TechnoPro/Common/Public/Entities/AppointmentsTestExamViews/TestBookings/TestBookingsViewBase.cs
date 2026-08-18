using System;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.TestBookings
{
	// Token: 0x020004F3 RID: 1267
	public class TestBookingsViewBase : BusinessBase<int>
	{
		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x00028DA0 File Offset: 0x00026FA0
		// (set) Token: 0x06002630 RID: 9776 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x00028DB8 File Offset: 0x00026FB8
		// (set) Token: 0x06002632 RID: 9778 RVA: 0x00028DC0 File Offset: 0x00026FC0
		public BasicPerson Student { get; set; }

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x00028DC9 File Offset: 0x00026FC9
		// (set) Token: 0x06002634 RID: 9780 RVA: 0x00028DD1 File Offset: 0x00026FD1
		public int ExamId { get; set; }

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x00028DDA File Offset: 0x00026FDA
		// (set) Token: 0x06002636 RID: 9782 RVA: 0x00028DE2 File Offset: 0x00026FE2
		public int LuCourseId { get; set; }

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x00028DEB File Offset: 0x00026FEB
		// (set) Token: 0x06002638 RID: 9784 RVA: 0x00028DF3 File Offset: 0x00026FF3
		public DateTime ScheduledStartDateTime { get; set; }

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x00028DFC File Offset: 0x00026FFC
		// (set) Token: 0x0600263A RID: 9786 RVA: 0x00028E04 File Offset: 0x00027004
		public DateTime ScheduledEndDateTime { get; set; }

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x00028E0D File Offset: 0x0002700D
		// (set) Token: 0x0600263C RID: 9788 RVA: 0x00028E15 File Offset: 0x00027015
		public bool IsCancelled { get; set; }

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x00028E1E File Offset: 0x0002701E
		// (set) Token: 0x0600263E RID: 9790 RVA: 0x00028E26 File Offset: 0x00027026
		public bool IsNoShow { get; set; }

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x00028E2F File Offset: 0x0002702F
		// (set) Token: 0x06002640 RID: 9792 RVA: 0x00028E37 File Offset: 0x00027037
		public bool IsTentative { get; set; }

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x00028E40 File Offset: 0x00027040
		// (set) Token: 0x06002642 RID: 9794 RVA: 0x00028E48 File Offset: 0x00027048
		public virtual string CourseTitle { get; set; }

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x00028E51 File Offset: 0x00027051
		// (set) Token: 0x06002644 RID: 9796 RVA: 0x00028E59 File Offset: 0x00027059
		public virtual eClassTestType ClassTestType { get; set; }

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x00028E62 File Offset: 0x00027062
		// (set) Token: 0x06002646 RID: 9798 RVA: 0x00028E6A File Offset: 0x0002706A
		public AppTypeBase AppointmentTypeBase { get; set; }

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x00028E73 File Offset: 0x00027073
		// (set) Token: 0x06002648 RID: 9800 RVA: 0x00028E7B File Offset: 0x0002707B
		public eTestBookingsStatus Status { get; set; }

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x00028E84 File Offset: 0x00027084
		// (set) Token: 0x0600264A RID: 9802 RVA: 0x00028E8C File Offset: 0x0002708C
		public TestLabel Label { get; set; }

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x00028E95 File Offset: 0x00027095
		// (set) Token: 0x0600264C RID: 9804 RVA: 0x00028E9D File Offset: 0x0002709D
		public AppointmentRoom Room { get; set; }

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x00028EA6 File Offset: 0x000270A6
		// (set) Token: 0x0600264E RID: 9806 RVA: 0x00028EAE File Offset: 0x000270AE
		public string Location { get; set; }

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x00028EB7 File Offset: 0x000270B7
		// (set) Token: 0x06002650 RID: 9808 RVA: 0x00028EBF File Offset: 0x000270BF
		public bool HasTestCopy { get; set; }

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x00028EC8 File Offset: 0x000270C8
		// (set) Token: 0x06002652 RID: 9810 RVA: 0x00028ED0 File Offset: 0x000270D0
		public string TestCopyNote { get; set; }

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x00028ED9 File Offset: 0x000270D9
		// (set) Token: 0x06002654 RID: 9812 RVA: 0x00028EE1 File Offset: 0x000270E1
		public DateTime ClassTestStartDateTime { get; set; }

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x00028EEA File Offset: 0x000270EA
		// (set) Token: 0x06002656 RID: 9814 RVA: 0x00028EF2 File Offset: 0x000270F2
		public DateTime ClassTestEndDateTime { get; set; }

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06002657 RID: 9815 RVA: 0x00028EFB File Offset: 0x000270FB
		// (set) Token: 0x06002658 RID: 9816 RVA: 0x00028F03 File Offset: 0x00027103
		public DateTime StudentReportedClassTestStartDateTime { get; set; }

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x00028F0C File Offset: 0x0002710C
		// (set) Token: 0x0600265A RID: 9818 RVA: 0x00028F14 File Offset: 0x00027114
		public DateTime StudentReportedClassTestEndDateTime { get; set; }

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x00028F1D File Offset: 0x0002711D
		// (set) Token: 0x0600265C RID: 9820 RVA: 0x00028F25 File Offset: 0x00027125
		public int TotalBreakMinutes { get; set; }
	}
}
