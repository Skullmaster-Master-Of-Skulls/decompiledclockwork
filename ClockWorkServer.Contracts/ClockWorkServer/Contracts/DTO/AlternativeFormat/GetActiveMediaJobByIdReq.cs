using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BAA RID: 2986
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveMediaJobByIdReq : BaseMessageReq
	{
		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x06003F34 RID: 16180 RVA: 0x0001F1F6 File Offset: 0x0001D3F6
		// (set) Token: 0x06003F35 RID: 16181 RVA: 0x0001F1FE File Offset: 0x0001D3FE
		[DataMember]
		public int MediaJobId { get; set; }
	}
}
