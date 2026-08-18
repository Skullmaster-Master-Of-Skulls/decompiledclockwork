using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace EmailClassLibrary
{
	// Token: 0x0200000C RID: 12
	public class MeetingRequest
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002F48 File Offset: 0x00001F48
		public static void EmailAppointment(int ClockWorkAppId, DateTime StartDate, DateTime EndDate, string Subject, string Summary, string Location, string AttendeeName, string AttendeeEmail, string OrganizerName, string OrganizerEmail, string smtp_host, int smtp_port, string smtp_uname, string smtp_pass)
		{
			MailMessage mailMessage = new MailMessage();
			SmtpClient smtpClient = new SmtpClient(smtp_host, smtp_port);
			if (smtp_uname != null)
			{
				smtpClient.Credentials = new NetworkCredential(smtp_uname, smtp_pass);
			}
			ContentType contentType = new ContentType("text/plain");
			ContentType contentType2 = new ContentType("text/html");
			ContentType contentType3 = new ContentType("text/calendar");
			contentType3.Parameters.Add("method", "REQUEST");
			contentType3.Parameters.Add("name", "meeting.ics");
			AlternateView item = AlternateView.CreateAlternateViewFromString(MeetingRequest.BodyText(ClockWorkAppId, StartDate, EndDate, Subject, Summary, Location, AttendeeName, AttendeeEmail, OrganizerName, OrganizerEmail), contentType);
			mailMessage.AlternateViews.Add(item);
			AlternateView item2 = AlternateView.CreateAlternateViewFromString(MeetingRequest.BodyHTML(ClockWorkAppId, StartDate, EndDate, Subject, Summary, Location, AttendeeName, AttendeeEmail, OrganizerName, OrganizerEmail), contentType2);
			mailMessage.AlternateViews.Add(item2);
			AlternateView alternateView = AlternateView.CreateAlternateViewFromString(MeetingRequest.VCalendar(ClockWorkAppId, StartDate, EndDate, Subject, Summary, Location, AttendeeName, AttendeeEmail, OrganizerName, OrganizerEmail), contentType3);
			alternateView.TransferEncoding = TransferEncoding.SevenBit;
			mailMessage.AlternateViews.Add(alternateView);
			mailMessage.From = new MailAddress(OrganizerEmail);
			mailMessage.To.Add(new MailAddress(AttendeeEmail));
			mailMessage.Subject = Subject;
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.Send(mailMessage);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003088 File Offset: 0x00002088
		public static void SaveTempVcsFile(string tempFilename, int ClockWorkAppId, DateTime StartDate, DateTime EndDate, string AppType, string Memo, string Location)
		{
			string str = Memo.Replace("\n", "=0D=0A");
			string str2 = AppType.Replace("\n", "=0D=0A");
			string str3 = Location.Replace("\n", "=0D=0A");
			TextWriter textWriter = new StreamWriter(tempFilename, false);
			textWriter.WriteLine("BEGIN:VCALENDAR");
			textWriter.WriteLine("PRODID:-//TechnoPro Inc.//tpro//EN");
			textWriter.WriteLine("BEGIN:VEVENT");
			textWriter.WriteLine("DTSTART:" + StartDate.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z"));
			textWriter.WriteLine("DTEND:" + EndDate.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z"));
			textWriter.WriteLine("LOCATION:" + str3);
			textWriter.WriteLine("DESCRIPTION;ENCODING=QUOTED-PRINTABLE:" + str);
			textWriter.WriteLine("SUMMARY:" + str2);
			textWriter.WriteLine("PRIORITY:3");
			textWriter.WriteLine("END:VEVENT");
			textWriter.WriteLine("END:VCALENDAR");
			textWriter.Close();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003198 File Offset: 0x00002198
		public static void SaveTempIcsFile(string tempFilename, int ClockWorkAppId, DateTime StartDate, DateTime EndDate, string appType, string memo, string Location)
		{
			string format = "yyyyMMddTHHmmssZ";
			TextWriter textWriter = new StreamWriter(tempFilename, false);
			textWriter.WriteLine("BEGIN:VCALENDAR");
			textWriter.WriteLine("VERSION:2.0");
			textWriter.WriteLine("PRODID:-//AkonaDev/CalendarAppointment");
			textWriter.WriteLine("CALSCALE:GREGORIAN");
			textWriter.WriteLine("BEGIN:VEVENT");
			textWriter.WriteLine("BEGIN:VTIMEZONE");
			textWriter.WriteLine("TZID:US/Eastern");
			textWriter.WriteLine("BEGIN:STANDARD");
			textWriter.WriteLine("DTSTART:19970714T170000Z");
			textWriter.WriteLine("RRULE:FREQ=YEARLY;BYDAY=1SU;BYMONTH=11");
			textWriter.WriteLine("TZOFFSETFROM:-0400");
			textWriter.WriteLine("TZOFFSETTO:-0500");
			textWriter.WriteLine("TZNAME:EST");
			textWriter.WriteLine("END:STANDARD");
			textWriter.WriteLine("BEGIN:DAYLIGHT");
			textWriter.WriteLine("DTSTART:20070311T020000");
			textWriter.WriteLine("RRULE:FREQ=YEARLY;BYDAY=2SU;BYMONTH=3");
			textWriter.WriteLine("TZOFFSETFROM:-0500");
			textWriter.WriteLine("TZOFFSETTO:-0400");
			textWriter.WriteLine("TZNAME:EDT");
			textWriter.WriteLine("END:DAYLIGHT");
			textWriter.WriteLine("END:VTIMEZONE");
			textWriter.Write("DTSTART;TZID=US/Eastern:");
			textWriter.WriteLine(StartDate.ToUniversalTime().ToString(format));
			textWriter.Write("DTEND;TZID=US/Eastern:");
			textWriter.WriteLine(EndDate.ToUniversalTime().ToString(format));
			textWriter.WriteLine("SUMMARY:" + appType);
			textWriter.WriteLine("DESCRIPTION:memo");
			textWriter.WriteLine("UID:1");
			textWriter.WriteLine("SEQUENCE:0");
			textWriter.WriteLine("METHOD:PUBLISH");
			textWriter.WriteLine("DTSTAMP:" + DateTime.Now.ToUniversalTime().ToString(format));
			textWriter.WriteLine("END:VEVENT");
			textWriter.WriteLine("END:VCALENDAR");
			textWriter.Close();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003367 File Offset: 0x00002367
		private static string FormatDateTimeValue(short DateValue)
		{
			if (DateValue < 10)
			{
				return "0" + DateValue.ToString();
			}
			return DateValue.ToString();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003388 File Offset: 0x00002388
		public static string BodyText(int ClockWorkAppId, DateTime StartDate, DateTime EndDate, string Subject, string Summary, string Location, string AttendeeName, string AttendeeEmail, string OrganizerName, string OrganizerEmail)
		{
			string format = string.Concat(new string[]
			{
				"Type:Single Meeting",
				Environment.NewLine,
				"Organizer: {0}",
				Environment.NewLine,
				"Start Time:{1}",
				Environment.NewLine,
				"End Time:{2}",
				Environment.NewLine,
				"Time Zone:{3}",
				Environment.NewLine,
				"Location: {4}",
				Environment.NewLine,
				Environment.NewLine,
				"*~*~*~*~*~*~*~*~*~*",
				Environment.NewLine,
				Environment.NewLine,
				"{5}"
			});
			return string.Format(format, new object[]
			{
				OrganizerName,
				StartDate.ToLongDateString() + " " + StartDate.ToLongTimeString(),
				EndDate.ToLongDateString() + " " + EndDate.ToLongTimeString(),
				TimeZone.CurrentTimeZone.StandardName,
				Location,
				Summary
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003490 File Offset: 0x00002490
		public static string BodyHTML(int ClockWorkAppId, DateTime StartDate, DateTime EndDate, string Subject, string Summary, string Location, string AttendeeName, string AttendeeEmail, string OrganizerName, string OrganizerEmail)
		{
			string newLine = Environment.NewLine;
			string format = string.Concat(new string[]
			{
				"<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 3.2//EN\">",
				newLine,
				"<HTML>",
				newLine,
				"<HEAD>",
				newLine,
				"<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=utf-8\">",
				newLine,
				"<META NAME=\"Generator\" CONTENT=\"MS Exchange Server version 6.5.7652.24\">",
				newLine,
				"<TITLE>{0}</TITLE>",
				newLine,
				"</HEAD>",
				newLine,
				"<BODY>",
				newLine,
				"<!-- Converted from text/plain format -->",
				newLine,
				"<P><FONT SIZE=2>Type:Single Meeting<BR>",
				newLine,
				"Organizer:{1}<BR>",
				newLine,
				"Start Time:{2}<BR>",
				newLine,
				"End Time:{3}<BR>",
				newLine,
				"Time Zone:{4}<BR>",
				newLine,
				"Location:{5}<BR>",
				newLine,
				"<BR>",
				newLine,
				"*~*~*~*~*~*~*~*~*~*<BR>",
				newLine,
				"<BR>",
				newLine,
				"{6}<BR>",
				newLine,
				"</FONT>",
				newLine,
				"</P>",
				newLine,
				newLine,
				"</BODY>",
				newLine,
				"</HTML>"
			});
			return string.Format(format, new object[]
			{
				Summary,
				OrganizerName,
				StartDate.ToLongDateString() + " " + StartDate.ToLongTimeString(),
				EndDate.ToLongDateString() + " " + EndDate.ToLongTimeString(),
				TimeZone.CurrentTimeZone.StandardName,
				Location,
				Summary
			});
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000364C File Offset: 0x0000264C
		public static string VCalendar(int ClockWorkAppId, DateTime StartDate, DateTime EndDate, string Subject, string Summary, string Location, string AttendeeName, string AttendeeEmail, string OrganizerName, string OrganizerEmail)
		{
			string format = string.Concat(new string[]
			{
				"BEGIN:VCALENDAR",
				Environment.NewLine,
				"METHOD:REQUEST",
				Environment.NewLine,
				"PRODID:Microsoft CDO for Microsoft Exchange",
				Environment.NewLine,
				"VERSION:2.0",
				Environment.NewLine,
				"BEGIN:VTIMEZONE",
				Environment.NewLine,
				"TZID:(GMT-06.00) Central Time (US & Canada)",
				Environment.NewLine,
				"X-MICROSOFT-CDO-TZID:11",
				Environment.NewLine,
				"BEGIN:STANDARD",
				Environment.NewLine,
				"DTSTART:16010101T020000",
				Environment.NewLine,
				"TZOFFSETFROM:-0500",
				Environment.NewLine,
				"TZOFFSETTO:-0600",
				Environment.NewLine,
				"RRULE:FREQ=YEARLY;WKST=MO;INTERVAL=1;BYMONTH=11;BYDAY=1SU",
				Environment.NewLine,
				"END:STANDARD",
				Environment.NewLine,
				"BEGIN:DAYLIGHT",
				Environment.NewLine,
				"DTSTART:16010101T020000",
				Environment.NewLine,
				"TZOFFSETFROM:-0600",
				Environment.NewLine,
				"TZOFFSETTO:-0500",
				Environment.NewLine,
				"RRULE:FREQ=YEARLY;WKST=MO;INTERVAL=1;BYMONTH=3;BYDAY=2SU",
				Environment.NewLine,
				"END:DAYLIGHT",
				Environment.NewLine,
				"END:VTIMEZONE",
				Environment.NewLine,
				"BEGIN:VEVENT",
				Environment.NewLine,
				"DTSTAMP:{8}",
				Environment.NewLine,
				"DTSTART:{0}",
				Environment.NewLine,
				"SUMMARY:{7}",
				Environment.NewLine,
				"UID:{5}",
				Environment.NewLine,
				"ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN=\"{9}\":MAILTO:{9}",
				Environment.NewLine,
				"ACTION;RSVP=TRUE;CN=\"{4}\":MAILTO:{4}",
				Environment.NewLine,
				"ORGANIZER;CN=\"{3}\":mailto:{4}",
				Environment.NewLine,
				"LOCATION:{2}",
				Environment.NewLine,
				"DTEND:{1}",
				Environment.NewLine,
				"DESCRIPTION:{7}\\n",
				Environment.NewLine,
				"SEQUENCE:1",
				Environment.NewLine,
				"PRIORITY:5",
				Environment.NewLine,
				"CLASS:",
				Environment.NewLine,
				"CREATED:{8}",
				Environment.NewLine,
				"LAST-MODIFIED:{8}",
				Environment.NewLine,
				"STATUS:CONFIRMED",
				Environment.NewLine,
				"TRANSP:OPAQUE",
				Environment.NewLine,
				"X-MICROSOFT-CDO-BUSYSTATUS:BUSY",
				Environment.NewLine,
				"X-MICROSOFT-CDO-INSTTYPE:0",
				Environment.NewLine,
				"X-MICROSOFT-CDO-INTENDEDSTATUS:BUSY",
				Environment.NewLine,
				"X-MICROSOFT-CDO-ALLDAYEVENT:FALSE",
				Environment.NewLine,
				"X-MICROSOFT-CDO-IMPORTANCE:1",
				Environment.NewLine,
				"X-MICROSOFT-CDO-OWNERAPPTID:-1",
				Environment.NewLine,
				"X-MICROSOFT-CDO-ATTENDEE-CRITICAL-CHANGE:{8}",
				Environment.NewLine,
				"X-MICROSOFT-CDO-OWNER-CRITICAL-CHANGE:{8}",
				Environment.NewLine,
				"BEGIN:VALARM",
				Environment.NewLine,
				"ACTION:DISPLAY",
				Environment.NewLine,
				"DESCRIPTION:REMINDER",
				Environment.NewLine,
				"TRIGGER;RELATED=START:-PT00H15M00S",
				Environment.NewLine,
				"END:VALARM",
				Environment.NewLine,
				"END:VEVENT",
				Environment.NewLine,
				"END:VCALENDAR",
				Environment.NewLine
			});
			return string.Format(format, new object[]
			{
				StartDate.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"),
				EndDate.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"),
				Location,
				OrganizerName,
				OrganizerEmail,
				"{" + ClockWorkAppId.ToString() + "}",
				Summary,
				Subject,
				DateTime.Now.ToUniversalTime().ToString("yyyyMMddTHHmmssZ"),
				AttendeeEmail
			});
		}
	}
}
