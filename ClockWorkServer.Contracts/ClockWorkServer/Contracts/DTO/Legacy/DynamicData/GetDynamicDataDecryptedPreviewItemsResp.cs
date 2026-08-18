using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData
{
	// Token: 0x020004E0 RID: 1248
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetDynamicDataDecryptedPreviewItemsResp
	{
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x0000C383 File Offset: 0x0000A583
		// (set) Token: 0x06001A74 RID: 6772 RVA: 0x0000C38B File Offset: 0x0000A58B
		[DataMember]
		public IList<DynamicDataDecryptedPreviewItemDTO> DecryptedItems { get; set; }
	}
}
