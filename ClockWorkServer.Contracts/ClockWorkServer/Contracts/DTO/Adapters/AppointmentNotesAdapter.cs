using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C7E RID: 3198
	public static class AppointmentNotesAdapter
	{
		// Token: 0x060042BB RID: 17083 RVA: 0x00021450 File Offset: 0x0001F650
		public static bool GetIsPointOfContact(this NotesAppointmentDTO notesApp)
		{
			DateTime startDateTime = notesApp.StartDateTime;
			DateTime endDateTime = notesApp.EndDateTime;
			return startDateTime.Hour == 0 && endDateTime.Hour == 1 && startDateTime.Minute == 0 && startDateTime.Minute == 0;
		}
	}
}
