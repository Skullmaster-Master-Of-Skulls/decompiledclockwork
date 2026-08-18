using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006D8 RID: 1752
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormApprovalForSupervisorReq : BaseMessageReq
	{
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x0001063E File Offset: 0x0000E83E
		// (set) Token: 0x060023DE RID: 9182 RVA: 0x00010646 File Offset: 0x0000E846
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x0001064F File Offset: 0x0000E84F
		// (set) Token: 0x060023E0 RID: 9184 RVA: 0x00010657 File Offset: 0x0000E857
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x00010660 File Offset: 0x0000E860
		// (set) Token: 0x060023E2 RID: 9186 RVA: 0x00010668 File Offset: 0x0000E868
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
