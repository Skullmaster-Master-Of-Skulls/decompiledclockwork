using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006CE RID: 1742
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormApprovalStatusResp
	{
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x00010473 File Offset: 0x0000E673
		// (set) Token: 0x0600239E RID: 9118 RVA: 0x0001047B File Offset: 0x0000E67B
		[DataMember]
		public eFormApprovalState FormApprovalStatus { get; set; }
	}
}
