using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000977 RID: 2423
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateShowTimeAsTypeReq : BaseMessageReq
	{
		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x00018105 File Offset: 0x00016305
		// (set) Token: 0x0600316B RID: 12651 RVA: 0x0001810D File Offset: 0x0001630D
		[DataMember]
		public AppShowTimeAsTypeDTO ShowTimeAsType { get; set; }
	}
}
