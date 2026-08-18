using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Accommodation
{
	// Token: 0x020004EB RID: 1259
	[DataContract(Namespace = "http://tpro.ca")]
	public class LogLoaIssuedDateReq : BaseMessageReq
	{
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x0000C65E File Offset: 0x0000A85E
		// (set) Token: 0x06001AD5 RID: 6869 RVA: 0x0000C666 File Offset: 0x0000A866
		[DataMember]
		public int Pid { get; set; }

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x0000C66F File Offset: 0x0000A86F
		// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x0000C677 File Offset: 0x0000A877
		[DataMember]
		public int Lucid { get; set; }

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x0000C680 File Offset: 0x0000A880
		// (set) Token: 0x06001AD9 RID: 6873 RVA: 0x0000C688 File Offset: 0x0000A888
		[DataMember]
		public string LoaString { get; set; }
	}
}
