using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting
{
	// Token: 0x02000360 RID: 864
	[DataContract(Namespace = "http://tpro.ca")]
	public class PerformanceTestTimeTakenDTO
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x00009463 File Offset: 0x00007663
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x0000946B File Offset: 0x0000766B
		[DataMember]
		public DateTime EntryPoint { get; set; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x00009474 File Offset: 0x00007674
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x0000947C File Offset: 0x0000767C
		[DataMember]
		public TimeSpan TimeElapsed { get; set; }
	}
}
