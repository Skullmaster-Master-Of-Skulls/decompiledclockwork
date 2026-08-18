using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BC8 RID: 3016
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedJobsReq : BaseMessageReq
	{
		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x06003FA8 RID: 16296 RVA: 0x0001F4D1 File Offset: 0x0001D6D1
		// (set) Token: 0x06003FA9 RID: 16297 RVA: 0x0001F4D9 File Offset: 0x0001D6D9
		[DataMember]
		public int CampusId { get; set; }
	}
}
