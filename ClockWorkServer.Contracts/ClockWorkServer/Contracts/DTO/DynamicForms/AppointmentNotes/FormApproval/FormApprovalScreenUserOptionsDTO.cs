using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006D6 RID: 1750
	[DataContract(Namespace = "http://tpro.ca")]
	public class FormApprovalScreenUserOptionsDTO
	{
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x000105A5 File Offset: 0x0000E7A5
		// (set) Token: 0x060023CA RID: 9162 RVA: 0x000105AD File Offset: 0x0000E7AD
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x000105B6 File Offset: 0x0000E7B6
		// (set) Token: 0x060023CC RID: 9164 RVA: 0x000105BE File Offset: 0x0000E7BE
		[DataMember]
		public bool IsEnabled { get; set; }

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060023CD RID: 9165 RVA: 0x000105C7 File Offset: 0x0000E7C7
		// (set) Token: 0x060023CE RID: 9166 RVA: 0x000105CF File Offset: 0x0000E7CF
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x000105D8 File Offset: 0x0000E7D8
		// (set) Token: 0x060023D0 RID: 9168 RVA: 0x000105E0 File Offset: 0x0000E7E0
		[DataMember]
		public bool IsSupervisor { get; set; }
	}
}
