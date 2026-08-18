using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000631 RID: 1585
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotesAppointmentByAppointmentIdResp
	{
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		// (set) Token: 0x06002048 RID: 8264 RVA: 0x0000EA74 File Offset: 0x0000CC74
		[DataMember]
		public NotesAppointmentDTO NotesAppointment { get; set; }
	}
}
