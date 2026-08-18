using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006E0 RID: 1760
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnApproveFormApprovalReq : BaseMessageReq
	{
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x000106F9 File Offset: 0x0000E8F9
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x00010701 File Offset: 0x0000E901
		[DataMember]
		public Guid FormApprovalId { get; set; }

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x0001070A File Offset: 0x0000E90A
		// (set) Token: 0x060023FE RID: 9214 RVA: 0x00010712 File Offset: 0x0000E912
		[DataMember]
		public FormApprovalCommentTextDTO CommentText { get; set; }
	}
}
