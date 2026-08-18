using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007F2 RID: 2034
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorCoursesReq : BaseMessageReq
	{
		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06002977 RID: 10615 RVA: 0x00013ADD File Offset: 0x00011CDD
		// (set) Token: 0x06002978 RID: 10616 RVA: 0x00013AE5 File Offset: 0x00011CE5
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06002979 RID: 10617 RVA: 0x00013AEE File Offset: 0x00011CEE
		// (set) Token: 0x0600297A RID: 10618 RVA: 0x00013AF6 File Offset: 0x00011CF6
		[DataMember]
		public int AlternateContactId { get; set; }

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x00013AFF File Offset: 0x00011CFF
		// (set) Token: 0x0600297C RID: 10620 RVA: 0x00013B07 File Offset: 0x00011D07
		[DataMember]
		public int PermissionLevel { get; set; }

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x00013B10 File Offset: 0x00011D10
		// (set) Token: 0x0600297E RID: 10622 RVA: 0x00013B18 File Offset: 0x00011D18
		[DataMember]
		public bool MustHaveClassTestDefinition { get; set; }

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x0600297F RID: 10623 RVA: 0x00013B21 File Offset: 0x00011D21
		// (set) Token: 0x06002980 RID: 10624 RVA: 0x00013B29 File Offset: 0x00011D29
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x00013B32 File Offset: 0x00011D32
		// (set) Token: 0x06002982 RID: 10626 RVA: 0x00013B3A File Offset: 0x00011D3A
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
