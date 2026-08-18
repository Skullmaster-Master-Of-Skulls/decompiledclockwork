using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management
{
	// Token: 0x02000814 RID: 2068
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupInstructorCourseStudentAttachmentForManagementDTO
	{
		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x00014062 File Offset: 0x00012262
		// (set) Token: 0x06002A28 RID: 10792 RVA: 0x0001406A File Offset: 0x0001226A
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x00014073 File Offset: 0x00012273
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x0001407B File Offset: 0x0001227B
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x00014084 File Offset: 0x00012284
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x0001408C File Offset: 0x0001228C
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x00014095 File Offset: 0x00012295
		// (set) Token: 0x06002A2E RID: 10798 RVA: 0x0001409D File Offset: 0x0001229D
		[DataMember]
		public bool IsCourseDropped { get; set; }
	}
}
