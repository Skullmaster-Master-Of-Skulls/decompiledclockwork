using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006DC RID: 1756
	[DataContract(Namespace = "http://tpro.ca")]
	public class ApproveFormReq : BaseMessageReq
	{
		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x000106A4 File Offset: 0x0000E8A4
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x000106AC File Offset: 0x0000E8AC
		[DataMember]
		public Guid FormApprovalId { get; set; }

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x000106B5 File Offset: 0x0000E8B5
		// (set) Token: 0x060023F0 RID: 9200 RVA: 0x000106BD File Offset: 0x0000E8BD
		[DataMember]
		public FormApprovalCommentTextDTO Comment { get; set; }

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x000106C6 File Offset: 0x0000E8C6
		// (set) Token: 0x060023F2 RID: 9202 RVA: 0x000106CE File Offset: 0x0000E8CE
		[DataMember]
		public FormApprovalSignatureDTO SupervisorSignature { get; set; }
	}
}
