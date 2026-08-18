using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.ClockWorkWeb.staff.schedule
{
	// Token: 0x02000102 RID: 258
	public class AppointmentWrapper
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x0000AF9E File Offset: 0x0000919E
		public AppointmentWrapper()
		{
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00038CF0 File Offset: 0x00036EF0
		public AppointmentWrapper(AppointmentDTO app)
		{
			this.AppointmentId = app.AppointmentId;
			this.Start = this.DateToJavascriptDateString(app.StartDateTime);
			this.End = this.DateToJavascriptDateString(app.EndDateTime);
			this.Subject = (app.SubTitle ?? "");
			this.Location = (app.Location ?? "").Trim();
			PersonBaseDTO room = app.GetRoom();
			bool flag = room != null;
			if (flag)
			{
				this.Location = ((this.Location.Length > 0) ? ((room.FirstName ?? "") + " - " + this.Location) : (room.FirstName ?? ""));
			}
			string memo = app.Memo;
			this.MemoPlainText = (((memo != null) ? memo.ConvertRtfToPlainText() : null) ?? "");
			List<AttendeeDTO> source = (from g in app.Attendees
			where g.Person.CoreGroup != eCoreGroupDTO.Rooms
			select g).ToList<AttendeeDTO>();
			this.Attendees = (from g in source
			select g.Person.GetName()).ToArray<string>();
			this.IsCancelled = app.IsCancelled;
			this.IsPrivate = app.IsPrivate;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00038E5C File Offset: 0x0003705C
		private string DateToJavascriptDateString(DateTime dt)
		{
			return dt.ToString("o");
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00038E7A File Offset: 0x0003707A
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x00038E82 File Offset: 0x00037082
		public string Start { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00038E8B File Offset: 0x0003708B
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x00038E93 File Offset: 0x00037093
		public string End { get; private set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00038E9C File Offset: 0x0003709C
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x00038EA4 File Offset: 0x000370A4
		public int AppointmentId { get; private set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00038EAD File Offset: 0x000370AD
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x00038EB5 File Offset: 0x000370B5
		public string Subject { get; private set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00038EBE File Offset: 0x000370BE
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x00038EC6 File Offset: 0x000370C6
		public string MemoPlainText { get; private set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00038ECF File Offset: 0x000370CF
		// (set) Token: 0x06000799 RID: 1945 RVA: 0x00038ED7 File Offset: 0x000370D7
		public string Location { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00038EE0 File Offset: 0x000370E0
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x00038EE8 File Offset: 0x000370E8
		public bool IsPrivate { get; private set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00038EF1 File Offset: 0x000370F1
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00038EF9 File Offset: 0x000370F9
		public bool IsCancelled { get; private set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00038F02 File Offset: 0x00037102
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x00038F0A File Offset: 0x0003710A
		public string[] Attendees { get; private set; }
	}
}
