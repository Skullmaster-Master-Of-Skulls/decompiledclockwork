using System;
using TechnoPro.Common.UI.Web.Entity.AppointmentsTestBooking.AutoTestBooking;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.AppointmentsTestBooking
{
	// Token: 0x0200001A RID: 26
	public interface IAutoTestBookingWebClientManager
	{
		// Token: 0x06000075 RID: 117
		MinMaxDateRangeValue FigureOutMinMaxDateRangeStudentIsAllowedToBookForExam(int PersonId);

		// Token: 0x06000076 RID: 118
		MinMaxDateRangeValue FigureOutMinMaxDateRangeStudentIsAllowedToBookForTest(int PersonId);
	}
}
