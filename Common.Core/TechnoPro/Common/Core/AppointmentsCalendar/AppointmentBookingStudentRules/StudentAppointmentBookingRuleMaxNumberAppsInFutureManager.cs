using System;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules
{
	// Token: 0x02000151 RID: 337
	public class StudentAppointmentBookingRuleMaxNumberAppsInFutureManager : IStudentAppointmentBookingRuleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000F19 RID: 3865 RVA: 0x00071454 File Offset: 0x0006F654
		public eStudentAppointmentBookingRuleType RuleType
		{
			get
			{
				return eStudentAppointmentBookingRuleType.MaxNumberInFuture;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00071457 File Offset: 0x0006F657
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x0007145F File Offset: 0x0006F65F
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F1C RID: 3868 RVA: 0x00071468 File Offset: 0x0006F668
		public AppointmentBookingRes ExecuteRuleCheck(AppointmentBookingReq bookingRequest, AppointmentBookingFilterParameters parameters)
		{
			int maxNumberOfAppointmentsInFuture = parameters.MaxNumberOfAppointmentsInFuture;
			bool flag = maxNumberOfAppointmentsInFuture < 1;
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
				bool flag2 = parameters.MaxNumberOfAppointmentsInFutureAppTypeIds == null || parameters.MaxNumberOfAppointmentsInFutureAppTypeIds.Length < 1;
				if (flag2)
				{
					parameters.MaxNumberOfAppointmentsInFutureAppTypeIds = null;
				}
				int numberOfNonCancelledAppointments = appointmentManager.GetNumberOfNonCancelledAppointments(bookingRequest.StudentPersonId, DateTime.Now, null, true, parameters.MaxNumberOfAppointmentsInFutureAppTypeIds);
				bool flag3 = numberOfNonCancelledAppointments < maxNumberOfAppointmentsInFuture;
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
							"You have too many appointments booked in the future.  You currently have ",
							numberOfNonCancelledAppointments.ToString(),
							" appointment(s), and you are only allowed to have ",
							maxNumberOfAppointmentsInFuture.ToString(),
							" appointment(s)."
						}),
						PrivateMessage = string.Format("Failed MaxNumberInFuture: currAppCount={0}:maxAppCount={1}", numberOfNonCancelledAppointments.ToString(), maxNumberOfAppointmentsInFuture.ToString())
					};
				}
			}
			return result;
		}
	}
}
