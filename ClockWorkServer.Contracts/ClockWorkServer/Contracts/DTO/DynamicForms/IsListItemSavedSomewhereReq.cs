using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000686 RID: 1670
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsListItemSavedSomewhereReq : BaseMessageReq
	{
		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x0000F7CD File Offset: 0x0000D9CD
		// (set) Token: 0x060021F5 RID: 8693 RVA: 0x0000F7D5 File Offset: 0x0000D9D5
		[DataMember]
		public int LookupListId { get; set; }
	}
}
