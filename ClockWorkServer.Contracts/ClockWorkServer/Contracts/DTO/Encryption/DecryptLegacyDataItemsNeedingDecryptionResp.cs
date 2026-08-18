using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x02000614 RID: 1556
	[DataContract(Namespace = "http://tpro.ca")]
	public class DecryptLegacyDataItemsNeedingDecryptionResp
	{
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x0000E62C File Offset: 0x0000C82C
		// (set) Token: 0x06001FAC RID: 8108 RVA: 0x0000E634 File Offset: 0x0000C834
		[DataMember]
		public IList<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO> DecryptedItems { get; set; }
	}
}
