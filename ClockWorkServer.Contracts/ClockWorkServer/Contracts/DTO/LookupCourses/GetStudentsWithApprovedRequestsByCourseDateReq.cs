using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007FA RID: 2042
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetStudentsWithApprovedRequestsByCourseDateReq : BaseMessageReq
	{
		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x00013BFE File Offset: 0x00011DFE
		// (set) Token: 0x060029A2 RID: 10658 RVA: 0x00013C06 File Offset: 0x00011E06
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x00013C0F File Offset: 0x00011E0F
		// (set) Token: 0x060029A4 RID: 10660 RVA: 0x00013C17 File Offset: 0x00011E17
		[DataMember]
		public int AlternateContactId { get; set; }

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x00013C20 File Offset: 0x00011E20
		// (set) Token: 0x060029A6 RID: 10662 RVA: 0x00013C28 File Offset: 0x00011E28
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x060029A7 RID: 10663 RVA: 0x00013C31 File Offset: 0x00011E31
		// (set) Token: 0x060029A8 RID: 10664 RVA: 0x00013C39 File Offset: 0x00011E39
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x060029A9 RID: 10665 RVA: 0x00013C42 File Offset: 0x00011E42
		// (set) Token: 0x060029AA RID: 10666 RVA: 0x00013C4A File Offset: 0x00011E4A
		[DataMember]
		public string ClockWorkSettingsInstanceName { get; set; }
	}
}
