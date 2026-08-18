using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000857 RID: 2135
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateClockWorkServerJobReq : BaseMessageReq
	{
		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x00014A9C File Offset: 0x00012C9C
		// (set) Token: 0x06002B8C RID: 11148 RVA: 0x00014AA4 File Offset: 0x00012CA4
		[DataMember]
		public ClockWorkServerJobInfoDTO ClockWorkServerJob { get; set; }
	}
}
