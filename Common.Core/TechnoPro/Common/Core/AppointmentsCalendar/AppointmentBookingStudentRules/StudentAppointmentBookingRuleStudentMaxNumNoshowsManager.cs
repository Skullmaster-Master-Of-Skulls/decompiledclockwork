using System;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules
{
	// Token: 0x02000155 RID: 341
	public class StudentAppointmentBookingRuleStudentMaxNumNoshowsManager : IStudentAppointmentBookingRuleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x000718B4 File Offset: 0x0006FAB4
		public eStudentAppointmentBookingRuleType RuleType
		{
			get
			{
				return eStudentAppointmentBookingRuleType.MaxNumberOfNoShows;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x000719CD File Offset: 0x0006FBCD
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x000719D5 File Offset: 0x0006FBD5
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F30 RID: 3888 RVA: 0x000719E0 File Offset: 0x0006FBE0
		public AppointmentBookingRes ExecuteRuleCheck(AppointmentBookingReq bookingRequest, AppointmentBookingFilterParameters parameters)
		{
			int maxNumberOfNoShows = parameters.MaxNumberOfNoShows;
			IAppointmentManager appointmentManager = new AppointmentManager(this.OpContext);
			bool flag = parameters.MaxNumberOfNoShowsAppTypeIds == null || parameters.MaxNumberOfNoShowsAppTypeIds.Length < 1;
			if (flag)
			{
				parameters.MaxNumberOfNoShowsAppTypeIds = null;
			}
			int numberOfConsecutiveNoshows = appointmentManager.GetNumberOfConsecutiveNoshows(bookingRequest.StudentPersonId, DateTime.Now, maxNumberOfNoShows, parameters.MaxNumberOfNoShowsAppTypeIds);
			bool flag2 = maxNumberOfNoShows < 1 || numberOfConsecutiveNoshows < maxNumberOfNoShows;
			AppointmentBookingRes result;
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
					PublicMessage = string.Concat(new string[]
					{
						"You have too many no-show appointments.  Your last ",
						numberOfConsecutiveNoshows.ToString(),
						" appointment(s) was/were all no-shows; you are only allowed to have a maximum consecutive no-show count of ",
						maxNumberOfNoShows.ToString(),
						" appointment(s)."
					}),
					PrivateMessage = string.Format("Failed MaxNumberNoShows:numNoshows={0}:Max={1}", numberOfConsecutiveNoshows, maxNumberOfNoShows)
				};
			}
			return result;
		}
	}
}
