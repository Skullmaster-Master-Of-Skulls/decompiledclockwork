using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006D0 RID: 1744
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPendingFormApprovalItemForCurrentUserByFormApprovalIdResp
	{
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x00010495 File Offset: 0x0000E695
		// (set) Token: 0x060023A4 RID: 9124 RVA: 0x0001049D File Offset: 0x0000E69D
		[DataMember]
		public FormApprovalPendingItemDTO PendingItem { get; set; }
	}
}
