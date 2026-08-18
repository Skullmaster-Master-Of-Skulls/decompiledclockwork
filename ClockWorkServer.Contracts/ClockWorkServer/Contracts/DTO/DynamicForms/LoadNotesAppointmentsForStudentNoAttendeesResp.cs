using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200062F RID: 1583
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotesAppointmentsForStudentNoAttendeesResp
	{
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x0000EA28 File Offset: 0x0000CC28
		// (set) Token: 0x0600203E RID: 8254 RVA: 0x0000EA30 File Offset: 0x0000CC30
		[DataMember]
		public IList<NotesAppointmentDTO> NotesAppointmentsNoAttendees { get; set; }
	}
}
