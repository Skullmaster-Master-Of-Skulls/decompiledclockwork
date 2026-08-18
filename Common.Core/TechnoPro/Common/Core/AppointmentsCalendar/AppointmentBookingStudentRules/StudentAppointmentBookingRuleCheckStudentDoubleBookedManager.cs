using System;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules
{
	// Token: 0x0200014E RID: 334
	public class StudentAppointmentBookingRuleCheckStudentDoubleBookedManager : IStudentAppointmentBookingRuleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x000710B4 File Offset: 0x0006F2B4
		public eStudentAppointmentBookingRuleType RuleType
		{
			get
			{
				return eStudentAppointmentBookingRuleType.CheckStudentDoubleBooked;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x000710B7 File Offset: 0x0006F2B7
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x000710BF File Offset: 0x0006F2BF
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F0F RID: 3855 RVA: 0x000710C8 File Offset: 0x0006F2C8
		public AppointmentBookingRes ExecuteRuleCheck(AppointmentBookingReq bookingRequest, AppointmentBookingFilterParameters parameters)
		{
			bool allowDoubleBookingStudent = parameters.AllowDoubleBookingStudent;
			AppointmentBookingRes result;
			if (allowDoubleBookingStudent)
			{
				result = new AppointmentBookingRes
				{
					PassedChecks = true
				};
			}
			else
			{
				bool flag = this.CheckDoubleBooked(bookingRequest.StudentPersonId, bookingRequest.StartDateTime, bookingRequest.EndDateTime);
				bool flag2 = !flag;
				if (flag2)
				{
					result = new AppointmentBookingRes
					{
						PassedChecks = true
					};
				}
				else
				{
					result = new AppointmentBookingRes
					{
						PassedChecks = false,
						PublicMessage = "You are already scheduled for another appointment at the same time as the appointment you are trying to schedule now.",
						PrivateMessage = "Failed CheckStudentDoubleBooked"
					};
				}
			}
			return result;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0007114C File Offset: 0x0006F34C
		private bool CheckDoubleBooked(int PersonId, DateTime StartDateTime, DateTime EndDateTime)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(this.OpContext);
			return appointmentAttendeeManager.CheckIfDoubleBooked(PersonId, StartDateTime, EndDateTime, Array.Empty<int>());
		}
	}
}
