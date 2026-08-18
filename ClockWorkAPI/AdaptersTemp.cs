using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace ClockWorkAPI
{
	// Token: 0x020000A2 RID: 162
	public static class AdaptersTemp
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x00030AA8 File Offset: 0x0002FAA8
		public static LookupCourseBaseDTO GetCourse(this AppointmentDTO Appointment)
		{
			LookupCourseBaseDTO result;
			if (Appointment == null || Appointment.TestExamInfo == null || Appointment.TestExamInfo.Course == null)
			{
				result = null;
			}
			else
			{
				result = Appointment.TestExamInfo.Course;
			}
			return result;
		}
	}
}
