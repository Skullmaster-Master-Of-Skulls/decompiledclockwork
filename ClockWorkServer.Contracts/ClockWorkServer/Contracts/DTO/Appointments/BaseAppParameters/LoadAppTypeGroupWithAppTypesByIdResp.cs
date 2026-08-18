using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200098B RID: 2443
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppTypeGroupWithAppTypesByIdResp
	{
		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x0001826A File Offset: 0x0001646A
		// (set) Token: 0x060031A9 RID: 12713 RVA: 0x00018272 File Offset: 0x00016472
		[DataMember]
		public AppTypeGroupWithAppTypesDTO AppTypeGroupWithAppTypes { get; set; }
	}
}
