using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002A9 RID: 681
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddProviderCourseRegistrationReq : BaseMessageReq
	{
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00007689 File Offset: 0x00005889
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x00007691 File Offset: 0x00005891
		[DataMember]
		public SPProviderCourseRegistrationDTO ProviderCourseRegistration { get; set; }
	}
}
