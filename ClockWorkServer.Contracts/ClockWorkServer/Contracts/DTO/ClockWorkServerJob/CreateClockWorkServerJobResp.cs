using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000856 RID: 2134
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateClockWorkServerJobResp
	{
		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x00014A8B File Offset: 0x00012C8B
		// (set) Token: 0x06002B89 RID: 11145 RVA: 0x00014A93 File Offset: 0x00012C93
		[DataMember]
		public int JobId { get; set; }
	}
}
