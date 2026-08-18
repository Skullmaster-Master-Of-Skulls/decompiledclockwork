using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000658 RID: 1624
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreatePerDateEntryReq : BaseMessageReq
	{
		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x0000F01B File Offset: 0x0000D21B
		// (set) Token: 0x0600210A RID: 8458 RVA: 0x0000F023 File Offset: 0x0000D223
		[DataMember]
		public PerDateEntryDTO PerDateEntry { get; set; }
	}
}
