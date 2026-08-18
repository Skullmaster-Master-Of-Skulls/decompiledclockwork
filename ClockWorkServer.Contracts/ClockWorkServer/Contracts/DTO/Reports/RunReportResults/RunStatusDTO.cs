using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults
{
	// Token: 0x0200035A RID: 858
	[DataContract(Namespace = "http://tpro.ca")]
	public class RunStatusDTO
	{
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00009397 File Offset: 0x00007597
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x0000939F File Offset: 0x0000759F
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x000093A8 File Offset: 0x000075A8
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x000093B0 File Offset: 0x000075B0
		[DataMember]
		public eRunStatusStepDTO LastStatusStep { get; set; }
	}
}
