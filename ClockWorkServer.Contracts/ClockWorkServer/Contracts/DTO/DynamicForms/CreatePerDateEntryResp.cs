using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000659 RID: 1625
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreatePerDateEntryResp
	{
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x0000F02C File Offset: 0x0000D22C
		// (set) Token: 0x0600210D RID: 8461 RVA: 0x0000F034 File Offset: 0x0000D234
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
