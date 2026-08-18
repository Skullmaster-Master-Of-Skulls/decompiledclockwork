using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x0200060F RID: 1551
	[DataContract(Namespace = "http://tpro.ca")]
	public class DecryptDataReq : BaseMessageReq
	{
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x0000E5C6 File Offset: 0x0000C7C6
		// (set) Token: 0x06001F9B RID: 8091 RVA: 0x0000E5CE File Offset: 0x0000C7CE
		[DataMember]
		public IList<byte[]> EncryptedValues { get; set; }
	}
}
