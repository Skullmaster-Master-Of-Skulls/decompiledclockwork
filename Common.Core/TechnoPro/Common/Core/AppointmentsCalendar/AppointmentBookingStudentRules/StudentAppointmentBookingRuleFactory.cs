using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.ICore.AppointmentsCalendar.AppointmentBookingStudentRules;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules
{
	// Token: 0x0200014F RID: 335
	public static class StudentAppointmentBookingRuleFactory
	{
		// Token: 0x06000F11 RID: 3857 RVA: 0x00071178 File Offset: 0x0006F378
		public static AppointmentBookingRes ExecuteBookingFilters(IList<IStudentAppointmentBookingRuleManager> allRuleManagers, eStudentAppointmentBookingRuleAppliesTo appliesTo, AppointmentBookingReq req, AppointmentBookingFilterParameters parameters)
		{
			List<IStudentAppointmentBookingRuleManager> list = (from g in allRuleManagers
			where g.RuleType.GetAttribute<StudentAppointmentBookingRuleTypeAttribute>().AppliesTo == appliesTo
			select g).ToList<IStudentAppointmentBookingRuleManager>();
			AppointmentBookingRes appointmentBookingRes = null;
			foreach (IStudentAppointmentBookingRuleManager studentAppointmentBookingRuleManager in list)
			{
				appointmentBookingRes = studentAppointmentBookingRuleManager.ExecuteRuleCheck(req, parameters);
				bool flag = !appointmentBookingRes.PassedChecks;
				if (flag)
				{
					return appointmentBookingRes;
				}
			}
			return appointmentBookingRes;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00071214 File Offset: 0x0006F414
		public static IList<IStudentAppointmentBookingRuleManager> GetAllStudentRuleManagers(OperationContext opContext)
		{
			var source = (from h in (eStudentAppointmentBookingRuleType[])Enum.GetValues(typeof(eStudentAppointmentBookingRuleType))
			select new
			{
				Enum = h,
				Attr = h.GetAttribute<StudentAppointmentBookingRuleTypeAttribute>()
			} into m
			where m != null && !string.IsNullOrWhiteSpace(m.Attr.ManagerClassName)
			select m).ToList();
			return source.Select(delegate(g)
			{
				string typeName = "TechnoPro.Common.Core.AppointmentsCalendar.AppointmentBookingStudentRules." + g.Attr.ManagerClassName + ", Common.Core";
				Type type = Type.GetType(typeName);
				IStudentAppointmentBookingRuleManager studentAppointmentBookingRuleManager = Activator.CreateInstance(type) as IStudentAppointmentBookingRuleManager;
				studentAppointmentBookingRuleManager.OpContext = opContext;
				return studentAppointmentBookingRuleManager;
			}).ToList<IStudentAppointmentBookingRuleManager>();
		}
	}
}
