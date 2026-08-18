using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000687 RID: 1671
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsListItemSavedSomewhereResp
	{
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x0000F7DE File Offset: 0x0000D9DE
		// (set) Token: 0x060021F8 RID: 8696 RVA: 0x0000F7E6 File Offset: 0x0000D9E6
		[DataMember]
		public bool DataExistsWithThisLookupListId { get; set; }
	}
}
