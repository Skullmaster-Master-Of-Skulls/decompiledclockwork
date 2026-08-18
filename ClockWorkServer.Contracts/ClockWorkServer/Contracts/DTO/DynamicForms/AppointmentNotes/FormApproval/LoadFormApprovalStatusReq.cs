using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006CD RID: 1741
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormApprovalStatusReq : BaseMessageReq
	{
		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x00010440 File Offset: 0x0000E640
		// (set) Token: 0x06002397 RID: 9111 RVA: 0x00010448 File Offset: 0x0000E648
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x00010451 File Offset: 0x0000E651
		// (set) Token: 0x06002399 RID: 9113 RVA: 0x00010459 File Offset: 0x0000E659
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x00010462 File Offset: 0x0000E662
		// (set) Token: 0x0600239B RID: 9115 RVA: 0x0001046A File Offset: 0x0000E66A
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
