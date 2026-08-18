using System;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules
{
	// Token: 0x0200014D RID: 333
	public class StudentAppointmentBookingRuleCheckStaffDoubleBookedManager : IStudentAppointmentBookingRuleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00070FEF File Offset: 0x0006F1EF
		public eStudentAppointmentBookingRuleType RuleType
		{
			get
			{
				return eStudentAppointmentBookingRuleType.CheckStaffDoubleBooked;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00070FF2 File Offset: 0x0006F1F2
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x00070FFA File Offset: 0x0006F1FA
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F09 RID: 3849 RVA: 0x00071004 File Offset: 0x0006F204
		public AppointmentBookingRes ExecuteRuleCheck(AppointmentBookingReq bookingRequest, AppointmentBookingFilterParameters parameters)
		{
			bool allowDoubleBookingStaff = parameters.AllowDoubleBookingStaff;
			AppointmentBookingRes result;
			if (allowDoubleBookingStaff)
			{
				result = new AppointmentBookingRes
				{
					PassedChecks = true
				};
			}
			else
			{
				bool flag = this.CheckDoubleBooked(bookingRequest.StaffPersonId, bookingRequest.StartDateTime, bookingRequest.EndDateTime);
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
						PublicMessage = "Somebody else scheduled the time slot you were trying to book between when you selected it in the last step and now.  Please go back and find a different time slot to book.",
						PrivateMessage = "Failed CheckTutorDoubleBooked"
					};
				}
			}
			return result;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00071088 File Offset: 0x0006F288
		private bool CheckDoubleBooked(int PersonId, DateTime StartDateTime, DateTime EndDateTime)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(this.OpContext);
			return appointmentAttendeeManager.CheckIfDoubleBooked(PersonId, StartDateTime, EndDateTime, Array.Empty<int>());
		}
	}
}
