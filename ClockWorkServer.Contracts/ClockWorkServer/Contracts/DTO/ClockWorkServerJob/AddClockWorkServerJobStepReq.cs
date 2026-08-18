using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200085D RID: 2141
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddClockWorkServerJobStepReq : BaseMessageReq
	{
		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06002B9F RID: 11167 RVA: 0x00014B13 File Offset: 0x00012D13
		// (set) Token: 0x06002BA0 RID: 11168 RVA: 0x00014B1B File Offset: 0x00012D1B
		[DataMember]
		public ClockWorkServerJobStepDTO ClockWorkServerJobStep { get; set; }
	}
}
