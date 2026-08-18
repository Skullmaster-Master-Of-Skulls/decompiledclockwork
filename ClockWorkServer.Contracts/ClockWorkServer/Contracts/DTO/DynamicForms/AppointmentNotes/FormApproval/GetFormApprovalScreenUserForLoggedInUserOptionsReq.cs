using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006C9 RID: 1737
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetFormApprovalScreenUserForLoggedInUserOptionsReq : BaseMessageReq
	{
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x0001040D File Offset: 0x0000E60D
		// (set) Token: 0x0600238D RID: 9101 RVA: 0x00010415 File Offset: 0x0000E615
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
