using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules
{
	// Token: 0x020000ED RID: 237
	public interface IStudentAppointmentBookingRuleManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000775 RID: 1909
		eStudentAppointmentBookingRuleType RuleType { get; }

		// Token: 0x06000776 RID: 1910
		AppointmentBookingRes ExecuteRuleCheck(AppointmentBookingReq bookingRequest, AppointmentBookingFilterParameters parameters);
	}
}
