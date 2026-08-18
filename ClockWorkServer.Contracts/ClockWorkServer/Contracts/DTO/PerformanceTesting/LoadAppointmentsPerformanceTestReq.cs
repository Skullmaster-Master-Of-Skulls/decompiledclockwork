using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting
{
	// Token: 0x0200035E RID: 862
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsPerformanceTestReq : BaseMessageReq
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x000093EC File Offset: 0x000075EC
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x000093F4 File Offset: 0x000075F4
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x000093FD File Offset: 0x000075FD
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x00009405 File Offset: 0x00007605
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0000940E File Offset: 0x0000760E
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x00009416 File Offset: 0x00007616
		[DataMember]
		public IList<int> PersonIds { get; set; }
	}
}
