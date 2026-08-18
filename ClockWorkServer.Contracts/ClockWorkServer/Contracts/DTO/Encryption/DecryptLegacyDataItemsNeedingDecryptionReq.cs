using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x02000613 RID: 1555
	[DataContract(Namespace = "http://tpro.ca")]
	public class DecryptLegacyDataItemsNeedingDecryptionReq : BaseMessageReq
	{
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06001FA8 RID: 8104 RVA: 0x0000E61B File Offset: 0x0000C81B
		// (set) Token: 0x06001FA9 RID: 8105 RVA: 0x0000E623 File Offset: 0x0000C823
		[DataMember]
		public IList<LegacyDynamicDataItemItemsToBeDecryptedDTO> ItemsToDecrypt { get; set; }
	}
}
