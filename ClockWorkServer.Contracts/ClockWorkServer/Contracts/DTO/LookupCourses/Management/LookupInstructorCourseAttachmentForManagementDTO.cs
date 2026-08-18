using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management
{
	// Token: 0x02000813 RID: 2067
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupInstructorCourseAttachmentForManagementDTO
	{
		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x00013FFC File Offset: 0x000121FC
		// (set) Token: 0x06002A1B RID: 10779 RVA: 0x00014004 File Offset: 0x00012204
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x0001400D File Offset: 0x0001220D
		// (set) Token: 0x06002A1D RID: 10781 RVA: 0x00014015 File Offset: 0x00012215
		[DataMember]
		public string CourseDescription { get; set; }

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x0001401E File Offset: 0x0001221E
		// (set) Token: 0x06002A1F RID: 10783 RVA: 0x00014026 File Offset: 0x00012226
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x0001402F File Offset: 0x0001222F
		// (set) Token: 0x06002A21 RID: 10785 RVA: 0x00014037 File Offset: 0x00012237
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x00014040 File Offset: 0x00012240
		// (set) Token: 0x06002A23 RID: 10787 RVA: 0x00014048 File Offset: 0x00012248
		[DataMember]
		public bool IsInstructorExemptFromDataSyncAssignment { get; set; }

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06002A24 RID: 10788 RVA: 0x00014051 File Offset: 0x00012251
		// (set) Token: 0x06002A25 RID: 10789 RVA: 0x00014059 File Offset: 0x00012259
		[DataMember]
		public IList<LookupInstructorCourseStudentAttachmentForManagementDTO> Students { get; set; }
	}
}
