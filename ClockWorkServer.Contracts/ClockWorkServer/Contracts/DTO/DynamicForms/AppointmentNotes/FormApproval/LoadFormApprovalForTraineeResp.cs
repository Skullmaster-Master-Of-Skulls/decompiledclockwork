using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006E5 RID: 1765
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormApprovalForTraineeResp
	{
		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x00010770 File Offset: 0x0000E970
		// (set) Token: 0x0600240F RID: 9231 RVA: 0x00010778 File Offset: 0x0000E978
		[DataMember]
		public FormApprovalForAppointmentDTO FormApproval { get; set; }
	}
}
