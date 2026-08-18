using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x02000609 RID: 1545
	[DataContract(Namespace = "http://tpro.ca")]
	public class DecryptReq : BaseMessageReq
	{
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x0000E53E File Offset: 0x0000C73E
		// (set) Token: 0x06001F85 RID: 8069 RVA: 0x0000E546 File Offset: 0x0000C746
		[DataMember]
		public byte[] EncryptedBytes { get; set; }
	}
}
