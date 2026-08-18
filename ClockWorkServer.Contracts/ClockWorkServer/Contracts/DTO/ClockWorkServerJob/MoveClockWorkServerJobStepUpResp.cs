using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000874 RID: 2164
	[DataContract(Namespace = "http://tpro.ca")]
	public class MoveClockWorkServerJobStepUpResp
	{
		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x00014C9A File Offset: 0x00012E9A
		// (set) Token: 0x06002BE5 RID: 11237 RVA: 0x00014CA2 File Offset: 0x00012EA2
		[DataMember]
		public ClockWorkServerJobStepDTO JobStep { get; set; }

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x00014CAB File Offset: 0x00012EAB
		// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x00014CB3 File Offset: 0x00012EB3
		[DataMember]
		public ClockWorkServerJobStepDTO PreviousJobStep { get; set; }
	}
}
