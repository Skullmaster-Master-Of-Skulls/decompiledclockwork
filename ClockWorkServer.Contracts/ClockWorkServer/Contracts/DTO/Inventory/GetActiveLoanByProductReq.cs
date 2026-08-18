using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000538 RID: 1336
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveLoanByProductReq : BaseMessageReq
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x0000CD60 File Offset: 0x0000AF60
		// (set) Token: 0x06001BF4 RID: 7156 RVA: 0x0000CD68 File Offset: 0x0000AF68
		[DataMember]
		public string ProductUniqueId { get; set; }

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x0000CD71 File Offset: 0x0000AF71
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x0000CD79 File Offset: 0x0000AF79
		[DataMember]
		public int AlternateProductId { get; set; }
	}
}
