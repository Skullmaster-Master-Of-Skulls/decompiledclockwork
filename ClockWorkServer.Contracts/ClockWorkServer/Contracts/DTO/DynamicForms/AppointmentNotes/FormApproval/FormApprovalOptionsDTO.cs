using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006C8 RID: 1736
	[DataContract(Namespace = "http://tpro.ca")]
	public class FormApprovalOptionsDTO
	{
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x000103C9 File Offset: 0x0000E5C9
		// (set) Token: 0x06002384 RID: 9092 RVA: 0x000103D1 File Offset: 0x0000E5D1
		[DataMember]
		public bool IsEnabled { get; set; }

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06002385 RID: 9093 RVA: 0x000103DA File Offset: 0x0000E5DA
		// (set) Token: 0x06002386 RID: 9094 RVA: 0x000103E2 File Offset: 0x0000E5E2
		[DataMember]
		public int[] SupervisorGroupIds { get; set; }

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x000103EB File Offset: 0x0000E5EB
		// (set) Token: 0x06002388 RID: 9096 RVA: 0x000103F3 File Offset: 0x0000E5F3
		[DataMember]
		public int[] ExemptGroupIds { get; set; }

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x000103FC File Offset: 0x0000E5FC
		// (set) Token: 0x0600238A RID: 9098 RVA: 0x00010404 File Offset: 0x0000E604
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
