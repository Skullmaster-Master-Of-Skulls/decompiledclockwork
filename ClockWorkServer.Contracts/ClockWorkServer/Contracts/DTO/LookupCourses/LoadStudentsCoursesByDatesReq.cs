using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007CD RID: 1997
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsCoursesByDatesReq : BaseMessageReq
	{
		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060028D0 RID: 10448 RVA: 0x00013548 File Offset: 0x00011748
		// (set) Token: 0x060028D1 RID: 10449 RVA: 0x00013550 File Offset: 0x00011750
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x00013559 File Offset: 0x00011759
		// (set) Token: 0x060028D3 RID: 10451 RVA: 0x00013561 File Offset: 0x00011761
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x0001356A File Offset: 0x0001176A
		// (set) Token: 0x060028D5 RID: 10453 RVA: 0x00013572 File Offset: 0x00011772
		[DataMember]
		public int PersonId { get; set; }
	}
}
