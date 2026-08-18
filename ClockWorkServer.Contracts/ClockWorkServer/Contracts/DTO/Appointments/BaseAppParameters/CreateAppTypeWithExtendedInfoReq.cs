using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200099E RID: 2462
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAppTypeWithExtendedInfoReq : BaseMessageReq
	{
		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x060031D7 RID: 12759 RVA: 0x00018358 File Offset: 0x00016558
		// (set) Token: 0x060031D8 RID: 12760 RVA: 0x00018360 File Offset: 0x00016560
		[DataMember]
		public AppTypeWithExtendedInfoDTO AppType { get; set; }
	}
}
