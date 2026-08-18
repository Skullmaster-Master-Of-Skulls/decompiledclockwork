using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006C6 RID: 1734
	[DataContract(Namespace = "http://tpro.ca")]
	public class FormApprovalCommentTextDTO
	{
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x00010330 File Offset: 0x0000E530
		// (set) Token: 0x06002370 RID: 9072 RVA: 0x00010338 File Offset: 0x0000E538
		[DataMember]
		public string CommentText { get; set; }
	}
}
