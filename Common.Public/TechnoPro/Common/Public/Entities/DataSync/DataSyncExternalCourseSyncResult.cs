using System;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Public.Entities.DataSync
{
	// Token: 0x020003D4 RID: 980
	public class DataSyncExternalCourseSyncResult
	{
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x00021CDA File Offset: 0x0001FEDA
		// (set) Token: 0x06001E33 RID: 7731 RVA: 0x00021CE2 File Offset: 0x0001FEE2
		public int Lucid { get; set; }

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x00021CEB File Offset: 0x0001FEEB
		// (set) Token: 0x06001E35 RID: 7733 RVA: 0x00021CF3 File Offset: 0x0001FEF3
		public int InstructorId { get; set; }

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x00021CFC File Offset: 0x0001FEFC
		// (set) Token: 0x06001E37 RID: 7735 RVA: 0x00021D04 File Offset: 0x0001FF04
		public DataSyncExternalCourse ExternalCourse { get; set; }

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x00021D0D File Offset: 0x0001FF0D
		// (set) Token: 0x06001E39 RID: 7737 RVA: 0x00021D15 File Offset: 0x0001FF15
		public eDataSyncCourseRegistrationAction CourseRegistrationAction { get; set; }

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06001E3A RID: 7738 RVA: 0x00021D1E File Offset: 0x0001FF1E
		// (set) Token: 0x06001E3B RID: 7739 RVA: 0x00021D26 File Offset: 0x0001FF26
		public eDataSyncCourseLookupCourseAction LookupCourseAction { get; set; }

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x00021D2F File Offset: 0x0001FF2F
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x00021D37 File Offset: 0x0001FF37
		public eDataSyncCourseInstructorAction InstructorAction { get; set; }

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x00021D40 File Offset: 0x0001FF40
		// (set) Token: 0x06001E3F RID: 7743 RVA: 0x00021D48 File Offset: 0x0001FF48
		public eDataSyncCourseMiscAction MiscAction { get; set; }

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x00021D51 File Offset: 0x0001FF51
		// (set) Token: 0x06001E41 RID: 7745 RVA: 0x00021D59 File Offset: 0x0001FF59
		public eDataSyncCourseError ErrorAction { get; set; }

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x00021D62 File Offset: 0x0001FF62
		// (set) Token: 0x06001E43 RID: 7747 RVA: 0x00021D6A File Offset: 0x0001FF6A
		public ClassTestBase FinalExamClassTestBase { get; set; }

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x00021D73 File Offset: 0x0001FF73
		// (set) Token: 0x06001E45 RID: 7749 RVA: 0x00021D7B File Offset: 0x0001FF7B
		public string Msg { get; set; }
	}
}
