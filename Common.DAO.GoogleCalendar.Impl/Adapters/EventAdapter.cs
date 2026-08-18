using System;
using System.Text;
using Google.Apis.Calendar.v3.Data;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.Adapters
{
	// Token: 0x0200000D RID: 13
	public static class EventAdapter
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00004190 File Offset: 0x00002390
		public static string ToDisplayString(this Event app)
		{
			bool flag = app == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Summary: " + app.Summary);
				stringBuilder.AppendLine("Start: " + app.Start.DateTime.Value.ToString("F"));
				stringBuilder.AppendLine("End: " + app.End.DateTime.Value.ToString("F"));
				stringBuilder.AppendLine("Description: " + app.Description);
				stringBuilder.AppendLine("Updated: " + app.Updated.Value.ToString("F"));
				stringBuilder.AppendLine("Organizer.Name: " + app.Organizer.DisplayName);
				stringBuilder.AppendLine("Organizer.Email: " + app.Organizer.Email);
				bool flag2 = app.Attendees != null;
				if (flag2)
				{
					stringBuilder.AppendLine(string.Format("nAttendees: {0}", app.Attendees.Count));
					int num = 1;
					foreach (EventAttendee attendee in app.Attendees)
					{
						stringBuilder.AppendLine(string.Format(" ----- Begin Attendee{0} --------", num++));
						stringBuilder.AppendLine(attendee.ToDisplayString());
						stringBuilder.AppendLine(string.Format(" ----- End Attendee{0} --------", num));
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004378 File Offset: 0x00002578
		public static string ToDisplayString(this EventAttendee attendee)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Name: " + attendee.DisplayName);
			stringBuilder.AppendLine("Email: " + attendee.Email);
			stringBuilder.AppendLine(string.Format("Organizer: {0}", attendee.Organizer.Value));
			stringBuilder.AppendLine(string.Format("Optional: {0}", attendee.Optional.Value));
			stringBuilder.AppendLine("ResponseStatus: " + attendee.ResponseStatus);
			return stringBuilder.ToString();
		}
	}
}
