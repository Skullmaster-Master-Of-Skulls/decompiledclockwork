using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006DE RID: 1758
	[DataContract(Namespace = "http://tpro.ca")]
	public class SendFormBackToTraineeReq : BaseMessageReq
	{
		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x060023F5 RID: 9205 RVA: 0x000106D7 File Offset: 0x0000E8D7
		// (set) Token: 0x060023F6 RID: 9206 RVA: 0x000106DF File Offset: 0x0000E8DF
		[DataMember]
		public Guid FormApprovalId { get; set; }

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x060023F7 RID: 9207 RVA: 0x000106E8 File Offset: 0x0000E8E8
		// (set) Token: 0x060023F8 RID: 9208 RVA: 0x000106F0 File Offset: 0x0000E8F0
		[DataMember]
		public FormApprovalCommentTextDTO Comment { get; set; }
	}
}
