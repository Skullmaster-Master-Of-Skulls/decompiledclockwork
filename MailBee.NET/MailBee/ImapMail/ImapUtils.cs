using System;
using System.Globalization;
using System.Text;
using a.f;

namespace MailBee.ImapMail
{
	// Token: 0x02000195 RID: 405
	public class ImapUtils
	{
		// Token: 0x06000E7D RID: 3709 RVA: 0x00035E6F File Offset: 0x00034E6F
		private ImapUtils()
		{
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00035E77 File Offset: 0x00034E77
		public static string GetImapDateString()
		{
			return ImapUtils.GetImapDateString(DateTime.Today);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00035E83 File Offset: 0x00034E83
		public static string GetImapDateString(DateTime dt)
		{
			return ImapUtils.GetImapDateTimeString(dt, false, false);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00035E8D File Offset: 0x00034E8D
		public static string GetImapDateTimeString()
		{
			return ImapUtils.GetImapDateTimeString(DateTime.Now);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00035E99 File Offset: 0x00034E99
		public static string GetImapDateTimeString(DateTime dt)
		{
			return ImapUtils.GetImapDateTimeString(dt, true, false);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00035EA4 File Offset: 0x00034EA4
		public static string GetImapDateTimeString(DateTime dt, bool includeTime, bool isUtc)
		{
			string text = null;
			if (isUtc)
			{
				if (includeTime)
				{
					text = "+0000";
				}
			}
			else if (includeTime)
			{
				TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(dt);
				if (utcOffset < TimeSpan.Zero)
				{
					text = " -";
				}
				else
				{
					text = " +";
				}
				text += Math.Abs(utcOffset.Hours).ToString("##00");
				text += Math.Abs(utcOffset.Minutes).ToString("##00");
			}
			return ImapUtils.GetImapDateTimeString(dt, includeTime, text);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00035F34 File Offset: 0x00034F34
		public static string GetImapDateTimeString(DateTime dt, bool includeTime, string timeZoneOffset)
		{
			if (timeZoneOffset == null || !includeTime)
			{
				timeZoneOffset = string.Empty;
			}
			if (timeZoneOffset.Length > 0 && !char.IsWhiteSpace(timeZoneOffset[0]))
			{
				timeZoneOffset = " " + timeZoneOffset;
			}
			string format = includeTime ? "dd-MMM-yyyy HH:mm:ss" : "dd-MMM-yyyy";
			return dt.ToString(format, DateTimeFormatInfo.InvariantInfo) + timeZoneOffset;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00035F98 File Offset: 0x00034F98
		public static DateTime GetDateTimeFromImapDate(string dateTimeString)
		{
			if (dateTimeString == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			DateTime result;
			try
			{
				dateTimeString = dateTimeString.Trim();
				string[] array = dateTimeString.Split(null);
				string text = "d-MMM-yyyy";
				if (array.Length >= 2 && array[1].IndexOf(':') > 0)
				{
					text += " H:m:s";
				}
				if (array.Length >= 3)
				{
					text += " zzz";
				}
				result = DateTime.ParseExact(dateTimeString, text, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowInnerWhite | DateTimeStyles.AdjustToUniversal);
			}
			catch
			{
				result = DateTime.MinValue;
			}
			return result;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00036024 File Offset: 0x00035024
		public static string ToQuotedString(string s)
		{
			if (s == null)
			{
				return "\"\"";
			}
			return "\"" + global::a.f.b.a(s) + "\"";
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00036044 File Offset: 0x00035044
		public static string ToUtf8QuotedString(string s)
		{
			if (s == null)
			{
				return "\"\"";
			}
			return "\"" + global::a.f.b.a(Global.DefaultEncoding.GetString(Encoding.UTF8.GetBytes(s))) + "\"";
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00036078 File Offset: 0x00035078
		public static string ToUtf7QuotedString(string s)
		{
			if (s == null)
			{
				return "\"\"";
			}
			return "\"" + global::a.f.b.a(s, true) + "\"";
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00036099 File Offset: 0x00035099
		public static string ToUtf7String(string s)
		{
			if (s == null)
			{
				return null;
			}
			return f.b(s);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x000360A6 File Offset: 0x000350A6
		public static string FromUtf7String(string s)
		{
			if (s == null)
			{
				return null;
			}
			return f.a(s);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000360B3 File Offset: 0x000350B3
		public static string ToLiteral(string s)
		{
			return ImapUtils.ToLiteral(s, null, null);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x000360C0 File Offset: 0x000350C0
		public static string ToLiteral(string s, Encoding charsetEncoding, Encoding requestEncoding)
		{
			if (s == null || s == string.Empty)
			{
				return "\"\"";
			}
			if (charsetEncoding == null)
			{
				charsetEncoding = Encoding.UTF8;
			}
			if (requestEncoding == null)
			{
				requestEncoding = Global.DefaultEncoding;
			}
			byte[] bytes = charsetEncoding.GetBytes(s);
			return "{" + bytes.Length.ToString() + "+}\r\n" + requestEncoding.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00036124 File Offset: 0x00035124
		public static string AllOf(params string[] list)
		{
			foreach (string text in list)
			{
				if (text == null || text == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
			}
			int i = list.Length;
			if (i == 0)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if (i != 1)
			{
				return "(" + string.Join(" ", list) + ")";
			}
			return list[0];
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00036190 File Offset: 0x00035190
		public static string AnyOf(params string[] list)
		{
			foreach (string text in list)
			{
				if (text == null || text == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
			}
			int i = list.Length;
			if (i == 0)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if (i != 1)
			{
				string text2 = list[0];
				for (int j = 1; j < list.Length; j++)
				{
					text2 = string.Concat(new string[]
					{
						"(OR ",
						text2,
						" ",
						list[j],
						")"
					});
				}
				return text2;
			}
			return list[0];
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00036228 File Offset: 0x00035228
		public static string Not(string s)
		{
			if (s == null || s == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			return "NOT " + s;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003624D File Offset: 0x0003524D
		public static string GmailSearch(string s)
		{
			if (s == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			return "X-GM-RAW " + ImapUtils.ToLiteral(s);
		}
	}
}
