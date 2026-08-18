using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006E3 RID: 1763
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSupervisorSignatureResp
	{
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x0001072C File Offset: 0x0000E92C
		// (set) Token: 0x06002405 RID: 9221 RVA: 0x00010734 File Offset: 0x0000E934
		[DataMember]
		public FormApprovalSignatureDTO Signature { get; set; }
	}
}
