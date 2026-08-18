using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x02000607 RID: 1543
	[DataContract(Namespace = "http://tpro.ca")]
	public class EncryptReq : BaseMessageReq
	{
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x0000E51C File Offset: 0x0000C71C
		// (set) Token: 0x06001F7F RID: 8063 RVA: 0x0000E524 File Offset: 0x0000C724
		[DataMember]
		public string Text { get; set; }
	}
}
