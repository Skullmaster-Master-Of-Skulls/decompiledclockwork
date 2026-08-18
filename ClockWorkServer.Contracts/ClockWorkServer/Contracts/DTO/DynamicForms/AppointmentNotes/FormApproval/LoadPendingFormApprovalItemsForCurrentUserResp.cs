using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006CC RID: 1740
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPendingFormApprovalItemsForCurrentUserResp
	{
		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x0001042F File Offset: 0x0000E62F
		// (set) Token: 0x06002394 RID: 9108 RVA: 0x00010437 File Offset: 0x0000E637
		[DataMember]
		public IList<FormApprovalPendingItemDTO> PendingItems { get; set; }
	}
}
