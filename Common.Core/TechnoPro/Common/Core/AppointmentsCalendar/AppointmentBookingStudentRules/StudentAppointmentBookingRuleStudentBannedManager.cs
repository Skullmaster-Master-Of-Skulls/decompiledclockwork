using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;

namespace TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules
{
	// Token: 0x02000153 RID: 339
	public class StudentAppointmentBookingRuleStudentBannedManager : IStudentAppointmentBookingRuleManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00071755 File Offset: 0x0006F955
		public eStudentAppointmentBookingRuleType RuleType
		{
			get
			{
				return eStudentAppointmentBookingRuleType.CheckStudentBanned;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00071758 File Offset: 0x0006F958
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x00071760 File Offset: 0x0006F960
		public OperationContext OpContext { get; set; }

		// Token: 0x06000F26 RID: 3878 RVA: 0x0007176C File Offset: 0x0006F96C
		public AppointmentBookingRes ExecuteRuleCheck(AppointmentBookingReq bookingRequest, AppointmentBookingFilterParameters parameters)
		{
			int bannedExpiryDateCid = parameters.BannedExpiryDateCid;
			bool flag = bannedExpiryDateCid < 1;
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
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				IList<IDynamicDataSerializableItem> list = dynamicDataManager.LoadDynamicDataItemsByControlIds(new DynamicDataContext
				{
					PrimaryId = bookingRequest.StudentPersonId
				}, new List<int>
				{
					bannedExpiryDateCid
				}, eDynamicFormType.PerStudent);
				bool flag2 = list == null || list.Count < 1;
				if (flag2)
				{
					result = new AppointmentBookingRes
					{
						PassedChecks = true
					};
				}
				else
				{
					bool flag3 = (from t in list
					select t.WriteToStorage() into storageItem
					where storageItem.DateTimeValue != null
					select storageItem.DateTimeValue.Value >= DateTime.Now).FirstOrDefault<bool>();
					bool flag4 = !flag3;
					if (flag4)
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
							PrivateMessage = "Student is banned",
							PublicMessage = "Your account is currently restricted and not able to book appointments.  Please contact us for more information."
						};
					}
				}
			}
			return result;
		}
	}
}
