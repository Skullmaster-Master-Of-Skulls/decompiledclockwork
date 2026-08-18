using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004D1 RID: 1233
	public class UpdateServiceProviderRequestNotesReq : BaseMessageReq
	{
		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x0000C031 File Offset: 0x0000A231
		// (set) Token: 0x06001A01 RID: 6657 RVA: 0x0000C039 File Offset: 0x0000A239
		[DataMember]
		public int RequestId { get; set; }

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0000C042 File Offset: 0x0000A242
		// (set) Token: 0x06001A03 RID: 6659 RVA: 0x0000C04A File Offset: 0x0000A24A
		[DataMember]
		public string Notes { get; set; }
	}
}
