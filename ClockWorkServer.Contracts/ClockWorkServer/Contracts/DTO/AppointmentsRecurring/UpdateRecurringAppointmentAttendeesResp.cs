using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring
{
	// Token: 0x02000ABA RID: 2746
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRecurringAppointmentAttendeesResp
	{
		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06003A3F RID: 14911 RVA: 0x0001C437 File Offset: 0x0001A637
		// (set) Token: 0x06003A40 RID: 14912 RVA: 0x0001C43F File Offset: 0x0001A63F
		[DataMember]
		public IList<AppointmentForNotificationDTO> AppointmentsForNotification { get; set; }
	}
}
