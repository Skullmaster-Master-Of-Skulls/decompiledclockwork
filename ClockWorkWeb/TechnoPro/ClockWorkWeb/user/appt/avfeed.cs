using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.StudentAppointmentBooking;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.UI.ClientManager.Web.Auth;

namespace TechnoPro.ClockWorkWeb.user.appt
{
	// Token: 0x020000EF RID: 239
	public class avfeed : IHttpHandler, IRequiresSessionState
	{
		// Token: 0x060006FD RID: 1789 RVA: 0x00035460 File Offset: 0x00033660
		public void ProcessRequest(HttpContext context)
		{
			string text = (context.Request.QueryString["showGraphical"] ?? "").Trim();
			bool flag2;
			bool flag = text.Length < 1 || !bool.TryParse(text, out flag2);
			if (flag)
			{
				flag2 = false;
			}
			bool flag3 = !flag2;
			if (flag3)
			{
				context.Response.ContentType = "text/json";
				context.Response.Write("[]");
				context.Response.End();
			}
			else
			{
				int studentPid = WebAuthenticationAuthorizationWebClientManager.CurrentInstance.GetStudentPid();
				HttpSessionState session = HttpContext.Current.Session;
				string s = context.Request.QueryString["start"];
				string s2 = context.Request.QueryString["end"];
				string text2 = context.Request.QueryString["optionalCalendarTitle"];
				string text3 = context.Request.QueryString["channelId"];
				DateTime minValue;
				bool flag4 = !DateTime.TryParse(s, out minValue);
				if (flag4)
				{
					minValue = DateTime.MinValue;
				}
				DateTime minValue2;
				bool flag5 = !DateTime.TryParse(s2, out minValue2);
				if (flag5)
				{
					minValue2 = DateTime.MinValue;
				}
				bool flag6 = minValue == DateTime.MinValue || minValue2 == DateTime.MinValue;
				if (flag6)
				{
					throw new ArgumentException("Invalid start/end");
				}
				bool flag7 = text3.Length < 1;
				if (flag7)
				{
					throw new ArgumentException("Invalid channelId");
				}
				int num = Convert.ToInt32((minValue2 - minValue).TotalDays);
				bool flag8 = num < 1;
				if (flag8)
				{
					num = 1;
				}
				bool flag9 = num > 1000;
				if (flag9)
				{
					num = 1000;
				}
				session.Add("AppointmentBookingCalendarContext_ChannelId", text3 ?? "");
				session.Add("AppointmentBookingCalendarContext_Date", minValue.ToString("yyyy-MM-dd"));
				session.Add("AppointmentBookingCalendarContext_WithWhom", text2 ?? "");
				IAppointmentBookingStudentClientManager appointmentBookingStudentClientManager = new AppointmentBookingStudentClientManager();
				IList<ChannelCalendarWithAvailabilityDTO> list = appointmentBookingStudentClientManager.LoadAvailabilityForChannel(studentPid, text3, text2, minValue, num);
				List<avfeed.Event> list2 = new List<avfeed.Event>();
				foreach (ChannelCalendarWithAvailabilityDTO channelCalendarWithAvailabilityDTO in list)
				{
					foreach (AvailabilityForChannelCalendarDTO availabilityForChannelCalendarDTO in channelCalendarWithAvailabilityDTO.Availabilities)
					{
						list2.Add(new avfeed.Event(Guid.NewGuid().ToString(), availabilityForChannelCalendarDTO.AvailabilityTitle ?? "", this.FixDate(availabilityForChannelCalendarDTO.StartDateTime), this.FixDate(availabilityForChannelCalendarDTO.EndDateTime), channelCalendarWithAvailabilityDTO.CalendarTitle ?? "", text3, channelCalendarWithAvailabilityDTO.CalendarTitle ?? "", availabilityForChannelCalendarDTO.AvailabilityGroupId));
					}
				}
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				string s3 = javaScriptSerializer.Serialize(list2);
				context.Response.ContentType = "text/json";
				context.Response.Write(s3);
				context.Response.End();
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000357B8 File Offset: 0x000339B8
		private string FixDate(DateTime value)
		{
			return value.ToString("yyyy-MM-dd HH:mm");
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000357D8 File Offset: 0x000339D8
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0200021C RID: 540
		public class Event
		{
			// Token: 0x06000E2B RID: 3627 RVA: 0x0000AF9E File Offset: 0x0000919E
			public Event()
			{
			}

			// Token: 0x06000E2C RID: 3628 RVA: 0x00050554 File Offset: 0x0004E754
			public Event(string id, string title, string start, string end, string resourceId, string channelId, string calendarTitle, int availabilityGroupId)
			{
				this.id = id;
				this.title = title;
				this.start = start;
				this.end = end;
				this.resourceId = resourceId;
				this.extendedProps = new avfeed.ExtendedEventInfo
				{
					channelId = channelId,
					availabilityGroupId = availabilityGroupId,
					calendarTitle = calendarTitle
				};
			}

			// Token: 0x17000325 RID: 805
			// (get) Token: 0x06000E2D RID: 3629 RVA: 0x000505BA File Offset: 0x0004E7BA
			// (set) Token: 0x06000E2E RID: 3630 RVA: 0x000505C2 File Offset: 0x0004E7C2
			public string id { get; set; }

			// Token: 0x17000326 RID: 806
			// (get) Token: 0x06000E2F RID: 3631 RVA: 0x000505CB File Offset: 0x0004E7CB
			// (set) Token: 0x06000E30 RID: 3632 RVA: 0x000505D3 File Offset: 0x0004E7D3
			public string title { get; set; }

			// Token: 0x17000327 RID: 807
			// (get) Token: 0x06000E31 RID: 3633 RVA: 0x000505DC File Offset: 0x0004E7DC
			// (set) Token: 0x06000E32 RID: 3634 RVA: 0x000505E4 File Offset: 0x0004E7E4
			public string start { get; set; }

			// Token: 0x17000328 RID: 808
			// (get) Token: 0x06000E33 RID: 3635 RVA: 0x000505ED File Offset: 0x0004E7ED
			// (set) Token: 0x06000E34 RID: 3636 RVA: 0x000505F5 File Offset: 0x0004E7F5
			public string end { get; set; }

			// Token: 0x17000329 RID: 809
			// (get) Token: 0x06000E35 RID: 3637 RVA: 0x000505FE File Offset: 0x0004E7FE
			// (set) Token: 0x06000E36 RID: 3638 RVA: 0x00050606 File Offset: 0x0004E806
			public string resourceId { get; set; }

			// Token: 0x1700032A RID: 810
			// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0005060F File Offset: 0x0004E80F
			// (set) Token: 0x06000E38 RID: 3640 RVA: 0x00050617 File Offset: 0x0004E817
			public avfeed.ExtendedEventInfo extendedProps { get; set; }
		}

		// Token: 0x0200021D RID: 541
		public class ExtendedEventInfo
		{
			// Token: 0x1700032B RID: 811
			// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00050620 File Offset: 0x0004E820
			// (set) Token: 0x06000E3A RID: 3642 RVA: 0x00050628 File Offset: 0x0004E828
			public string channelId { get; set; }

			// Token: 0x1700032C RID: 812
			// (get) Token: 0x06000E3B RID: 3643 RVA: 0x00050631 File Offset: 0x0004E831
			// (set) Token: 0x06000E3C RID: 3644 RVA: 0x00050639 File Offset: 0x0004E839
			public string calendarTitle { get; set; }

			// Token: 0x1700032D RID: 813
			// (get) Token: 0x06000E3D RID: 3645 RVA: 0x00050642 File Offset: 0x0004E842
			// (set) Token: 0x06000E3E RID: 3646 RVA: 0x0005064A File Offset: 0x0004E84A
			public int availabilityGroupId { get; set; }
		}
	}
}
