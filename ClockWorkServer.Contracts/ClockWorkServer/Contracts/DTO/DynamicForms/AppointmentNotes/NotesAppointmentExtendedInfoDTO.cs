using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes
{
	// Token: 0x020006C3 RID: 1731
	[DataContract(Namespace = "http://tpro.ca")]
	public class NotesAppointmentExtendedInfoDTO
	{
		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x00010297 File Offset: 0x0000E497
		// (set) Token: 0x0600235B RID: 9051 RVA: 0x0001029F File Offset: 0x0000E49F
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x000102A8 File Offset: 0x0000E4A8
		// (set) Token: 0x0600235D RID: 9053 RVA: 0x000102B0 File Offset: 0x0000E4B0
		[DataMember]
		public IList<AttendeeDTO> Attendees { get; set; }

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x0600235E RID: 9054 RVA: 0x000102B9 File Offset: 0x0000E4B9
		// (set) Token: 0x0600235F RID: 9055 RVA: 0x000102C1 File Offset: 0x0000E4C1
		[DataMember]
		public StudentClassTestDTO StudentClassTestInfo { get; set; }
	}
}
