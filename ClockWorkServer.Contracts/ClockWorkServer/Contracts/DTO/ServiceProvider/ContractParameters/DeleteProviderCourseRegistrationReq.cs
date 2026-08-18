using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002AD RID: 685
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteProviderCourseRegistrationReq : BaseMessageReq
	{
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x000076AB File Offset: 0x000058AB
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x000076B3 File Offset: 0x000058B3
		[DataMember]
		public int SPProviderCourseRegistrationId { get; set; }
	}
}
