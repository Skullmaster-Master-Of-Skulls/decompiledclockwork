using System;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules
{
	// Token: 0x02000150 RID: 336
	public class StudentAppointmentBookingRuleMaxNumberAppsInDayManager : IStudentAppointmentBookingRuleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000712AC File Offset: 0x0006F4AC
		public eStudentAppointmentBookingRuleType RuleType
		{
			get
			{
				return eStudentAppointmentBookingRuleType.MaxNumberPerday;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x000712AF File Offset: 0x0006F4AF
		// (set) Token: 0x06000F16 RID: 3862 RVA: 0x000712B7 File Offset: 0x0006F4B7
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F17 RID: 3863 RVA: 0x000712C0 File Offset: 0x0006F4C0
		public AppointmentBookingRes ExecuteRuleCheck(AppointmentBookingReq bookingRequest, AppointmentBookingFilterParameters parameters)
		{
			bool flag = parameters.MaxNumberOfAppointmentsPerDay < 1;
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
				DateTime value = date;
				bool flag2 = parameters.MaxNumberOfAppointmentsPerDayAppTypeIds == null || parameters.MaxNumberOfAppointmentsPerDayAppTypeIds.Length < 1;
				if (flag2)
				{
					parameters.MaxNumberOfAppointmentsPerDayAppTypeIds = null;
				}
				int numberOfNonCancelledAppointments = appointmentManager.GetNumberOfNonCancelledAppointments(bookingRequest.StudentPersonId, date, new DateTime?(value), true, parameters.MaxNumberOfAppointmentsPerDayAppTypeIds);
				bool flag3 = numberOfNonCancelledAppointments < parameters.MaxNumberOfAppointmentsPerDay;
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
							"You have too many appointments booked on the same day of the appointment you are trying to schedule.  You currently have ",
							numberOfNonCancelledAppointments.ToString(),
							" appointment(s) the day of ",
							date.ToString("ddd MMM d, yyyy"),
							", and you are only allowed to have ",
							parameters.MaxNumberOfAppointmentsPerDay.ToString(),
							" appointment(s)."
						}),
						PrivateMessage = string.Concat(new string[]
						{
							"Failed MaxNumberInDay: currAppCount=",
							numberOfNonCancelledAppointments.ToString(),
							":maxAppCountInDay=",
							parameters.MaxNumberOfAppointmentsPerDay.ToString(),
							":sd=",
							date.ToString("yyyy-MM-dd"),
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
