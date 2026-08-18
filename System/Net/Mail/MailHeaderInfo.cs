using System;
using System.Collections.Generic;

namespace System.Net.Mail
{
	// Token: 0x0200069D RID: 1693
	internal static class MailHeaderInfo
	{
		// Token: 0x0600344F RID: 13391 RVA: 0x000DDDFC File Offset: 0x000DCDFC
		static MailHeaderInfo()
		{
			for (int i = 0; i < MailHeaderInfo.m_HeaderInfo.Length; i++)
			{
				MailHeaderInfo.m_HeaderDictionary.Add(MailHeaderInfo.m_HeaderInfo[i].NormalizedName, i);
			}
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x000DE1A4 File Offset: 0x000DD1A4
		internal static string GetString(MailHeaderID id)
		{
			if (id == MailHeaderID.Unknown || id == (MailHeaderID)33)
			{
				return null;
			}
			return MailHeaderInfo.m_HeaderInfo[(int)id].NormalizedName;
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x000DE1D4 File Offset: 0x000DD1D4
		internal static MailHeaderID GetID(string name)
		{
			int result;
			if (MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out result))
			{
				return (MailHeaderID)result;
			}
			return MailHeaderID.Unknown;
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x000DE1F4 File Offset: 0x000DD1F4
		internal static bool IsWellKnown(string name)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num);
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000DE210 File Offset: 0x000DD210
		internal static bool IsSingleton(string name)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) && MailHeaderInfo.m_HeaderInfo[num].IsSingleton;
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000DE244 File Offset: 0x000DD244
		internal static string NormalizeCase(string name)
		{
			int num;
			if (MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num))
			{
				return MailHeaderInfo.m_HeaderInfo[num].NormalizedName;
			}
			return name;
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000DE278 File Offset: 0x000DD278
		internal static bool IsMatch(string name, MailHeaderID header)
		{
			int num;
			return MailHeaderInfo.m_HeaderDictionary.TryGetValue(name, out num) && num == (int)header;
		}

		// Token: 0x04003038 RID: 12344
		private static readonly MailHeaderInfo.HeaderInfo[] m_HeaderInfo = new MailHeaderInfo.HeaderInfo[]
		{
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Bcc, "Bcc", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Cc, "Cc", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Comments, "Comments", false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDescription, "Content-Description", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentDisposition, "Content-Disposition", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentID, "Content-ID", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentLocation, "Content-Location", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentTransferEncoding, "Content-Transfer-Encoding", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ContentType, "Content-Type", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Date, "Date", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.From, "From", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Importance, "Importance", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.InReplyTo, "In-Reply-To", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Keywords, "Keywords", false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Max, "Max", false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MessageID, "Message-ID", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.MimeVersion, "MIME-Version", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Priority, "Priority", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.References, "References", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ReplyTo, "Reply-To", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentBcc, "Resent-Bcc", false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentCc, "Resent-Cc", false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentDate, "Resent-Date", false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentFrom, "Resent-From", false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentMessageID, "Resent-Message-ID", false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentSender, "Resent-Sender", false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.ResentTo, "Resent-To", false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Sender, "Sender", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.Subject, "Subject", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.To, "To", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XPriority, "X-Priority", true),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XReceiver, "X-Receiver", false),
			new MailHeaderInfo.HeaderInfo(MailHeaderID.XSender, "X-Sender", true)
		};

		// Token: 0x04003039 RID: 12345
		private static readonly Dictionary<string, int> m_HeaderDictionary = new Dictionary<string, int>(33, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0200069E RID: 1694
		private struct HeaderInfo
		{
			// Token: 0x06003456 RID: 13398 RVA: 0x000DE29B File Offset: 0x000DD29B
			public HeaderInfo(MailHeaderID id, string name, bool isSingleton)
			{
				this.ID = id;
				this.NormalizedName = name;
				this.IsSingleton = isSingleton;
			}

			// Token: 0x0400303A RID: 12346
			public readonly string NormalizedName;

			// Token: 0x0400303B RID: 12347
			public readonly bool IsSingleton;

			// Token: 0x0400303C RID: 12348
			public readonly MailHeaderID ID;
		}
	}
}
