using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200085E RID: 2142
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddClockWorkServerJobStepResp
	{
		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x00014B24 File Offset: 0x00012D24
		// (set) Token: 0x06002BA3 RID: 11171 RVA: 0x00014B2C File Offset: 0x00012D2C
		[DataMember]
		public int JobStepId { get; set; }
	}
}
