using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x02000103 RID: 259
	public class CalendarEvent
	{
		// Token: 0x060007A0 RID: 1952 RVA: 0x0000AF9E File Offset: 0x0000919E
		public CalendarEvent()
		{
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00038F14 File Offset: 0x00037114
		private string DateToJavascriptDateString(DateTime dt)
		{
			return dt.ToString("o");
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00038F34 File Offset: 0x00037134
		public CalendarEvent(AppointmentDTO app, IList<eAppointmentPermissionRestriction> restrictions)
		{
			this.Id = app.AppointmentId;
			TimeSpan timeOfDay = app.StartDateTime.TimeOfDay;
			TimeSpan timeOfDay2 = app.EndDateTime.TimeOfDay;
			int num = Convert.ToInt32((timeOfDay2 - timeOfDay).TotalMinutes);
			bool flag = num >= 1438;
			if (flag)
			{
				this.IsFullDay = true;
			}
			this.Start = this.DateToJavascriptDateString(app.StartDateTime);
			this.End = this.DateToJavascriptDateString(app.EndDateTime);
			this.Title = app.GetTitleAndSubtitle();
			int[] array;
			if (restrictions == null)
			{
				array = null;
			}
			else
			{
				array = (from g in restrictions
				select (int)g).ToArray<int>();
			}
			this.Restrictions = (array ?? new int[0]);
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00039017 File Offset: 0x00037217
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x0003901F File Offset: 0x0003721F
		public bool IsFullDay { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00039028 File Offset: 0x00037228
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x00039030 File Offset: 0x00037230
		public string Start { get; private set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00039039 File Offset: 0x00037239
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x00039041 File Offset: 0x00037241
		public string End { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0003904A File Offset: 0x0003724A
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x00039052 File Offset: 0x00037252
		public string Title { get; private set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0003905B File Offset: 0x0003725B
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x00039063 File Offset: 0x00037263
		public int Id { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0003906C File Offset: 0x0003726C
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x00039074 File Offset: 0x00037274
		public int[] Restrictions { get; set; }
	}
}
