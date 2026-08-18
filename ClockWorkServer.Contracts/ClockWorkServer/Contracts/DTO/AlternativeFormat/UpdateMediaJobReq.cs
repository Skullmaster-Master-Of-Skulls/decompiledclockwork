using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BD8 RID: 3032
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateMediaJobReq : BaseMessageReq
	{
		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x06003FF2 RID: 16370 RVA: 0x0001F6BE File Offset: 0x0001D8BE
		// (set) Token: 0x06003FF3 RID: 16371 RVA: 0x0001F6C6 File Offset: 0x0001D8C6
		[DataMember]
		public MediaJobDTO MediaJob { get; set; }
	}
}
