using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006ED RID: 1773
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTraineeSignatureResp
	{
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x0001083C File Offset: 0x0000EA3C
		// (set) Token: 0x0600242F RID: 9263 RVA: 0x00010844 File Offset: 0x0000EA44
		[DataMember]
		public FormApprovalSignatureDTO Signature { get; set; }
	}
}
