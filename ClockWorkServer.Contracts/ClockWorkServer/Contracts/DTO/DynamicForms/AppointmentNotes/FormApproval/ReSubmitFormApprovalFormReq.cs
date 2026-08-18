using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006E8 RID: 1768
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReSubmitFormApprovalFormReq : BaseMessageReq
	{
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x000107A3 File Offset: 0x0000E9A3
		// (set) Token: 0x06002418 RID: 9240 RVA: 0x000107AB File Offset: 0x0000E9AB
		[DataMember]
		public Guid FormApprovalId { get; set; }

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x000107B4 File Offset: 0x0000E9B4
		// (set) Token: 0x0600241A RID: 9242 RVA: 0x000107BC File Offset: 0x0000E9BC
		[DataMember]
		public FormApprovalCommentTextDTO Comment { get; set; }
	}
}
