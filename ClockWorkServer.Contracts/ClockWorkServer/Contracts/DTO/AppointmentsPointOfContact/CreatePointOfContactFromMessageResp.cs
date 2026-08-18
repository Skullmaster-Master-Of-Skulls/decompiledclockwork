using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsPointOfContact
{
	// Token: 0x02000924 RID: 2340
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreatePointOfContactFromMessageResp
	{
		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06002F63 RID: 12131 RVA: 0x000168BD File Offset: 0x00014ABD
		// (set) Token: 0x06002F64 RID: 12132 RVA: 0x000168C5 File Offset: 0x00014AC5
		[DataMember]
		public int NewAppointmentId { get; set; }
	}
}
