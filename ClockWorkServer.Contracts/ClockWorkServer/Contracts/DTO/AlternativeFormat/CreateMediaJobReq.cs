using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BD6 RID: 3030
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaJobReq : BaseMessageReq
	{
		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x06003FEC RID: 16364 RVA: 0x0001F69C File Offset: 0x0001D89C
		// (set) Token: 0x06003FED RID: 16365 RVA: 0x0001F6A4 File Offset: 0x0001D8A4
		[DataMember]
		public MediaJobDTO MediaJob { get; set; }
	}
}
