using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006EB RID: 1771
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateFormApprovalFormResp
	{
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x0001081A File Offset: 0x0000EA1A
		// (set) Token: 0x06002429 RID: 9257 RVA: 0x00010822 File Offset: 0x0000EA22
		[DataMember]
		public Guid FormApprovalId { get; set; }
	}
}
