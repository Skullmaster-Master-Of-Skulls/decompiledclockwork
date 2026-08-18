using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes;

namespace TechnoPro.Common.ICore.DynamicForms
{
	// Token: 0x02000095 RID: 149
	public interface IAppointmentNotesManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600042D RID: 1069
		IList<int> LoadAllAppointmentIdsWithNotes(int PersonId, Range<DateTime> DateRange, params int[] ScreenNums);

		// Token: 0x0600042E RID: 1070
		string GetAppointmentNotesSummaryHtml(int PersonId, int[] AppointmentIds, int[] ScreenNums);

		// Token: 0x0600042F RID: 1071
		void SaveAppointmentNotesToFirstRtfInFirstFormAttachedToAppointmentType(int StudentPersonId, int AppointmentId, int AppTypeId, string NotesRtf);

		// Token: 0x06000430 RID: 1072
		string LoadAppointmentNotesRtfFromFirstRtfInFirstFormAttachedToAppointmentType(int StudentPersonId, int AppointmentId, int AppTypeId);

		// Token: 0x06000431 RID: 1073
		IList<NotesAppointment> LoadNotesAppointmentsForStudentNoAttendees(int primaryStudentPersonId, Range<DateTime> dateRange, IList<int> appTypeIds, IList<int> screenNums);

		// Token: 0x06000432 RID: 1074
		NotesAppointment LoadNotesAppointmentByAppointmentId(int appointmentId, int primaryStudentPersonId, IList<int> screenNums);

		// Token: 0x06000433 RID: 1075
		IList<NotesAppointmentExtendedInfo> LoadNotesAppointmentExtendedInfos(params int[] appointmentIds);
	}
}
