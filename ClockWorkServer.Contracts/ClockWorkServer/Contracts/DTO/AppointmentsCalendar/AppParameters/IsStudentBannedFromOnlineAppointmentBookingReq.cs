using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B03 RID: 2819
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsStudentBannedFromOnlineAppointmentBookingReq : BaseMessageReq
	{
		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06003B99 RID: 15257 RVA: 0x0001CFDF File Offset: 0x0001B1DF
		// (set) Token: 0x06003B9A RID: 15258 RVA: 0x0001CFE7 File Offset: 0x0001B1E7
		[DataMember]
		public int PersonId { get; set; }
	}
}
