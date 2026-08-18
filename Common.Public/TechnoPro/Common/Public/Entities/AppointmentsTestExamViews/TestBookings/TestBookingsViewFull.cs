using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.TestBookings
{
	// Token: 0x020004F5 RID: 1269
	public class TestBookingsViewFull : TestBookingsViewBase
	{
		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x00028F61 File Offset: 0x00027161
		// (set) Token: 0x06002666 RID: 9830 RVA: 0x00028F69 File Offset: 0x00027169
		public string MemoPlainText { get; set; }

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06002667 RID: 9831 RVA: 0x00028F72 File Offset: 0x00027172
		// (set) Token: 0x06002668 RID: 9832 RVA: 0x00028F7A File Offset: 0x0002717A
		public bool InstructorSubmittedClassTestInfo { get; set; }

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x00028F83 File Offset: 0x00027183
		// (set) Token: 0x0600266A RID: 9834 RVA: 0x00028F8B File Offset: 0x0002718B
		public LookupCourse Course { get; set; }

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x0600266B RID: 9835 RVA: 0x00028F94 File Offset: 0x00027194
		public override string CourseTitle
		{
			get
			{
				LookupCourse course = this.Course;
				return (course != null) ? course.GetCourseDescription() : null;
			}
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x00028FA8 File Offset: 0x000271A8
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x00028FB0 File Offset: 0x000271B0
		public string BookingNote { get; set; }

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x00028FB9 File Offset: 0x000271B9
		// (set) Token: 0x0600266F RID: 9839 RVA: 0x00028FC1 File Offset: 0x000271C1
		public DateTime DateBooked { get; set; }

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x00028FCA File Offset: 0x000271CA
		// (set) Token: 0x06002671 RID: 9841 RVA: 0x00028FD2 File Offset: 0x000271D2
		public BasicPerson WhoBooked { get; set; }

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x00028FDB File Offset: 0x000271DB
		// (set) Token: 0x06002673 RID: 9843 RVA: 0x00028FE3 File Offset: 0x000271E3
		public BasicPerson AssignedAdvisor { get; set; }

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x00028FEC File Offset: 0x000271EC
		// (set) Token: 0x06002675 RID: 9845 RVA: 0x00028FF4 File Offset: 0x000271F4
		public DateTime? ActualStartDateTime { get; set; }

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x00028FFD File Offset: 0x000271FD
		// (set) Token: 0x06002677 RID: 9847 RVA: 0x00029005 File Offset: 0x00027205
		public DateTime? ActualEndDateTime { get; set; }

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x0002900E File Offset: 0x0002720E
		// (set) Token: 0x06002679 RID: 9849 RVA: 0x00029016 File Offset: 0x00027216
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x0002901F File Offset: 0x0002721F
		// (set) Token: 0x0600267B RID: 9851 RVA: 0x00029027 File Offset: 0x00027227
		public string InstructorContactedNote { get; set; }

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x00029030 File Offset: 0x00027230
		// (set) Token: 0x0600267D RID: 9853 RVA: 0x00029038 File Offset: 0x00027238
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x00029041 File Offset: 0x00027241
		// (set) Token: 0x0600267F RID: 9855 RVA: 0x00029049 File Offset: 0x00027249
		public string TestPickedUpNote { get; set; }

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x00029052 File Offset: 0x00027252
		// (set) Token: 0x06002681 RID: 9857 RVA: 0x0002905A File Offset: 0x0002725A
		public string PrivateNote2 { get; set; }

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x00029063 File Offset: 0x00027263
		// (set) Token: 0x06002683 RID: 9859 RVA: 0x0002906B File Offset: 0x0002726B
		public string ClassLocation { get; set; }

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06002684 RID: 9860 RVA: 0x00029074 File Offset: 0x00027274
		// (set) Token: 0x06002685 RID: 9861 RVA: 0x0002907C File Offset: 0x0002727C
		public SittingBase Sitting { get; set; }

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x00029085 File Offset: 0x00027285
		// (set) Token: 0x06002687 RID: 9863 RVA: 0x0002908D File Offset: 0x0002728D
		public IList<BasicPerson> Proctors { get; set; }

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x00029096 File Offset: 0x00027296
		// (set) Token: 0x06002689 RID: 9865 RVA: 0x0002909E File Offset: 0x0002729E
		public DateTime? DateAccommodationLetterIssued { get; set; }
	}
}
