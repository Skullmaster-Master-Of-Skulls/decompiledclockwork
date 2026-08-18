using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007CB RID: 1995
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsCoursesBySessionReq : BaseMessageReq
	{
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060028C8 RID: 10440 RVA: 0x00013515 File Offset: 0x00011715
		// (set) Token: 0x060028C9 RID: 10441 RVA: 0x0001351D File Offset: 0x0001171D
		[DataMember]
		public SessionDTO Session { get; set; }

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x00013526 File Offset: 0x00011726
		// (set) Token: 0x060028CB RID: 10443 RVA: 0x0001352E File Offset: 0x0001172E
		[DataMember]
		public int PersonId { get; set; }
	}
}
