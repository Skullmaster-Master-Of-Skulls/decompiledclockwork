using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x02000054 RID: 84
	public class CalendarAppointmentWrapper
	{
		// Token: 0x0600020B RID: 523 RVA: 0x0000AF9E File Offset: 0x0000919E
		public CalendarAppointmentWrapper()
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000CF8C File Offset: 0x0000B18C
		public CalendarAppointmentWrapper(AppointmentDTO app, IList<int> tutorPids)
		{
			this.AppointmentId = app.AppointmentId;
			this.UrlId = NavigatorClientManager.CurrentInstance.ConvertIntParameterToUrlString(app.AppointmentId);
			this.Start = this.DateToJavascriptDateString(app.StartDateTime);
			this.End = this.DateToJavascriptDateString(app.EndDateTime);
			this.Title = app.GetTitleAndSubtitle();
			this.DateAndTimeForDisplay = string.Concat(new string[]
			{
				app.StartDateTime.ToString("ddd MMM d, yyyy"),
				" at ",
				app.StartDateTime.ToString("h:mm tt"),
				" to ",
				app.EndDateTime.ToString("h:mm tt")
			});
			List<AttendeeDTO> source = (from g in app.Attendees
			where tutorPids.Contains(g.Person.PersonId)
			select g).ToList<AttendeeDTO>();
			this.Tutors = string.Join(", ", (from g in source
			select g.Person.GetName()).ToArray<string>());
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		private string DateToJavascriptDateString(DateTime dt)
		{
			return dt.ToString("o");
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000D0E2 File Offset: 0x0000B2E2
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000D0EA File Offset: 0x0000B2EA
		public string UrlId { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000D0F3 File Offset: 0x0000B2F3
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000D0FB File Offset: 0x0000B2FB
		public int AppointmentId { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000D104 File Offset: 0x0000B304
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000D10C File Offset: 0x0000B30C
		public string Start { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000D115 File Offset: 0x0000B315
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000D11D File Offset: 0x0000B31D
		public string End { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000D126 File Offset: 0x0000B326
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000D12E File Offset: 0x0000B32E
		public string Title { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000D137 File Offset: 0x0000B337
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000D13F File Offset: 0x0000B33F
		public string Tutors { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000D148 File Offset: 0x0000B348
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000D150 File Offset: 0x0000B350
		public string DateAndTimeForDisplay { get; private set; }
	}
}
