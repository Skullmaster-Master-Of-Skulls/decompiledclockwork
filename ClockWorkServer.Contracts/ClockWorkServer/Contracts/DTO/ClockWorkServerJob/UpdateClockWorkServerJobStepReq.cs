using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000861 RID: 2145
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateClockWorkServerJobStepReq : BaseMessageReq
	{
		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x00014B57 File Offset: 0x00012D57
		// (set) Token: 0x06002BAC RID: 11180 RVA: 0x00014B5F File Offset: 0x00012D5F
		[DataMember]
		public ClockWorkServerJobStepDTO ClockWorkServerJobStep { get; set; }
	}
}
