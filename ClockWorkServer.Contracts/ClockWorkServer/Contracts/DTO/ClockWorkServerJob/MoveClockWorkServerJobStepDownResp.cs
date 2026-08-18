using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000876 RID: 2166
	[DataContract(Namespace = "http://tpro.ca")]
	public class MoveClockWorkServerJobStepDownResp
	{
		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x00014CCD File Offset: 0x00012ECD
		// (set) Token: 0x06002BED RID: 11245 RVA: 0x00014CD5 File Offset: 0x00012ED5
		[DataMember]
		public ClockWorkServerJobStepDTO JobStep { get; set; }

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x00014CDE File Offset: 0x00012EDE
		// (set) Token: 0x06002BEF RID: 11247 RVA: 0x00014CE6 File Offset: 0x00012EE6
		[DataMember]
		public ClockWorkServerJobStepDTO NextJobStep { get; set; }
	}
}
