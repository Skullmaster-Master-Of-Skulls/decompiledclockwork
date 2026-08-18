using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006EA RID: 1770
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateFormApprovalFormReq : BaseMessageReq
	{
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x000107C5 File Offset: 0x0000E9C5
		// (set) Token: 0x0600241E RID: 9246 RVA: 0x000107CD File Offset: 0x0000E9CD
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x000107D6 File Offset: 0x0000E9D6
		// (set) Token: 0x06002420 RID: 9248 RVA: 0x000107DE File Offset: 0x0000E9DE
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x000107E7 File Offset: 0x0000E9E7
		// (set) Token: 0x06002422 RID: 9250 RVA: 0x000107EF File Offset: 0x0000E9EF
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x000107F8 File Offset: 0x0000E9F8
		// (set) Token: 0x06002424 RID: 9252 RVA: 0x00010800 File Offset: 0x0000EA00
		[DataMember]
		public FormApprovalCommentTextDTO Comment { get; set; }

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x00010809 File Offset: 0x0000EA09
		// (set) Token: 0x06002426 RID: 9254 RVA: 0x00010811 File Offset: 0x0000EA11
		[DataMember]
		public FormApprovalSignatureDTO TraineeSignature { get; set; }
	}
}
