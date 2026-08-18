using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006C5 RID: 1733
	[DataContract(Namespace = "http://tpro.ca")]
	public class FormApprovalCommentDTO
	{
		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x000102EC File Offset: 0x0000E4EC
		// (set) Token: 0x06002367 RID: 9063 RVA: 0x000102F4 File Offset: 0x0000E4F4
		[DataMember]
		public Guid FormApprovalCommentId { get; set; }

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x000102FD File Offset: 0x0000E4FD
		// (set) Token: 0x06002369 RID: 9065 RVA: 0x00010305 File Offset: 0x0000E505
		[DataMember]
		public FormApprovalCommentTextDTO Comment { get; set; }

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x0001030E File Offset: 0x0000E50E
		// (set) Token: 0x0600236B RID: 9067 RVA: 0x00010316 File Offset: 0x0000E516
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x0001031F File Offset: 0x0000E51F
		// (set) Token: 0x0600236D RID: 9069 RVA: 0x00010327 File Offset: 0x0000E527
		[DataMember]
		public BasicPersonDTO WhoEntered { get; set; }
	}
}
