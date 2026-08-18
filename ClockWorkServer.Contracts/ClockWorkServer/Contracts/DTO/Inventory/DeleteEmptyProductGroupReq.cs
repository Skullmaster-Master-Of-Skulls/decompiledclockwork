using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000529 RID: 1321
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteEmptyProductGroupReq : BaseMessageReq
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x0000CB37 File Offset: 0x0000AD37
		// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x0000CB3F File Offset: 0x0000AD3F
		[DataMember]
		public int GroupId { get; set; }
	}
}
