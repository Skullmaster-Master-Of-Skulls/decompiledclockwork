using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000869 RID: 2153
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerExecutingLogsReq : BaseMessageReq
	{
		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x00014C01 File Offset: 0x00012E01
		// (set) Token: 0x06002BC8 RID: 11208 RVA: 0x00014C09 File Offset: 0x00012E09
		[DataMember]
		public DateTime StartTime { get; set; }

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x00014C12 File Offset: 0x00012E12
		// (set) Token: 0x06002BCA RID: 11210 RVA: 0x00014C1A File Offset: 0x00012E1A
		[DataMember]
		public DateTime EndTime { get; set; }
	}
}
