using System;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x02001313 RID: 4883
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class SchedulerPostBackEvent
	{
		// Token: 0x170041C1 RID: 16833
		// (get) Token: 0x0600CC50 RID: 52304 RVA: 0x002D8C66 File Offset: 0x002D6E66
		// (set) Token: 0x0600CC51 RID: 52305 RVA: 0x002D8C6E File Offset: 0x002D6E6E
		public object AppointmentID { get; set; }

		// Token: 0x170041C2 RID: 16834
		// (get) Token: 0x0600CC52 RID: 52306 RVA: 0x002D8C77 File Offset: 0x002D6E77
		// (set) Token: 0x0600CC53 RID: 52307 RVA: 0x002D8C7F File Offset: 0x002D6E7F
		public string StartDate { get; set; }

		// Token: 0x170041C3 RID: 16835
		// (get) Token: 0x0600CC54 RID: 52308 RVA: 0x002D8C88 File Offset: 0x002D6E88
		// (set) Token: 0x0600CC55 RID: 52309 RVA: 0x002D8C90 File Offset: 0x002D6E90
		public DateTime StartDateParsed { get; set; }

		// Token: 0x170041C4 RID: 16836
		// (get) Token: 0x0600CC56 RID: 52310 RVA: 0x002D8C99 File Offset: 0x002D6E99
		// (set) Token: 0x0600CC57 RID: 52311 RVA: 0x002D8CA1 File Offset: 0x002D6EA1
		public string EndDate { get; set; }

		// Token: 0x170041C5 RID: 16837
		// (get) Token: 0x0600CC58 RID: 52312 RVA: 0x002D8CAA File Offset: 0x002D6EAA
		// (set) Token: 0x0600CC59 RID: 52313 RVA: 0x002D8CB2 File Offset: 0x002D6EB2
		public DateTime EndDateParsed { get; set; }

		// Token: 0x170041C6 RID: 16838
		// (get) Token: 0x0600CC5A RID: 52314 RVA: 0x002D8CBB File Offset: 0x002D6EBB
		// (set) Token: 0x0600CC5B RID: 52315 RVA: 0x002D8CC3 File Offset: 0x002D6EC3
		public SchedulerPostBackCommand Command { get; set; }

		// Token: 0x170041C7 RID: 16839
		// (get) Token: 0x0600CC5C RID: 52316 RVA: 0x002D8CCC File Offset: 0x002D6ECC
		// (set) Token: 0x0600CC5D RID: 52317 RVA: 0x002D8CD4 File Offset: 0x002D6ED4
		public string ContextMenuCommandName { get; set; }

		// Token: 0x170041C8 RID: 16840
		// (get) Token: 0x0600CC5E RID: 52318 RVA: 0x002D8CDD File Offset: 0x002D6EDD
		// (set) Token: 0x0600CC5F RID: 52319 RVA: 0x002D8CE5 File Offset: 0x002D6EE5
		public bool EditSeries { get; set; }

		// Token: 0x170041C9 RID: 16841
		// (get) Token: 0x0600CC60 RID: 52320 RVA: 0x002D8CEE File Offset: 0x002D6EEE
		// (set) Token: 0x0600CC61 RID: 52321 RVA: 0x002D8CF6 File Offset: 0x002D6EF6
		public int ScrollTop { get; set; }

		// Token: 0x170041CA RID: 16842
		// (get) Token: 0x0600CC62 RID: 52322 RVA: 0x002D8CFF File Offset: 0x002D6EFF
		// (set) Token: 0x0600CC63 RID: 52323 RVA: 0x002D8D07 File Offset: 0x002D6F07
		public string TargetSlotIndex { get; set; }

		// Token: 0x170041CB RID: 16843
		// (get) Token: 0x0600CC64 RID: 52324 RVA: 0x002D8D10 File Offset: 0x002D6F10
		// (set) Token: 0x0600CC65 RID: 52325 RVA: 0x002D8D18 File Offset: 0x002D6F18
		public string LastSlotIndex { get; set; }

		// Token: 0x170041CC RID: 16844
		// (get) Token: 0x0600CC66 RID: 52326 RVA: 0x002D8D21 File Offset: 0x002D6F21
		// (set) Token: 0x0600CC67 RID: 52327 RVA: 0x002D8D29 File Offset: 0x002D6F29
		public string SourceSlotIndex { get; set; }

		// Token: 0x170041CD RID: 16845
		// (get) Token: 0x0600CC68 RID: 52328 RVA: 0x002D8D32 File Offset: 0x002D6F32
		// (set) Token: 0x0600CC69 RID: 52329 RVA: 0x002D8D3A File Offset: 0x002D6F3A
		public Appointment Appointment { get; set; }

		// Token: 0x170041CE RID: 16846
		// (get) Token: 0x0600CC6A RID: 52330 RVA: 0x002D8D43 File Offset: 0x002D6F43
		// (set) Token: 0x0600CC6B RID: 52331 RVA: 0x002D8D4B File Offset: 0x002D6F4B
		public int SlotWidth { get; set; }

		// Token: 0x170041CF RID: 16847
		// (get) Token: 0x0600CC6C RID: 52332 RVA: 0x002D8D54 File Offset: 0x002D6F54
		// (set) Token: 0x0600CC6D RID: 52333 RVA: 0x002D8D5C File Offset: 0x002D6F5C
		public int SlotHeight { get; set; }

		// Token: 0x170041D0 RID: 16848
		// (get) Token: 0x0600CC6E RID: 52334 RVA: 0x002D8D65 File Offset: 0x002D6F65
		// (set) Token: 0x0600CC6F RID: 52335 RVA: 0x002D8D6D File Offset: 0x002D6F6D
		public string MenuItemIndex { get; set; }

		// Token: 0x170041D1 RID: 16849
		// (get) Token: 0x0600CC70 RID: 52336 RVA: 0x002D8D76 File Offset: 0x002D6F76
		// (set) Token: 0x0600CC71 RID: 52337 RVA: 0x002D8D7E File Offset: 0x002D6F7E
		public string ContextMenuID { get; set; }

		// Token: 0x0600CC72 RID: 52338 RVA: 0x002D8D88 File Offset: 0x002D6F88
		public static SchedulerPostBackEvent DeserializeFromJSON(string json, RadScheduler scheduler)
		{
			json = json.Replace("/", "\\/");
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new AppointmentConverter(scheduler)
			});
			SchedulerPostBackEvent schedulerPostBackEvent = javaScriptSerializer.Deserialize<SchedulerPostBackEvent>(json);
			if (schedulerPostBackEvent.AppointmentID != null && !schedulerPostBackEvent.AppointmentID.Equals(-1))
			{
				schedulerPostBackEvent.AppointmentID = LosSerializer.Deserialize(schedulerPostBackEvent.AppointmentID.ToString());
			}
			if (schedulerPostBackEvent.StartDate != null)
			{
				schedulerPostBackEvent.StartDateParsed = scheduler.DisplayToUtc(SchedulerPostBackEvent.ParseJavaScriptTime(schedulerPostBackEvent.StartDate));
			}
			if (schedulerPostBackEvent.EndDate != null)
			{
				schedulerPostBackEvent.EndDateParsed = scheduler.DisplayToUtc(SchedulerPostBackEvent.ParseJavaScriptTime(schedulerPostBackEvent.EndDate));
			}
			if (schedulerPostBackEvent.Appointment != null)
			{
				if (schedulerPostBackEvent.Appointment.ID != null && !schedulerPostBackEvent.Appointment.ID.Equals(-1))
				{
					schedulerPostBackEvent.Appointment.ID = LosSerializer.Deserialize(schedulerPostBackEvent.Appointment.ID.ToString());
				}
				if (schedulerPostBackEvent.StartDate != null)
				{
					schedulerPostBackEvent.Appointment.Start = schedulerPostBackEvent.StartDateParsed;
				}
				if (schedulerPostBackEvent.EndDate != null)
				{
					schedulerPostBackEvent.Appointment.End = schedulerPostBackEvent.EndDateParsed;
				}
				if (schedulerPostBackEvent.EndDate != null)
				{
					schedulerPostBackEvent.Appointment.End = scheduler.DisplayToUtc(SchedulerPostBackEvent.ParseJavaScriptTime(schedulerPostBackEvent.EndDate));
				}
				if (schedulerPostBackEvent.Appointment.Subject != null)
				{
					schedulerPostBackEvent.Appointment.Subject = SchedulerPostBackEvent.DecodeClientString(schedulerPostBackEvent.Appointment.Subject);
				}
				if (schedulerPostBackEvent.Appointment.Description != null)
				{
					schedulerPostBackEvent.Appointment.Description = SchedulerPostBackEvent.DecodeClientString(schedulerPostBackEvent.Appointment.Description);
				}
				if (schedulerPostBackEvent.Appointment.TimeZoneID != null)
				{
					schedulerPostBackEvent.Appointment.TimeZoneID = schedulerPostBackEvent.Appointment.TimeZoneID;
				}
				if (schedulerPostBackEvent.Appointment.Resources.Count > 0)
				{
					for (int i = 0; i < schedulerPostBackEvent.Appointment.Resources.Count; i++)
					{
						schedulerPostBackEvent.Appointment.Resources[i].Text = SchedulerPostBackEvent.DecodeClientString(schedulerPostBackEvent.Appointment.Resources[i].Text);
					}
				}
			}
			return schedulerPostBackEvent;
		}

		// Token: 0x0600CC73 RID: 52339 RVA: 0x002D8FB4 File Offset: 0x002D71B4
		private static string DecodeClientString(string clientString)
		{
			return HttpUtility.UrlDecode(clientString).Replace("&squote", "'");
		}

		// Token: 0x0600CC74 RID: 52340 RVA: 0x002D8FCC File Offset: 0x002D71CC
		private static DateTime ParseJavaScriptTime(string jsTime)
		{
			return DateTime.ParseExact(jsTime, "yyyyMMddHHmm", null, DateTimeStyles.AssumeUniversal).ToUniversalTime();
		}
	}
}
