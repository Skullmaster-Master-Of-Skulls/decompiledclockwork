using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting
{
	// Token: 0x02000361 RID: 865
	[DataContract(Namespace = "http://tpro.ca")]
	public class SearchForPersonPerformanceTestResultDTO
	{
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00009485 File Offset: 0x00007685
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x0000948D File Offset: 0x0000768D
		[DataMember]
		public PerformanceTestResultDTO TestResult { get; set; }

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00009496 File Offset: 0x00007696
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x0000949E File Offset: 0x0000769E
		[DataMember]
		public IList<UserGroupObjectDTO> FoundPersons { get; set; }
	}
}
