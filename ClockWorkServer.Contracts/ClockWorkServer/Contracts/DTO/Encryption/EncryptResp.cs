using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x02000608 RID: 1544
	[DataContract(Namespace = "http://tpro.ca")]
	public class EncryptResp
	{
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x0000E52D File Offset: 0x0000C72D
		// (set) Token: 0x06001F82 RID: 8066 RVA: 0x0000E535 File Offset: 0x0000C735
		[DataMember]
		public byte[] EncryptedBytes { get; set; }
	}
}
