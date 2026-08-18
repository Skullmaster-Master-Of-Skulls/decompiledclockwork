using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000ADB RID: 2779
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteClosedDayReq : BaseMessageReq
	{
		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x0001CA35 File Offset: 0x0001AC35
		// (set) Token: 0x06003ACA RID: 15050 RVA: 0x0001CA3D File Offset: 0x0001AC3D
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x0001CA46 File Offset: 0x0001AC46
		// (set) Token: 0x06003ACC RID: 15052 RVA: 0x0001CA4E File Offset: 0x0001AC4E
		[DataMember]
		public DateTime Date { get; set; }
	}
}
