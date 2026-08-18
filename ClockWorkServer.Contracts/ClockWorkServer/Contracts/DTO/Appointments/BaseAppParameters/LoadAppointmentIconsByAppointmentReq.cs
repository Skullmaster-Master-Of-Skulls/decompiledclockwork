using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000949 RID: 2377
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentIconsByAppointmentReq : BaseMessageReq
	{
		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x00017C3D File Offset: 0x00015E3D
		// (set) Token: 0x060030AD RID: 12461 RVA: 0x00017C45 File Offset: 0x00015E45
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
