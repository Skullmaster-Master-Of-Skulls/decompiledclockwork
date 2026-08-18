using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B6C RID: 2924
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByIdentifierReq : BaseMessageReq
	{
		// Token: 0x170016D5 RID: 5845
		// (get) Token: 0x06003DFC RID: 15868 RVA: 0x0001E6B9 File Offset: 0x0001C8B9
		// (set) Token: 0x06003DFD RID: 15869 RVA: 0x0001E6C1 File Offset: 0x0001C8C1
		[DataMember]
		public MediaContentIdentifierDTO Identifier { get; set; }
	}
}
