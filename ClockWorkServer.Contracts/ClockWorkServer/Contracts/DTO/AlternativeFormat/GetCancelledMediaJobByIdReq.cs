using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BBE RID: 3006
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCancelledMediaJobByIdReq : BaseMessageReq
	{
		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x0001F3B0 File Offset: 0x0001D5B0
		// (set) Token: 0x06003F7D RID: 16253 RVA: 0x0001F3B8 File Offset: 0x0001D5B8
		[DataMember]
		public int MediaJobId { get; set; }
	}
}
