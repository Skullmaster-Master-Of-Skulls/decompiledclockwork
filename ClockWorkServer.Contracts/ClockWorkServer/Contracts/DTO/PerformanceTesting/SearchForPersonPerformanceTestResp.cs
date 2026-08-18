using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting
{
	// Token: 0x0200035B RID: 859
	[DataContract(Namespace = "http://tpro.ca")]
	public class SearchForPersonPerformanceTestResp
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x000093B9 File Offset: 0x000075B9
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x000093C1 File Offset: 0x000075C1
		[DataMember]
		public SearchForPersonPerformanceTestResultDTO Result { get; set; }
	}
}
