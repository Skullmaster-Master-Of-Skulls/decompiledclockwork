using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes;

namespace TechnoPro.Common.DAO.DynamicForms
{
	// Token: 0x0200007F RID: 127
	public interface IAppointmentNotesDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600031D RID: 797
		IList<int> LoadAllAppointmentIdsWithNotes(int PersonId, Range<DateTime> DateRange, IList<int> AllowedAppTypeIds, params int[] ScreenNums);

		// Token: 0x0600031E RID: 798
		IList<NotesAppointment> LoadNotesAppointmentsForStudentNoAttendeesNoHasNotes(int primaryStudentPersonId, Range<DateTime> dateRange, IList<int> appTypeIds);

		// Token: 0x0600031F RID: 799
		NotesAppointment LoadNotesAppointmentByAppointmentId(int appointmentId);
	}
}
