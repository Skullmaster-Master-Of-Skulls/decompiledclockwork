using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200065F RID: 1631
	[DataContract(Namespace = "http://tpro.ca")]
	public class DoesAtLeastOneSavedDataItemExistByControlIdsResp
	{
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002128 RID: 8488 RVA: 0x0000F0E7 File Offset: 0x0000D2E7
		// (set) Token: 0x06002129 RID: 8489 RVA: 0x0000F0EF File Offset: 0x0000D2EF
		[DataMember]
		public bool AtLeastOneDataItemExists { get; set; }
	}
}
