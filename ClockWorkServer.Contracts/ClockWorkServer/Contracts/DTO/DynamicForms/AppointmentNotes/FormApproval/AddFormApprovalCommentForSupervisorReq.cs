using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006DA RID: 1754
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddFormApprovalCommentForSupervisorReq : BaseMessageReq
	{
		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x00010682 File Offset: 0x0000E882
		// (set) Token: 0x060023E8 RID: 9192 RVA: 0x0001068A File Offset: 0x0000E88A
		[DataMember]
		public Guid FormApprovalId { get; set; }

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x00010693 File Offset: 0x0000E893
		// (set) Token: 0x060023EA RID: 9194 RVA: 0x0001069B File Offset: 0x0000E89B
		[DataMember]
		public FormApprovalCommentTextDTO Comment { get; set; }
	}
}
