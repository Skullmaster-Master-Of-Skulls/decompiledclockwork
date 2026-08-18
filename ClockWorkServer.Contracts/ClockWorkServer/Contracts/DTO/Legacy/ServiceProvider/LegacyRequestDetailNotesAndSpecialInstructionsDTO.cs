using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004CA RID: 1226
	[DataContract(Namespace = "http://tpro.ca")]
	public class LegacyRequestDetailNotesAndSpecialInstructionsDTO
	{
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x0000BFBA File Offset: 0x0000A1BA
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x0000BFC2 File Offset: 0x0000A1C2
		[DataMember]
		public int RequestId { get; set; }

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x0000BFCB File Offset: 0x0000A1CB
		// (set) Token: 0x060019EE RID: 6638 RVA: 0x0000BFD3 File Offset: 0x0000A1D3
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		// (set) Token: 0x060019F0 RID: 6640 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		[DataMember]
		public string SpecialInstructions { get; set; }
	}
}
