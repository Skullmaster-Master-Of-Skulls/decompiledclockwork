using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x02000610 RID: 1552
	[DataContract(Namespace = "http://tpro.ca")]
	public class DecryptDataResp
	{
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x0000E5D7 File Offset: 0x0000C7D7
		// (set) Token: 0x06001F9E RID: 8094 RVA: 0x0000E5DF File Offset: 0x0000C7DF
		[DataMember]
		public IList<string> PlainTextValues { get; set; }
	}
}
