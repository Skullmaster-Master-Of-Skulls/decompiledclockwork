using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006CA RID: 1738
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetFormApprovalScreenUserForLoggedInUserOptionsResp
	{
		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x0600238F RID: 9103 RVA: 0x0001041E File Offset: 0x0000E61E
		// (set) Token: 0x06002390 RID: 9104 RVA: 0x00010426 File Offset: 0x0000E626
		[DataMember]
		public FormApprovalScreenUserOptionsDTO Options { get; set; }
	}
}
