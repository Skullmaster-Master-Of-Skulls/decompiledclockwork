using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001B8 RID: 440
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetDefaultFromAddressResp
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00004912 File Offset: 0x00002B12
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x0000491A File Offset: 0x00002B1A
		[DataMember]
		public string EmailAddress { get; set; }
	}
}
