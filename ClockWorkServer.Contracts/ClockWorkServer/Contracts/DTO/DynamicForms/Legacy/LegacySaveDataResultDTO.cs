using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Legacy
{
	// Token: 0x020006BB RID: 1723
	[DataContract(Namespace = "http://tpro.ca")]
	public class LegacySaveDataResultDTO
	{
		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x0001004C File Offset: 0x0000E24C
		// (set) Token: 0x0600230F RID: 8975 RVA: 0x00010054 File Offset: 0x0000E254
		public int PersonId { get; set; }

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x0001005D File Offset: 0x0000E25D
		// (set) Token: 0x06002311 RID: 8977 RVA: 0x00010065 File Offset: 0x0000E265
		public int ControlId { get; set; }

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x0001006E File Offset: 0x0000E26E
		// (set) Token: 0x06002313 RID: 8979 RVA: 0x00010076 File Offset: 0x0000E276
		public Exception Exception { get; set; }
	}
}
