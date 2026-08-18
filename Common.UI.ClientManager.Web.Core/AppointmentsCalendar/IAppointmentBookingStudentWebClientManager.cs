using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.AppointmentsCalendar
{
	// Token: 0x0200001B RID: 27
	public interface IAppointmentBookingStudentWebClientManager
	{
		// Token: 0x06000077 RID: 119
		IList<Channel> GetAppointmentBookingActiveChannels(int studentPersonId);

		// Token: 0x06000078 RID: 120
		bool IsStudentBannedFromOnlineAppointmentBooking(int PersonId);

		// Token: 0x06000079 RID: 121
		DateTime? MarkStudentBannedFromOnlineAppointmentBooking(int PersonId);
	}
}
