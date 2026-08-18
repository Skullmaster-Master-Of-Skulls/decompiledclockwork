using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200098F RID: 2447
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateAppTypeGroupReq : BaseMessageReq
	{
		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x000182AE File Offset: 0x000164AE
		// (set) Token: 0x060031B5 RID: 12725 RVA: 0x000182B6 File Offset: 0x000164B6
		[DataMember]
		public AppTypeGroupDTO AppTypeGroup { get; set; }
	}
}
