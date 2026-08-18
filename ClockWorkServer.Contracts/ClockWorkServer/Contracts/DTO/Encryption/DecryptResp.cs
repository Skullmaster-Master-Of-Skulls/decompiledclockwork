using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x0200060A RID: 1546
	[DataContract(Namespace = "http://tpro.ca")]
	public class DecryptResp
	{
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x0000E54F File Offset: 0x0000C74F
		// (set) Token: 0x06001F88 RID: 8072 RVA: 0x0000E557 File Offset: 0x0000C757
		[DataMember]
		public string Text { get; set; }
	}
}
