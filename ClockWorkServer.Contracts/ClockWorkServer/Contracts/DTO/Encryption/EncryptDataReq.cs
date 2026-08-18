using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x0200060D RID: 1549
	[DataContract(Namespace = "http://tpro.ca")]
	public class EncryptDataReq : BaseMessageReq
	{
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		// (set) Token: 0x06001F95 RID: 8085 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		[DataMember]
		public IList<string> PlainTextValues { get; set; }
	}
}
