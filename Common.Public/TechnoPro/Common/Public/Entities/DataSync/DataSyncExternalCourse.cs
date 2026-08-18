using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.DataSync
{
	// Token: 0x020003C9 RID: 969
	public class DataSyncExternalCourse : BusinessBase<string>
	{
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x00021526 File Offset: 0x0001F726
		// (set) Token: 0x06001D9C RID: 7580 RVA: 0x0002152E File Offset: 0x0001F72E
		public virtual string ExternalCourseId { get; set; }

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x00021537 File Offset: 0x0001F737
		// (set) Token: 0x06001D9E RID: 7582 RVA: 0x0002153F File Offset: 0x0001F73F
		public DateTime StartDate { get; set; }

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x00021548 File Offset: 0x0001F748
		// (set) Token: 0x06001DA0 RID: 7584 RVA: 0x00021550 File Offset: 0x0001F750
		public DateTime EndDate { get; set; }

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x00021559 File Offset: 0x0001F759
		// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x00021561 File Offset: 0x0001F761
		public string Duration { get; set; }

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x0002156A File Offset: 0x0001F76A
		// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x00021572 File Offset: 0x0001F772
		public string Term { get; set; }

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x0002157B File Offset: 0x0001F77B
		// (set) Token: 0x06001DA6 RID: 7590 RVA: 0x00021583 File Offset: 0x0001F783
		public string Subject { get; set; }

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x0002158C File Offset: 0x0001F78C
		// (set) Token: 0x06001DA8 RID: 7592 RVA: 0x00021594 File Offset: 0x0001F794
		public string SubjectLong { get; set; }

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x0002159D File Offset: 0x0001F79D
		// (set) Token: 0x06001DAA RID: 7594 RVA: 0x000215A5 File Offset: 0x0001F7A5
		public string Course { get; set; }

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x000215AE File Offset: 0x0001F7AE
		// (set) Token: 0x06001DAC RID: 7596 RVA: 0x000215B6 File Offset: 0x0001F7B6
		public string Section { get; set; }

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x000215BF File Offset: 0x0001F7BF
		// (set) Token: 0x06001DAE RID: 7598 RVA: 0x000215C7 File Offset: 0x0001F7C7
		public string TimeOfDay { get; set; }

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x000215D0 File Offset: 0x0001F7D0
		// (set) Token: 0x06001DB0 RID: 7600 RVA: 0x000215D8 File Offset: 0x0001F7D8
		public string Campus { get; set; }

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x000215E1 File Offset: 0x0001F7E1
		// (set) Token: 0x06001DB2 RID: 7602 RVA: 0x000215E9 File Offset: 0x0001F7E9
		public string Department { get; set; }

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x000215F2 File Offset: 0x0001F7F2
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x000215FA File Offset: 0x0001F7FA
		public string Location { get; set; }

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x00021603 File Offset: 0x0001F803
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x0002160B File Offset: 0x0001F80B
		public string CourseNote { get; set; }

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x00021614 File Offset: 0x0001F814
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x0002161C File Offset: 0x0001F81C
		public List<DataSyncExternalCourseInstructor> Instructors { get; set; }

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x00021625 File Offset: 0x0001F825
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x0002162D File Offset: 0x0001F82D
		public List<DataSyncExternalCourseTimetableItem> TimetableItems { get; set; }

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x00021636 File Offset: 0x0001F836
		// (set) Token: 0x06001DBC RID: 7612 RVA: 0x0002163E File Offset: 0x0001F83E
		public List<DataSyncExternalCourseAltContact> AlternateContacts { get; set; }

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x00021647 File Offset: 0x0001F847
		// (set) Token: 0x06001DBE RID: 7614 RVA: 0x0002164F File Offset: 0x0001F84F
		public LookupCourse MatchingClockWorkLookupCourse { get; set; }

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x00021658 File Offset: 0x0001F858
		// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x00021660 File Offset: 0x0001F860
		public IList<DataSyncExternalCourseFinalExamInfo> FinalExamInfos { get; set; }

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x00021669 File Offset: 0x0001F869
		// (set) Token: 0x06001DC2 RID: 7618 RVA: 0x00021671 File Offset: 0x0001F871
		public decimal Credits { get; set; }

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x0002167A File Offset: 0x0001F87A
		// (set) Token: 0x06001DC4 RID: 7620 RVA: 0x00021682 File Offset: 0x0001F882
		public DataSyncExternalCourseStudentSpecific StudentSpecificInfo { get; set; }
	}
}
