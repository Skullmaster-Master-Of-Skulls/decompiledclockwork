using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x0200091B RID: 2331
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdatePointOfContactReq : BaseMessageReq
	{
		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x000167CF File Offset: 0x000149CF
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x000167D7 File Offset: 0x000149D7
		[DataMember]
		public PointOfContactDTO PointOfContact { get; set; }
	}
}
