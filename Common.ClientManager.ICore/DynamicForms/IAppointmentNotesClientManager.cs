using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.DynamicForms
{
	// Token: 0x0200005C RID: 92
	public interface IAppointmentNotesClientManager : IWebService
	{
		// Token: 0x060002B1 RID: 689
		IList<int> LoadAllAppointmentIdsWithNotes(int PersonId, Range<DateTime> DateRange, params int[] ScreenNums);

		// Token: 0x060002B2 RID: 690
		string GetAppointmentNotesSummaryHtml(int PersonId, int[] AppointmentIds, int[] ScreenNums);

		// Token: 0x060002B3 RID: 691
		void SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(int StudentPersonId, int AppointmentId, int AppTypeId, string NotesRtf);

		// Token: 0x060002B4 RID: 692
		string LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(int StudentPersonId, int AppointmentId, int AppTypeId);

		// Token: 0x060002B5 RID: 693
		IList<NotesAppointmentDTO> LoadNotesAppointmentsForStudentNoAttendees(int primaryStudentPersonId, Range<DateTime> dateRange, IList<int> appTypeIds, IList<int> screenNums);

		// Token: 0x060002B6 RID: 694
		NotesAppointmentDTO LoadNotesAppointmentByAppointmentId(int appointmentId, int primaryStudentPersonId, IList<int> screenNums);

		// Token: 0x060002B7 RID: 695
		IList<NotesAppointmentExtendedInfoDTO> LoadNotesAppointmentExtendedInfos(params int[] appointmentIds);
	}
}
