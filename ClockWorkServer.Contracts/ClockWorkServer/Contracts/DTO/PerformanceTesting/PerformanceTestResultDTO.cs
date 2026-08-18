using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting
{
	// Token: 0x0200035F RID: 863
	[DataContract(Namespace = "http://tpro.ca")]
	public class PerformanceTestResultDTO
	{
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0000941F File Offset: 0x0000761F
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x00009427 File Offset: 0x00007627
		[DataMember]
		public PerformanceTestTimeTakenDTO ServiceTimeTaken { get; set; }

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00009430 File Offset: 0x00007630
		// (set) Token: 0x060013C7 RID: 5063 RVA: 0x00009438 File Offset: 0x00007638
		[DataMember]
		public PerformanceTestTimeTakenDTO ServiceManagerTimeTaken { get; set; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00009441 File Offset: 0x00007641
		// (set) Token: 0x060013C9 RID: 5065 RVA: 0x00009449 File Offset: 0x00007649
		[DataMember]
		public PerformanceTestTimeTakenDTO ManagerTimeTaken { get; set; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x00009452 File Offset: 0x00007652
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x0000945A File Offset: 0x0000765A
		[DataMember]
		public string Notes { get; set; }
	}
}
