using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200099C RID: 2460
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateAppTypeWithExtendedInfoReq : BaseMessageReq
	{
		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x00018347 File Offset: 0x00016547
		// (set) Token: 0x060031D4 RID: 12756 RVA: 0x0001834F File Offset: 0x0001654F
		[DataMember]
		public AppTypeWithExtendedInfoDTO AppType { get; set; }
	}
}
