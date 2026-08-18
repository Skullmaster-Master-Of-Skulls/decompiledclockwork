using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BE2 RID: 3042
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaJobStatusReq : BaseMessageReq
	{
		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x0001F89A File Offset: 0x0001DA9A
		// (set) Token: 0x06004035 RID: 16437 RVA: 0x0001F8A2 File Offset: 0x0001DAA2
		[DataMember]
		public MediaJobStatusDTO MediaJobStatus { get; set; }
	}
}
