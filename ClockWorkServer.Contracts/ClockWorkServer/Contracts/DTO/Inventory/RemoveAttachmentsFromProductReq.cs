using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004F9 RID: 1273
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveAttachmentsFromProductReq : BaseMessageReq
	{
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x0000C7B2 File Offset: 0x0000A9B2
		// (set) Token: 0x06001B0B RID: 6923 RVA: 0x0000C7BA File Offset: 0x0000A9BA
		[DataMember]
		public IList<int> AttachedFileIds { get; set; }
	}
}
