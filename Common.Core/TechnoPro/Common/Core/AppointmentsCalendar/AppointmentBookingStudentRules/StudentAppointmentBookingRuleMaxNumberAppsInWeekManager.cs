using System;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules
{
	// Token: 0x02000152 RID: 338
	public class StudentAppointmentBookingRuleMaxNumberAppsInWeekManager : IStudentAppointmentBookingRuleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0007157E File Offset: 0x0006F77E
		public eStudentAppointmentBookingRuleType RuleType
		{
			get
			{
				return eStudentAppointmentBookingRuleType.MaxNumberInAWeek;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x00071581 File Offset: 0x0006F781
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x00071589 File Offset: 0x0006F789
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F21 RID: 3873 RVA: 0x00071594 File Offset: 0x0006F794
		public AppointmentBookingRes ExecuteRuleCheck(AppointmentBookingReq bookingRequest, AppointmentBookingFilterParameters parameters)
		{
			int maxNumberOfAppointmentsPerWeek = parameters.MaxNumberOfAppointmentsPerWeek;
			bool flag = maxNumberOfAppointmentsPerWeek < 1;
			AppointmentBookingRes result;
			if (flag)
			{
				result = new AppointmentBookingRes
				{
					PassedChecks = true
				};
			}
			else
			{
				IAppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
				DateTime date = bookingRequest.StartDateTime.Date;
				int dayOfWeek = (int)date.DayOfWeek;
				DateTime startDate = date.AddDays((double)(-(double)dayOfWeek));
				DateTime value = startDate.AddDays(6.0);
				bool flag2 = parameters.MaxNumberOfAppointmentsPerWeekAppTypeIds == null || parameters.MaxNumberOfAppointmentsPerWeekAppTypeIds.Length < 1;
				if (flag2)
				{
					parameters.MaxNumberOfAppointmentsPerWeekAppTypeIds = null;
				}
				int numberOfNonCancelledAppointments = appointmentManager.GetNumberOfNonCancelledAppointments(bookingRequest.StudentPersonId, startDate, new DateTime?(value), true, parameters.MaxNumberOfAppointmentsPerWeekAppTypeIds);
				bool flag3 = numberOfNonCancelledAppointments < maxNumberOfAppointmentsPerWeek;
				if (flag3)
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
						PublicMessage = string.Concat(new string[]
						{
							"You have too many appointments booked in the same week of the appointment you are trying to schedule.  You currently have ",
							numberOfNonCancelledAppointments.ToString(),
							" appointment(s) the week of ",
							startDate.ToString("ddd MMM d, yyyy"),
							" to ",
							value.ToString("ddd MMM d, yyyy"),
							", and you are only allowed to have ",
							maxNumberOfAppointmentsPerWeek.ToString(),
							" appointment(s)."
						}),
						PrivateMessage = string.Concat(new string[]
						{
							"Failed MaxNumberInWeek: currAppCount=",
							numberOfNonCancelledAppointments.ToString(),
							":maxAppCountInWeek=",
							maxNumberOfAppointmentsPerWeek.ToString(),
							":sd=",
							startDate.ToString("yyyy-MM-dd"),
							":ed=",
							value.ToString("yyyy-MM-dd")
						})
					};
				}
			}
			return result;
		}
	}
}
