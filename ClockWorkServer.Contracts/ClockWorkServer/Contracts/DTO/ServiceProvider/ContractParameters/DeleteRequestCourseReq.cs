using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002CB RID: 715
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteRequestCourseReq : BaseMessageReq
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x0000790F File Offset: 0x00005B0F
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x00007917 File Offset: 0x00005B17
		[DataMember]
		public int SPRequestCourseId { get; set; }
	}
}
