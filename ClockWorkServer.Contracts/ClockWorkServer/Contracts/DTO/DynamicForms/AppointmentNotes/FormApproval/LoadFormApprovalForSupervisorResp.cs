using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006D9 RID: 1753
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormApprovalForSupervisorResp
	{
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060023E4 RID: 9188 RVA: 0x00010671 File Offset: 0x0000E871
		// (set) Token: 0x060023E5 RID: 9189 RVA: 0x00010679 File Offset: 0x0000E879
		[DataMember]
		public FormApprovalForAppointmentDTO FormApproval { get; set; }
	}
}
