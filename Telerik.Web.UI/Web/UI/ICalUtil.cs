using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI.Scheduler.TimeZones;

namespace Telerik.Web.UI
{
	// Token: 0x020012D8 RID: 4824
	internal static class ICalUtil
	{
		// Token: 0x0600CA78 RID: 51832 RVA: 0x002D2BDF File Offset: 0x002D0DDF
		public static string Export(Appointment app, bool outlookCompatibleMode, TimeSpan timeZoneOffset)
		{
			return ICalUtil.ExportAppointmentInternal(app, outlookCompatibleMode, timeZoneOffset, false);
		}

		// Token: 0x0600CA79 RID: 51833 RVA: 0x002D2BEA File Offset: 0x002D0DEA
		public static string Export(AppointmentCollection appointments, bool outlookCompatibleMode, TimeSpan timeZoneOffset)
		{
			return ICalUtil.ExportAppointmentsInternal(appointments, outlookCompatibleMode, timeZoneOffset, false);
		}

		// Token: 0x0600CA7A RID: 51834 RVA: 0x002D2BF5 File Offset: 0x002D0DF5
		public static string ExportWithTimeZones(AppointmentCollection appointments, bool outlookCompatibleMode, bool hasTimeZones)
		{
			return ICalUtil.ExportAppointmentsInternal(appointments, outlookCompatibleMode, TimeSpan.Zero, hasTimeZones);
		}

		// Token: 0x0600CA7B RID: 51835 RVA: 0x002D2C04 File Offset: 0x002D0E04
		public static string ExportWithTimeZones(Appointment appointment, bool outlookCompatibleMode, bool hasTimeZones)
		{
			return ICalUtil.ExportAppointmentInternal(appointment, outlookCompatibleMode, TimeSpan.Zero, hasTimeZones);
		}

		// Token: 0x0600CA7C RID: 51836 RVA: 0x002D2C14 File Offset: 0x002D0E14
		private static string ExportAppointmentInternal(Appointment app, bool outlookCompatibleMode, TimeSpan timeZoneOffset, bool hasTimeZones)
		{
			AppointmentCollection appointmentCollection = new AppointmentCollection();
			if (app.RecurrenceState == RecurrenceState.Occurrence && app.RecurrenceParentID != null && app.Owner != null)
			{
				appointmentCollection.Add(app.Owner.Appointments.FindByID(app.RecurrenceParentID));
			}
			else
			{
				appointmentCollection.Add(app);
			}
			return ICalUtil.ExportAppointmentsInternal(appointmentCollection, outlookCompatibleMode, timeZoneOffset, hasTimeZones);
		}

		// Token: 0x0600CA7D RID: 51837 RVA: 0x002D2C70 File Offset: 0x002D0E70
		private static string ExportAppointmentsInternal(AppointmentCollection appointments, bool outlookCompatibleMode, TimeSpan timeZoneOffset, bool hasTimeZones)
		{
			StringBuilder stringBuilder = new StringBuilder();
			ICalUtil.WriteFileHeader(stringBuilder, outlookCompatibleMode);
			if (hasTimeZones)
			{
				string[] array = ICalUtil.GatherAllTimeZones(appointments);
				foreach (string text in array)
				{
					ICalUtil.WriteTimeZoneHeader(stringBuilder);
					ICalUtil.WriteTimeZoneInfo(string.IsNullOrEmpty(text) ? TimeZoneInfo.Utc.Id : text, stringBuilder);
					ICalUtil.WriteTimeZoneFooter(stringBuilder);
				}
			}
			foreach (Appointment appointment in appointments)
			{
				if (appointment.RecurrenceState != RecurrenceState.Occurrence)
				{
					if (outlookCompatibleMode)
					{
						ICalUtil.ValidateOutlookCompatibility(appointment);
					}
					ICalUtil.WriteTask(stringBuilder, appointment, outlookCompatibleMode, timeZoneOffset, hasTimeZones);
				}
			}
			ICalUtil.WriteFileFooter(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0600CA7E RID: 51838 RVA: 0x002D2D38 File Offset: 0x002D0F38
		private static void WriteTimeZoneInfo(string timeZoneId, StringBuilder output)
		{
			TimeZoneInfo timeZoneInfoById = ICalUtil.provider.GetTimeZoneInfoById(timeZoneId);
			output.AppendLine("TZID:" + timeZoneInfoById.Id);
			output.AppendLine("BEGIN:STANDARD");
			if (timeZoneInfoById.SupportsDaylightSavingTime)
			{
				ICalUtil.AppendStartTime(output, timeZoneInfoById, "STANDARD");
				ICalUtil.AppendRecurrenceRule(output, timeZoneInfoById, "STANDARD");
			}
			else
			{
				output.AppendLine("DTSTART:" + ICalUtil.FormatUtcDate(new DateTime(1601, 1, 1)));
			}
			ICalUtil.AppendTimeZoneOffsets(output, timeZoneInfoById, "STANDARD");
			output.AppendLine("END:STANDARD");
			if (timeZoneInfoById.SupportsDaylightSavingTime)
			{
				output.AppendLine("BEGIN:DAYLIGHT");
				ICalUtil.AppendStartTime(output, timeZoneInfoById, "DAYLIGHT");
				ICalUtil.AppendRecurrenceRule(output, timeZoneInfoById, "DAYLIGHT");
				ICalUtil.AppendTimeZoneOffsets(output, timeZoneInfoById, "DAYLIGHT");
				output.AppendLine("END:DAYLIGHT");
			}
		}

		// Token: 0x0600CA7F RID: 51839 RVA: 0x002D2E14 File Offset: 0x002D1014
		private static void AppendStartTime(StringBuilder output, TimeZoneInfo timeZoneInfo, string mode)
		{
			TimeZoneInfoModel timeZoneInfoModel = new TimeZoneInfoModel(timeZoneInfo);
			TimeZoneInfo.AdjustmentRule rule = timeZoneInfo.GetAdjustmentRules().Last<TimeZoneInfo.AdjustmentRule>();
			DateTime date = DateTime.Now;
			if (mode != null)
			{
				if (!(mode == "STANDARD"))
				{
					if (mode == "DAYLIGHT")
					{
						date = timeZoneInfoModel.GetTransitionStart(rule);
					}
				}
				else
				{
					date = timeZoneInfoModel.GetTransitionEnd(rule);
				}
			}
			output.AppendLine("DTSTART:" + ICalUtil.FormatUtcDate(date));
		}

		// Token: 0x0600CA80 RID: 51840 RVA: 0x002D2E84 File Offset: 0x002D1084
		private static void AppendTimeZoneOffsets(StringBuilder output, TimeZoneInfo timeZoneInfo, string mode)
		{
			TimeSpan ts = TimeSpan.Zero;
			if (timeZoneInfo.GetAdjustmentRules().Length > 0)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = timeZoneInfo.GetAdjustmentRules().Last<TimeZoneInfo.AdjustmentRule>();
				ts = adjustmentRule.DaylightDelta;
			}
			TimeSpan baseUtcOffset = timeZoneInfo.BaseUtcOffset;
			TimeSpan offset = TimeSpan.Zero;
			TimeSpan offset2 = TimeSpan.Zero;
			if (mode != null)
			{
				if (!(mode == "STANDARD"))
				{
					if (mode == "DAYLIGHT")
					{
						offset = baseUtcOffset;
						offset2 = baseUtcOffset.Add(ts);
					}
				}
				else
				{
					offset = baseUtcOffset.Add(ts);
					offset2 = baseUtcOffset;
				}
			}
			output.AppendLine("TZOFFSETFROM:" + ICalUtil.FormatOffset(offset));
			output.AppendLine("TZOFFSETTO:" + ICalUtil.FormatOffset(offset2));
		}

		// Token: 0x0600CA81 RID: 51841 RVA: 0x002D2F38 File Offset: 0x002D1138
		private static string FormatOffset(TimeSpan offset)
		{
			string str = (offset.Ticks < 0L) ? "-" : "+";
			offset = ((offset.Ticks < 0L) ? offset.Negate() : offset);
			return str + default(DateTime).Add(offset).ToString("hhmm");
		}

		// Token: 0x0600CA82 RID: 51842 RVA: 0x002D2F9C File Offset: 0x002D119C
		private static void AppendRecurrenceRule(StringBuilder output, TimeZoneInfo timeZoneInfo, string mode)
		{
			if (mode != null)
			{
				if (mode == "STANDARD")
				{
					TimeZoneInfo.TransitionTime daylightTransitionEnd = timeZoneInfo.GetAdjustmentRules().Last<TimeZoneInfo.AdjustmentRule>().DaylightTransitionEnd;
					int num = (daylightTransitionEnd.Week > 4) ? -1 : daylightTransitionEnd.Week;
					string text = daylightTransitionEnd.DayOfWeek.ToString().Substring(0, 2).ToUpperInvariant();
					int month = daylightTransitionEnd.Month;
					output.AppendLine(string.Format("RRULE:FREQ=YEARLY;INTERVAL={0};BYSETPOS={1};BYDAY={2};BYMONTH={3}", new object[]
					{
						1,
						num,
						text,
						month
					}));
					return;
				}
				if (!(mode == "DAYLIGHT"))
				{
					return;
				}
				TimeZoneInfo.TransitionTime daylightTransitionStart = timeZoneInfo.GetAdjustmentRules().Last<TimeZoneInfo.AdjustmentRule>().DaylightTransitionStart;
				int num2 = (daylightTransitionStart.Week > 4) ? -1 : daylightTransitionStart.Week;
				string text2 = daylightTransitionStart.DayOfWeek.ToString().Substring(0, 2).ToUpperInvariant();
				int month2 = daylightTransitionStart.Month;
				output.AppendLine(string.Format("RRULE:FREQ=YEARLY;INTERVAL={0};BYSETPOS={1};BYDAY={2};BYMONTH={3}", new object[]
				{
					1,
					num2,
					text2,
					month2
				}));
			}
		}

		// Token: 0x0600CA83 RID: 51843 RVA: 0x002D30F0 File Offset: 0x002D12F0
		private static string[] GatherAllTimeZones(AppointmentCollection appointments)
		{
			List<string> list = new List<string>(4);
			foreach (Appointment appointment in appointments)
			{
				if (!list.Contains(appointment.TimeZoneID))
				{
					list.Add(appointment.TimeZoneID);
					ICalUtil.InitProvider(appointment.TimeZoneID);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600CA84 RID: 51844 RVA: 0x002D3164 File Offset: 0x002D1364
		private static void InitProvider(string timeZoneId)
		{
			if (!ICalUtil.providers.ContainsKey(timeZoneId))
			{
				ICalUtil.providers.Add(timeZoneId, new TimeZoneInfoProvider(timeZoneId));
			}
		}

		// Token: 0x0600CA85 RID: 51845 RVA: 0x002D3184 File Offset: 0x002D1384
		private static void WriteTimeZoneHeader(StringBuilder output)
		{
			output.AppendLine("BEGIN:VTIMEZONE");
		}

		// Token: 0x0600CA86 RID: 51846 RVA: 0x002D3192 File Offset: 0x002D1392
		private static void WriteTimeZoneFooter(StringBuilder output)
		{
			output.AppendLine("END:VTIMEZONE");
		}

		// Token: 0x0600CA87 RID: 51847 RVA: 0x002D31A0 File Offset: 0x002D13A0
		private static void ValidateOutlookCompatibility(Appointment app)
		{
			if (!string.IsNullOrEmpty(app.RecurrenceRule))
			{
				RecurrenceRule recurrenceRule;
				if (!RecurrenceRule.TryParse(app.RecurrenceRule, out recurrenceRule))
				{
					throw new InvalidOperationException("Invalid recurrence rule.");
				}
				if (recurrenceRule.Pattern.Frequency == RecurrenceFrequency.Hourly)
				{
					throw new InvalidOperationException("Cannot export appointments with hourly recurrence in Outlook compatible mode.");
				}
				if (recurrenceRule.Pattern.Frequency == RecurrenceFrequency.Daily && recurrenceRule.Pattern.DaysOfWeekMask != RecurrenceDay.EveryDay && recurrenceRule.Pattern.DaysOfWeekMask != RecurrenceDay.WeekDays)
				{
					throw new InvalidOperationException("Cannot export appointments with daily recurrence and custom DaysOfWeekMask in Outlook compatible mode.");
				}
			}
		}

		// Token: 0x0600CA88 RID: 51848 RVA: 0x002D3224 File Offset: 0x002D1424
		private static void WriteTask(StringBuilder output, Appointment app, bool outlookCompatibleMode, TimeSpan timeZoneOffset, bool hasTimeZones)
		{
			output.AppendLine("BEGIN:VEVENT");
			if (!string.IsNullOrEmpty(app.RecurrenceRule))
			{
				RecurrenceRule recurrenceRule;
				if (!RecurrenceRule.TryParse(app.RecurrenceRule, out recurrenceRule))
				{
					throw new InvalidOperationException("Invalid recurrence rule.");
				}
				if (outlookCompatibleMode)
				{
					using (IEnumerator<DateTime> enumerator = recurrenceRule.Occurrences.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							DateTime start = enumerator.Current;
							recurrenceRule.Range.Start = start;
						}
					}
					if (recurrenceRule.HasExceptions && recurrenceRule.Exceptions[0] < recurrenceRule.Range.Start)
					{
						recurrenceRule.Range.Start = recurrenceRule.Exceptions[0];
					}
					if ((recurrenceRule.Pattern.Frequency == RecurrenceFrequency.Daily || recurrenceRule.Pattern.Frequency == RecurrenceFrequency.Monthly || recurrenceRule.Pattern.Frequency == RecurrenceFrequency.Yearly) && recurrenceRule.Pattern.DaysOfWeekMask == RecurrenceDay.EveryDay && !recurrenceRule.ToString().Contains("BYSETPOS"))
					{
						recurrenceRule.Pattern.DaysOfWeekMask = RecurrenceDay.None;
					}
				}
				if (!hasTimeZones)
				{
					ICalUtil.ConvertRecurrenceRuleToUtc(recurrenceRule, timeZoneOffset);
				}
				string value = hasTimeZones ? recurrenceRule.ToString(ICalUtil.providers[app.TimeZoneID]) : recurrenceRule.ToString();
				output.Append(value);
			}
			else
			{
				DateTime date = hasTimeZones ? ICalUtil.UtcToClient(app.Start, app.TimeZoneID) : ICalUtil.ClientToUtc(app.Start, timeZoneOffset);
				DateTime date2 = hasTimeZones ? ICalUtil.UtcToClient(app.End, app.TimeZoneID) : ICalUtil.ClientToUtc(app.End, timeZoneOffset);
				string text = "{0}{1}{2}";
				text = string.Format(text, "DTSTART", hasTimeZones ? ";TZID=" : ":", hasTimeZones ? ("\"" + ICalUtil.providers[app.TimeZoneID].OperationTimeZone.TimeZoneId + "\":") : "");
				string text2 = "{0}{1}{2}";
				text2 = string.Format(text2, "DTEND", hasTimeZones ? ";TZID=" : ":", hasTimeZones ? ("\"" + ICalUtil.providers[app.TimeZoneID].OperationTimeZone.TimeZoneId + "\":") : "");
				if (date.Hour == 0 && date2.Hour == 0 && app.Duration.TotalHours >= 24.0)
				{
					output.AppendFormat("{0}{1}\r\n", text, ICalUtil.FormatAllDayDate(date));
					output.AppendFormat("{0}{1}\r\n", text2, ICalUtil.FormatAllDayDate(date2));
				}
				else
				{
					output.AppendFormat("{0}{1}\r\n", text, hasTimeZones ? ICalUtil.FormatDate(date) : ICalUtil.FormatUtcDate(date));
					output.AppendFormat("{0}{1}\r\n", text2, hasTimeZones ? ICalUtil.FormatDate(date2) : ICalUtil.FormatUtcDate(date2));
				}
			}
			if (outlookCompatibleMode)
			{
				output.AppendFormat("UID:{0}-{1}\r\n", ICalUtil.FormatUtcDate(DateTime.Now.ToUniversalTime()), app.ID);
				output.AppendFormat("DTSTAMP:{0}\r\n", ICalUtil.FormatUtcDate(DateTime.Now.ToUniversalTime()));
			}
			if (app.Attributes["Location"] != null)
			{
				output.AppendFormat("LOCATION:{0}\r\n", ICalUtil.NormalizeNewLines(app.Attributes["Location"]));
			}
			output.AppendFormat("SUMMARY:{0}\r\n", ICalUtil.NormalizeNewLines(app.Subject));
			output.AppendFormat("DESCRIPTION:{0}\r\n", ICalUtil.NormalizeNewLines(app.Description));
			if (app.Reminders.Count > 0)
			{
				output.Append("BEGIN:VALARM\r\n");
				output.AppendFormat("TRIGGER:-PT{0}M\r\n", app.Reminders[0].Trigger.TotalMinutes.ToString());
				output.Append("ACTION:DISPLAY\r\n");
				output.Append("DESCRIPTION:Reminder\r\n");
				output.Append("END:VALARM\r\n");
			}
			output.AppendLine("END:VEVENT");
		}

		// Token: 0x0600CA89 RID: 51849 RVA: 0x002D3640 File Offset: 0x002D1840
		private static string NormalizeNewLines(string input)
		{
			return input.Replace("\r\n", "\\n").Replace("\n", "\\n");
		}

		// Token: 0x0600CA8A RID: 51850 RVA: 0x002D3661 File Offset: 0x002D1861
		private static void WriteFileHeader(StringBuilder output, bool outlookCompatibleMode)
		{
			output.AppendLine("BEGIN:VCALENDAR");
			output.AppendLine("VERSION:2.0");
			output.AppendLine("PRODID:-//Telerik Inc.//NONSGML RadScheduler//EN");
			if (outlookCompatibleMode)
			{
				output.AppendLine("METHOD:PUBLISH");
			}
		}

		// Token: 0x0600CA8B RID: 51851 RVA: 0x002D3696 File Offset: 0x002D1896
		private static void WriteFileFooter(StringBuilder output)
		{
			output.AppendLine("END:VCALENDAR");
		}

		// Token: 0x0600CA8C RID: 51852 RVA: 0x002D36A4 File Offset: 0x002D18A4
		private static DateTime ClientToUtc(DateTime date, TimeSpan offset)
		{
			return new DateTime(date.Add(-offset).Ticks, DateTimeKind.Utc);
		}

		// Token: 0x0600CA8D RID: 51853 RVA: 0x002D36CC File Offset: 0x002D18CC
		private static DateTime UtcToClient(DateTime date, string timeZoneId)
		{
			return ICalUtil.providers[timeZoneId].UtcToLocal(date);
		}

		// Token: 0x0600CA8E RID: 51854 RVA: 0x002D36DF File Offset: 0x002D18DF
		private static string FormatUtcDate(DateTime date)
		{
			return date.ToString("yyyyMMddTHHmmssZ");
		}

		// Token: 0x0600CA8F RID: 51855 RVA: 0x002D36ED File Offset: 0x002D18ED
		private static string FormatDate(DateTime date)
		{
			return date.ToString("yyyyMMddTHHmmss");
		}

		// Token: 0x0600CA90 RID: 51856 RVA: 0x002D36FC File Offset: 0x002D18FC
		private static string FormatAllDayDate(DateTime date)
		{
			return date.Date.ToString("yyyyMMdd");
		}

		// Token: 0x0600CA91 RID: 51857 RVA: 0x002D3720 File Offset: 0x002D1920
		private static void ConvertRecurrenceRuleToUtc(RecurrenceRule rrule, TimeSpan offset)
		{
			rrule.Range.Start = ICalUtil.ClientToUtc(rrule.Range.Start, offset);
			if (rrule.Range.RecursUntil < DateTime.MaxValue)
			{
				rrule.Range.RecursUntil = ICalUtil.ClientToUtc(rrule.Range.RecursUntil, offset);
			}
			for (int i = 0; i < rrule.Exceptions.Count; i++)
			{
				rrule.Exceptions[i] = ICalUtil.ClientToUtc(rrule.Exceptions[i], offset);
			}
		}

		// Token: 0x0400352D RID: 13613
		private const string DateFormatUtc = "yyyyMMddTHHmmssZ";

		// Token: 0x0400352E RID: 13614
		private const string DateFormat = "yyyyMMddTHHmmss";

		// Token: 0x0400352F RID: 13615
		private const string AllDayDateFormat = "yyyyMMdd";

		// Token: 0x04003530 RID: 13616
		private const string TimeZoneRRuleFormat = "RRULE:FREQ=YEARLY;INTERVAL={0};BYSETPOS={1};BYDAY={2};BYMONTH={3}";

		// Token: 0x04003531 RID: 13617
		private static readonly TimeZoneInfoProvider provider = new TimeZoneInfoProvider();

		// Token: 0x04003532 RID: 13618
		private static readonly Dictionary<string, TimeZoneInfoProvider> providers = new Dictionary<string, TimeZoneInfoProvider>();
	}
}
