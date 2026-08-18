using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData
{
	// Token: 0x020004DF RID: 1247
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetDynamicDataDecryptedPreviewItemsReq : BaseMessageReq
	{
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0000C361 File Offset: 0x0000A561
		// (set) Token: 0x06001A6F RID: 6767 RVA: 0x0000C369 File Offset: 0x0000A569
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x0000C372 File Offset: 0x0000A572
		// (set) Token: 0x06001A71 RID: 6769 RVA: 0x0000C37A File Offset: 0x0000A57A
		[DataMember]
		public int ControlId { get; set; }
	}
}
