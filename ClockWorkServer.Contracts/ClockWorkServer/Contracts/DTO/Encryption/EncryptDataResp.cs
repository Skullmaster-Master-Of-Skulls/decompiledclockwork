using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x0200060E RID: 1550
	[DataContract(Namespace = "http://tpro.ca")]
	public class EncryptDataResp
	{
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06001F97 RID: 8087 RVA: 0x0000E5B5 File Offset: 0x0000C7B5
		// (set) Token: 0x06001F98 RID: 8088 RVA: 0x0000E5BD File Offset: 0x0000C7BD
		[DataMember]
		public IList<byte[]> EncryptedValues { get; set; }
	}
}
