using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Startup
{
	// Token: 0x02000263 RID: 611
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkClientStartupReq : BaseMessageReq
	{
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00006A51 File Offset: 0x00004C51
		// (set) Token: 0x06000E20 RID: 3616 RVA: 0x00006A59 File Offset: 0x00004C59
		[DataMember]
		public int PersonId { get; set; }
	}
}
