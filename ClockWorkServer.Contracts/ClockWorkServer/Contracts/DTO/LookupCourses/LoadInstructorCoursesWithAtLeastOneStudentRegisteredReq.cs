using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007F8 RID: 2040
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorCoursesWithAtLeastOneStudentRegisteredReq : BaseMessageReq
	{
		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06002991 RID: 10641 RVA: 0x00013B87 File Offset: 0x00011D87
		// (set) Token: 0x06002992 RID: 10642 RVA: 0x00013B8F File Offset: 0x00011D8F
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06002993 RID: 10643 RVA: 0x00013B98 File Offset: 0x00011D98
		// (set) Token: 0x06002994 RID: 10644 RVA: 0x00013BA0 File Offset: 0x00011DA0
		[DataMember]
		public int AlternateContactId { get; set; }

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06002995 RID: 10645 RVA: 0x00013BA9 File Offset: 0x00011DA9
		// (set) Token: 0x06002996 RID: 10646 RVA: 0x00013BB1 File Offset: 0x00011DB1
		[DataMember]
		public int PermissionLevel { get; set; }

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06002997 RID: 10647 RVA: 0x00013BBA File Offset: 0x00011DBA
		// (set) Token: 0x06002998 RID: 10648 RVA: 0x00013BC2 File Offset: 0x00011DC2
		[DataMember]
		public bool MustHaveClassTestDefinition { get; set; }

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06002999 RID: 10649 RVA: 0x00013BCB File Offset: 0x00011DCB
		// (set) Token: 0x0600299A RID: 10650 RVA: 0x00013BD3 File Offset: 0x00011DD3
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x0600299B RID: 10651 RVA: 0x00013BDC File Offset: 0x00011DDC
		// (set) Token: 0x0600299C RID: 10652 RVA: 0x00013BE4 File Offset: 0x00011DE4
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
