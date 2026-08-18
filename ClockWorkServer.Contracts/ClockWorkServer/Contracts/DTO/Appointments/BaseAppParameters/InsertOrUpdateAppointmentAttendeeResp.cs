using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000962 RID: 2402
	[DataContract(Namespace = "http://tpro.ca")]
	public class InsertOrUpdateAppointmentAttendeeResp
	{
		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x00017E90 File Offset: 0x00016090
		// (set) Token: 0x0600310C RID: 12556 RVA: 0x00017E98 File Offset: 0x00016098
		[DataMember]
		public int AttendeeId { get; set; }
	}
}
