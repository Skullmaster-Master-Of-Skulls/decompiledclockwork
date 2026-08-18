using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000959 RID: 2393
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAttendeesByAppointmentIdReq : BaseMessageReq
	{
		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x00017DC4 File Offset: 0x00015FC4
		// (set) Token: 0x060030EB RID: 12523 RVA: 0x00017DCC File Offset: 0x00015FCC
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
