using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200098D RID: 2445
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateAppTypeGroupReq : BaseMessageReq
	{
		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x0001828C File Offset: 0x0001648C
		// (set) Token: 0x060031AF RID: 12719 RVA: 0x00018294 File Offset: 0x00016494
		[DataMember]
		public AppTypeGroupDTO AppTypeGroup { get; set; }
	}
}
