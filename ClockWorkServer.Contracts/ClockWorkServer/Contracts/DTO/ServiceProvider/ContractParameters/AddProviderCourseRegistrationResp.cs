using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002A8 RID: 680
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddProviderCourseRegistrationResp
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00007678 File Offset: 0x00005878
		// (set) Token: 0x06000FD3 RID: 4051 RVA: 0x00007680 File Offset: 0x00005880
		[DataMember]
		public int SPProviderCourseRegistrationId { get; set; }
	}
}
