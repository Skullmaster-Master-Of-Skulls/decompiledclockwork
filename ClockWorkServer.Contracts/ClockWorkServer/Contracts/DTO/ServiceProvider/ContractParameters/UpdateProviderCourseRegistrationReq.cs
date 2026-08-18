using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002AB RID: 683
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateProviderCourseRegistrationReq : BaseMessageReq
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0000769A File Offset: 0x0000589A
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x000076A2 File Offset: 0x000058A2
		[DataMember]
		public SPProviderCourseRegistrationDTO ProviderCourseRegistration { get; set; }
	}
}
