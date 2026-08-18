using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.UI.Web.Entity.appt;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.AppointmentsCalendar
{
	// Token: 0x0200001C RID: 28
	public interface IAppointmentClientCalendarWebClientManager
	{
		// Token: 0x0600007A RID: 122
		IList<AppointmentView> LoadAvailabilityForAppointmentBookingModule(int studentPid, IList<AttendeeView> users, Channel activeChannel, DateTime StartDate, DateTime EndDate);
	}
}
