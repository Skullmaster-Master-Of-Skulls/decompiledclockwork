using System;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules
{
	// Token: 0x0200014C RID: 332
	public class StudentAppointmentBookingRuleCheckCutoffTimeManager : IStudentAppointmentBookingRuleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00070EDD File Offset: 0x0006F0DD
		public eStudentAppointmentBookingRuleType RuleType
		{
			get
			{
				return eStudentAppointmentBookingRuleType.CheckCutoffTime;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00070EE0 File Offset: 0x0006F0E0
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x00070EE8 File Offset: 0x0006F0E8
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F04 RID: 3844 RVA: 0x00070EF4 File Offset: 0x0006F0F4
		public AppointmentBookingRes ExecuteRuleCheck(AppointmentBookingReq bookingRequest, AppointmentBookingFilterParameters parameters)
		{
			CutoffTime cutoffTime = parameters.CutoffTime;
			bool flag = cutoffTime == null || !cutoffTime.Enabled;
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
				DateTime? minimumDateForBeforeTypeCutoff = cutoffTime.GetMinimumDateForBeforeTypeCutoff();
				bool flag2 = minimumDateForBeforeTypeCutoff != null && bookingRequest.StartDateTime < minimumDateForBeforeTypeCutoff.Value;
				if (flag2)
				{
					result = new AppointmentBookingRes
					{
						PassedChecks = false,
						PublicMessage = "The cutoff time has passed for the time you are trying to schedule.  You cannot schedule any appointments before " + ((minimumDateForBeforeTypeCutoff != null) ? minimumDateForBeforeTypeCutoff.Value.ToString("ddd MMM d, yyyy h:mm tt") : "N/A") + ".",
						PrivateMessage = "Failed CheckCutoffTime:MinDateCanBookFor=" + ((minimumDateForBeforeTypeCutoff != null) ? minimumDateForBeforeTypeCutoff.Value.ToString("yyyy-MM-dd h:mm tt") : "N/A")
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
			return result;
		}
	}
}
