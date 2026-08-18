using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006CF RID: 1743
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdReq : BaseMessageReq
	{
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x00010484 File Offset: 0x0000E684
		// (set) Token: 0x060023A1 RID: 9121 RVA: 0x0001048C File Offset: 0x0000E68C
		[DataMember]
		public Guid FormApprovalId { get; set; }
	}
}
