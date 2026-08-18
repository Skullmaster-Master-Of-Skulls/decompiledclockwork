using System;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules
{
	// Token: 0x02000154 RID: 340
	public class StudentAppointmentBookingRuleStudentCheckValidDateTimeManager : IStudentAppointmentBookingRuleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x000718B4 File Offset: 0x0006FAB4
		public eStudentAppointmentBookingRuleType RuleType
		{
			get
			{
				return eStudentAppointmentBookingRuleType.MaxNumberOfNoShows;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x000718B7 File Offset: 0x0006FAB7
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x000718BF File Offset: 0x0006FABF
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F2B RID: 3883 RVA: 0x000718C8 File Offset: 0x0006FAC8
		public AppointmentBookingRes ExecuteRuleCheck(AppointmentBookingReq bookingRequest, AppointmentBookingFilterParameters parameters)
		{
			bool flag = bookingRequest.StartDateTime < DateTime.Now;
			AppointmentBookingRes result;
			if (flag)
			{
				result = new AppointmentBookingRes
				{
					PassedChecks = false,
					PrivateMessage = "Booking request start date time is less than right now",
					PublicMessage = "Booking request is for a time in the past"
				};
			}
			else
			{
				bool flag2 = bookingRequest.StartDateTime.Date != bookingRequest.EndDateTime.Date;
				if (flag2)
				{
					result = new AppointmentBookingRes
					{
						PassedChecks = false,
						PrivateMessage = "The booking request date is invalid",
						PublicMessage = "Invalid booking request date (start vs end)"
					};
				}
				else
				{
					double totalMinutes = (bookingRequest.EndDateTime - bookingRequest.StartDateTime).TotalMinutes;
					bool flag3 = totalMinutes <= 0.0;
					if (flag3)
					{
						result = new AppointmentBookingRes
						{
							PassedChecks = false,
							PrivateMessage = "The booking request time is invalid",
							PublicMessage = "Invalid booking request time"
						};
					}
					else
					{
						result = new AppointmentBookingRes
						{
							PassedChecks = true
						};
					}
				}
			}
			return result;
		}
	}
}
