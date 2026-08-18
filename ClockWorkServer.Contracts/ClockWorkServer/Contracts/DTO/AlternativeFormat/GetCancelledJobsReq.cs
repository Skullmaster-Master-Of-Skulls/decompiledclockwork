using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BCA RID: 3018
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCancelledJobsReq : BaseMessageReq
	{
		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x06003FAE RID: 16302 RVA: 0x0001F4F3 File Offset: 0x0001D6F3
		// (set) Token: 0x06003FAF RID: 16303 RVA: 0x0001F4FB File Offset: 0x0001D6FB
		[DataMember]
		public int CampusId { get; set; }
	}
}
