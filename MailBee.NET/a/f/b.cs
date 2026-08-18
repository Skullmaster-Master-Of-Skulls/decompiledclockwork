using System;
using System.Text;
using MailBee.ImapMail;

namespace a.f
{
	// Token: 0x0200008C RID: 140
	internal class b
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x0000D7B9 File Offset: 0x0000C7B9
		private b()
		{
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000D7C1 File Offset: 0x0000C7C1
		public static string a(string A_0)
		{
			return A_0.Replace("\\", "\\\\").Replace("\"", "\\\"");
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000D7E2 File Offset: 0x0000C7E2
		public static string a(string A_0, bool A_1)
		{
			if (!A_1)
			{
				return b.a(A_0);
			}
			return b.a(f.b(A_0));
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000D7FC File Offset: 0x0000C7FC
		public static string a(EnvelopeParts A_0, int A_1, string[] A_2, string[] A_3, out string A_4, out string A_5, out string A_6)
		{
			StringBuilder stringBuilder = new StringBuilder("UID");
			A_4 = null;
			A_5 = null;
			if ((A_0 & EnvelopeParts.Flags) > EnvelopeParts.Uid)
			{
				stringBuilder.Append(" FLAGS");
			}
			if ((A_0 & EnvelopeParts.InternalDate) > EnvelopeParts.Uid)
			{
				stringBuilder.Append(" INTERNALDATE");
			}
			if ((A_0 & EnvelopeParts.Rfc822Size) > EnvelopeParts.Uid || (A_0 & EnvelopeParts.MessagePreview) > EnvelopeParts.Uid)
			{
				stringBuilder.Append(" RFC822.SIZE");
			}
			if ((A_0 & EnvelopeParts.GmailMessageID) > EnvelopeParts.Uid)
			{
				stringBuilder.Append(" X-GM-MSGID");
			}
			if ((A_0 & EnvelopeParts.GmailThreadID) > EnvelopeParts.Uid)
			{
				stringBuilder.Append(" X-GM-THRID");
			}
			if ((A_0 & EnvelopeParts.GmailLabels) > EnvelopeParts.Uid)
			{
				stringBuilder.Append(" X-GM-LABELS");
			}
			if ((A_0 & EnvelopeParts.Envelope) > EnvelopeParts.Uid)
			{
				stringBuilder.Append(" ENVELOPE");
				if ((A_0 & EnvelopeParts.BodyStructure) == EnvelopeParts.Uid && (A_0 & EnvelopeParts.MessagePreview) == EnvelopeParts.Uid)
				{
					stringBuilder.Append(" BODY.PEEK[HEADER.FIELDS (CONTENT-TYPE)]");
				}
			}
			if ((A_0 & EnvelopeParts.BodyStructure) > EnvelopeParts.Uid)
			{
				stringBuilder.Append(" BODYSTRUCTURE");
			}
			if ((A_0 & EnvelopeParts.MessagePreview) > EnvelopeParts.Uid)
			{
				if (A_1 < 0)
				{
					A_4 = "BODY[]";
					A_5 = null;
					stringBuilder.Append(" " + ((A_1 == -1) ? "BODY[]" : "BODY.PEEK[]"));
				}
				else if (A_1 == 0)
				{
					A_4 = "BODY[HEADER]";
					A_5 = null;
					stringBuilder.Append(" BODY.PEEK[HEADER]");
				}
				else
				{
					A_4 = "BODY[HEADER]";
					A_5 = "BODY[TEXT]<0>";
					stringBuilder.Append(" BODY.PEEK[HEADER] BODY.PEEK[TEXT]<0." + A_1.ToString() + ">");
				}
			}
			if (A_2 == null)
			{
				A_6 = null;
			}
			else
			{
				string str = "[HEADER.FIELDS (" + string.Join(" ", A_2) + ")]";
				A_6 = "BODY" + str;
				stringBuilder.Append(" BODY.PEEK" + str);
			}
			if (A_3 != null)
			{
				stringBuilder.Append(" " + string.Join(" ", A_3));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000D9C4 File Offset: 0x0000C9C4
		public static string a(SystemMessageFlags A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if ((A_0 & SystemMessageFlags.Seen) > SystemMessageFlags.None)
			{
				stringBuilder.Append(" \\Seen");
			}
			if ((A_0 & SystemMessageFlags.Answered) > SystemMessageFlags.None)
			{
				stringBuilder.Append(" \\Answered");
			}
			if ((A_0 & SystemMessageFlags.Flagged) > SystemMessageFlags.None)
			{
				stringBuilder.Append(" \\Flagged");
			}
			if ((A_0 & SystemMessageFlags.Deleted) > SystemMessageFlags.None)
			{
				stringBuilder.Append(" \\Deleted");
			}
			if ((A_0 & SystemMessageFlags.Draft) > SystemMessageFlags.None)
			{
				stringBuilder.Append(" \\Draft");
			}
			if ((A_0 & SystemMessageFlags.Recent) > SystemMessageFlags.None)
			{
				stringBuilder.Append(" \\Recent");
			}
			if (stringBuilder.Length == 0)
			{
				return string.Empty;
			}
			return stringBuilder.Remove(0, 1).ToString();
		}
	}
}
