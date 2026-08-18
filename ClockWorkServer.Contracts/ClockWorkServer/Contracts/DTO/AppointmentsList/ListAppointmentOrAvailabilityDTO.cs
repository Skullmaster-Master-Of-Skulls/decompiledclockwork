using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AvailabilitySchedule2;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AC3 RID: 2755
	[DataContract(Namespace = "http://tpro.ca")]
	public class ListAppointmentOrAvailabilityDTO
	{
		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x0001C88C File Offset: 0x0001AA8C
		// (set) Token: 0x06003A80 RID: 14976 RVA: 0x0001C894 File Offset: 0x0001AA94
		[DataMember]
		public ListAppointmentDTO Appointment { get; set; }

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06003A81 RID: 14977 RVA: 0x0001C89D File Offset: 0x0001AA9D
		// (set) Token: 0x06003A82 RID: 14978 RVA: 0x0001C8A5 File Offset: 0x0001AAA5
		[DataMember]
		public Availability2ItemDTO Availability { get; set; }

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06003A83 RID: 14979 RVA: 0x0001C8AE File Offset: 0x0001AAAE
		// (set) Token: 0x06003A84 RID: 14980 RVA: 0x0001C8B6 File Offset: 0x0001AAB6
		[DataMember]
		public int BackgroundColorArgB { get; set; }
	}
}
