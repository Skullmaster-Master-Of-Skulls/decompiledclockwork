using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000AB7 RID: 2743
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsUserAllowedToEditAllAppointmentsInARecurringSetReq : BaseMessageReq
	{
		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x0001C3AF File Offset: 0x0001A5AF
		// (set) Token: 0x06003A2D RID: 14893 RVA: 0x0001C3B7 File Offset: 0x0001A5B7
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x06003A2E RID: 14894 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
		// (set) Token: 0x06003A2F RID: 14895 RVA: 0x0001C3C8 File Offset: 0x0001A5C8
		[DataMember]
		public int PersonId { get; set; }
	}
}
