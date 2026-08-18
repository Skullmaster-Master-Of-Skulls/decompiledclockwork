using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200097E RID: 2430
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppTypeByIdReq : BaseMessageReq
	{
		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x0001817C File Offset: 0x0001637C
		// (set) Token: 0x06003180 RID: 12672 RVA: 0x00018184 File Offset: 0x00016384
		[DataMember]
		public int AppTypeId { get; set; }
	}
}
