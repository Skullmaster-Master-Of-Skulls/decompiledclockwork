using System;

namespace TechnoPro.ClockWorkWeb.user.TutoringStudents
{
	// Token: 0x02000051 RID: 81
	public class AvailabilityEvent
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0000AF9E File Offset: 0x0000919E
		public AvailabilityEvent()
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000CA44 File Offset: 0x0000AC44
		private string DateToJavascriptDateString(DateTime dt)
		{
			return dt.ToString("o");
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000CA64 File Offset: 0x0000AC64
		public AvailabilityEvent(int tutorId, DateTime date, TimeSpan startTime, TimeSpan endTime, string title)
		{
			this.TutorId = tutorId;
			this.Start = this.DateToJavascriptDateString(date.Date.Add(startTime));
			this.End = this.DateToJavascriptDateString(date.Date.Add(endTime));
			this.Title = title;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000CAC5 File Offset: 0x0000ACC5
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000CACD File Offset: 0x0000ACCD
		public int TutorId { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000CAD6 File Offset: 0x0000ACD6
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000CADE File Offset: 0x0000ACDE
		public string Start { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000CAE7 File Offset: 0x0000ACE7
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000CAEF File Offset: 0x0000ACEF
		public string End { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000CAF8 File Offset: 0x0000ACF8
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000CB00 File Offset: 0x0000AD00
		public string Title { get; private set; }
	}
}
