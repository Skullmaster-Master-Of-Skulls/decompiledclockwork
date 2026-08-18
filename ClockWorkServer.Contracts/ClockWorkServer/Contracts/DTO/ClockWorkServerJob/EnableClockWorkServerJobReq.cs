using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000877 RID: 2167
	[DataContract(Namespace = "http://tpro.ca")]
	public class EnableClockWorkServerJobReq : BaseMessageReq
	{
		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x00014CEF File Offset: 0x00012EEF
		// (set) Token: 0x06002BF2 RID: 11250 RVA: 0x00014CF7 File Offset: 0x00012EF7
		[DataMember]
		public int JobId { get; set; }
	}
}
