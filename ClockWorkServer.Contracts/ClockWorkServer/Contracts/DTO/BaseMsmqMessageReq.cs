using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000EA RID: 234
	[DataContract(Namespace = "http://tpro.ca")]
	public class BaseMsmqMessageReq : BaseMessageReq
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x000028D1 File Offset: 0x00000AD1
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x000028D9 File Offset: 0x00000AD9
		[DataMember]
		public string SecureToken { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x000028E2 File Offset: 0x00000AE2
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x000028EA File Offset: 0x00000AEA
		[DataMember]
		public int NRetries { get; set; }
	}
}
