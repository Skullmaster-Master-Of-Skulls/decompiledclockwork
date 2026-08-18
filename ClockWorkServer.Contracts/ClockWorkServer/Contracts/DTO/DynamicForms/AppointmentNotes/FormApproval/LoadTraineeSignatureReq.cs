using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006EC RID: 1772
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTraineeSignatureReq : BaseMessageReq
	{
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x0001082B File Offset: 0x0000EA2B
		// (set) Token: 0x0600242C RID: 9260 RVA: 0x00010833 File Offset: 0x0000EA33
		[DataMember]
		public Guid FormApprovalId { get; set; }
	}
}
