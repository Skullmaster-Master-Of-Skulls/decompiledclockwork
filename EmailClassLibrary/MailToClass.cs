using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EmailClassLibrary
{
	// Token: 0x02000007 RID: 7
	public class MailToClass
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000027F3 File Offset: 0x000017F3
		public static void MailTo(Process process, List<string> to, string subject)
		{
			MailToClass.MailTo(process, MailToClass.ToDelimitedString(to, ';'), null, null, subject, null);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002807 File Offset: 0x00001807
		public static void MailTo(Process process, List<string> to, string subject, string body)
		{
			MailToClass.MailTo(process, MailToClass.ToDelimitedString(to, ';'), null, null, subject, body);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000281B File Offset: 0x0000181B
		public static void MailTo(Process process, List<string> to, List<string> cc, string subject, string body)
		{
			MailToClass.MailTo(process, MailToClass.ToDelimitedString(to, ';'), MailToClass.ToDelimitedString(cc, ';'), null, subject, body);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002837 File Offset: 0x00001837
		public static void MailTo(Process process, List<string> to, List<string> cc, List<string> bcc, string subject, string body)
		{
			MailToClass.MailTo(process, (to == null) ? null : MailToClass.ToDelimitedString(to, ';'), (cc == null) ? null : MailToClass.ToDelimitedString(cc, ';'), (bcc == null) ? null : MailToClass.ToDelimitedString(bcc, ';'), subject, body);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000286D File Offset: 0x0000186D
		public static void MailTo(Process process, List<string> to, List<string> cc, List<string> bcc, string subject, string body, string attachmentPath)
		{
			MailToClass.MailTo(process, (to == null) ? null : MailToClass.ToDelimitedString(to, ';'), (cc == null) ? null : MailToClass.ToDelimitedString(cc, ';'), (bcc == null) ? null : MailToClass.ToDelimitedString(bcc, ';'), subject, body, attachmentPath);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028A5 File Offset: 0x000018A5
		public static void MailTo(Process process, string to, string cc, string bcc, string subject, string body)
		{
			MailToClass.MailTo(process, to, cc, bcc, subject, body, null);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028B8 File Offset: 0x000018B8
		public static void MailTo(Process process, string to, string cc, string bcc, string subject, string body, string attachmentPath)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Uri.UriSchemeMailto + ":");
			stringBuilder.Append(MailToClass.FormatMailToArgument(to));
			if (!string.IsNullOrEmpty(cc) || !string.IsNullOrEmpty(bcc) || !string.IsNullOrEmpty(subject) || !string.IsNullOrEmpty(body) || !string.IsNullOrEmpty(attachmentPath))
			{
				stringBuilder.Append('?');
				List<string> list = new List<string>();
				if (!string.IsNullOrEmpty(subject))
				{
					list.Add("subject=" + MailToClass.FormatMailToArgument(subject));
				}
				if (!string.IsNullOrEmpty(body))
				{
					list.Add("body=" + MailToClass.FormatMailToArgument(body));
				}
				if (!string.IsNullOrEmpty(cc))
				{
					list.Add("CC=" + MailToClass.FormatMailToArgument(cc));
				}
				if (!string.IsNullOrEmpty(bcc))
				{
					list.Add("BCC=" + MailToClass.FormatMailToArgument(bcc));
				}
				if (!string.IsNullOrEmpty(attachmentPath))
				{
					list.Add("attachment=" + MailToClass.FormatMailToArgument(attachmentPath));
				}
				stringBuilder.Append(MailToClass.ToDelimitedString(list, '&'));
			}
			string fileName = Uri.EscapeUriString(stringBuilder.ToString());
			process.StartInfo = new ProcessStartInfo(fileName);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029F0 File Offset: 0x000019F0
		private static string ToDelimitedString(List<string> args, char delimiter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < args.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(delimiter);
				}
				stringBuilder.Append(args[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A34 File Offset: 0x00001A34
		private static string FormatMailToArgument(string argument)
		{
			return argument;
		}
	}
}
