using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006E2 RID: 1762
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSupervisorSignatureReq : BaseMessageReq
	{
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x0001071B File Offset: 0x0000E91B
		// (set) Token: 0x06002402 RID: 9218 RVA: 0x00010723 File Offset: 0x0000E923
		[DataMember]
		public Guid FormApprovalId { get; set; }
	}
}
