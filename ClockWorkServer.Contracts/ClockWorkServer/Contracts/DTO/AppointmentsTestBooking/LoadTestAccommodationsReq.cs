using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009F2 RID: 2546
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestAccommodationsReq : BaseMessageReq
	{
		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06003503 RID: 13571 RVA: 0x00019CC0 File Offset: 0x00017EC0
		// (set) Token: 0x06003504 RID: 13572 RVA: 0x00019CC8 File Offset: 0x00017EC8
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06003505 RID: 13573 RVA: 0x00019CD1 File Offset: 0x00017ED1
		// (set) Token: 0x06003506 RID: 13574 RVA: 0x00019CD9 File Offset: 0x00017ED9
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x00019CE2 File Offset: 0x00017EE2
		// (set) Token: 0x06003508 RID: 13576 RVA: 0x00019CEA File Offset: 0x00017EEA
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
