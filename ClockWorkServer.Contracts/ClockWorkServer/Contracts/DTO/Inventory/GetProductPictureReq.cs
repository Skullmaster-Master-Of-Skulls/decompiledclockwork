using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020004FD RID: 1277
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetProductPictureReq : BaseMessageReq
	{
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
		// (set) Token: 0x06001B13 RID: 6931 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		[DataMember]
		public Guid ProductId { get; set; }
	}
}
