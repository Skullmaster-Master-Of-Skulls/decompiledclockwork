using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006E6 RID: 1766
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddFormApprovalCommentForTraineeReq : BaseMessageReq
	{
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x00010781 File Offset: 0x0000E981
		// (set) Token: 0x06002412 RID: 9234 RVA: 0x00010789 File Offset: 0x0000E989
		[DataMember]
		public Guid FormApprovalId { get; set; }

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x00010792 File Offset: 0x0000E992
		// (set) Token: 0x06002414 RID: 9236 RVA: 0x0001079A File Offset: 0x0000E99A
		[DataMember]
		public FormApprovalCommentTextDTO Comment { get; set; }
	}
}
