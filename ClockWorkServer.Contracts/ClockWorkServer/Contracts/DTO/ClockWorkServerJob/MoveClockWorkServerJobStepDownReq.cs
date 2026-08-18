using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000875 RID: 2165
	[DataContract(Namespace = "http://tpro.ca")]
	public class MoveClockWorkServerJobStepDownReq : BaseMessageReq
	{
		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06002BE9 RID: 11241 RVA: 0x00014CBC File Offset: 0x00012EBC
		// (set) Token: 0x06002BEA RID: 11242 RVA: 0x00014CC4 File Offset: 0x00012EC4
		[DataMember]
		public ClockWorkServerJobStepDTO JobStep { get; set; }
	}
}
