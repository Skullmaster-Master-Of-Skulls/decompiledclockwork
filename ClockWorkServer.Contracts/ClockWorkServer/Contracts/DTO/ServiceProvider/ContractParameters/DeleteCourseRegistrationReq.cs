using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x02000297 RID: 663
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteCourseRegistrationReq : BaseMessageReq
	{
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00007568 File Offset: 0x00005768
		// (set) Token: 0x06000FA2 RID: 4002 RVA: 0x00007570 File Offset: 0x00005770
		[DataMember]
		public int SPProviderCourseRegistrationId { get; set; }
	}
}
